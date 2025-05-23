using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class SoundManager : MonoBehaviour
{
    // ╫л╠шео
    SoundController _soundController;
    GameObject _soundControllerPrefab;
    AddressableManager _addressableManager;

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

        set
        {
            _soundController = value;
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
        GenericSingleton<JsonManager>.Instance.ReadSoundDataFile();
    }
}