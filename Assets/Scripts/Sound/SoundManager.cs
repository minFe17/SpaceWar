using UnityEngine;
using Utils;

public class SoundManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    SoundController _soundController;
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
                GameObject temp = Resources.Load("Prefabs/SoundController") as GameObject;
                GameObject soundController = Instantiate(temp);
                _soundController = soundController.GetComponent<SoundController>();
            }
            return _soundController;
        }
    }

    public void Init()
    {
        CheckCsvFile();
    }

    void CheckCsvFile()
    {
        if(!GenericSingleton<SoundCsv>.Instance.ReadSound())
        {
            _bgmSound = 0.5f;
            _sfxSound = 0.5f;
        }
    }
}
