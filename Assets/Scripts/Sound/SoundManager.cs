using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class SoundManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    SoundController _soundController;
    GameObject _soundControllerPrefab;
    AddressableManager _addressableManager;
    float _bgmSound;
    float _sfxSound;

    public float BgmSound { get { return _bgmSound; } set { _bgmSound = value; } }
    public float SFXSound { get { return _sfxSound; } set { _sfxSound = value; } }

    public SoundController SoundController
    {
        get
        {
            if (_soundController == null)
            {
                GameObject soundController = Instantiate(_soundControllerPrefab);
                _soundController = soundController.GetComponent<SoundController>();
            }
            return _soundController;
        }
    }

    public async Task LoadAsset()
    {
        if (_soundControllerPrefab != null)
            return;
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        _soundControllerPrefab = await _addressableManager.GetAddressableAsset<GameObject>("SoundController");
    }

    public void Init()
    {
        CheckCsvFile();
    }

    void CheckCsvFile()
    {
        if (!GenericSingleton<SoundCsv>.Instance.ReadSound())
        {
            _bgmSound = 0.5f;
            _sfxSound = 0.5f;
        }
    }
}