using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class SoundOptionUI : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    SoundManager _soundManager;
    SoundVolumnData _soundVolumnData;
    SoundController _soundController;
    AudioClipManager _audioClipManager;

    void Awake()
    {
        _soundManager = GenericSingleton<SoundManager>.Instance;
        _soundVolumnData = DataSingleton<SoundVolumnData>.Instance;
        _soundController = _soundManager.SoundController;
        _audioClipManager = GenericSingleton<AudioClipManager>.Instance;

        _bgmSlider.value = _soundVolumnData.BgmSound;
        _sfxSlider.value = _soundVolumnData.SFXSound;

        _soundController.BGM.volume = _soundVolumnData.BgmSound;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
            _soundController.SFXAudio[i].volume = _soundVolumnData.SFXSound;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseButton();
    }

    public void CloseButton()
    {
        _audioClipManager.PlaySFX(ESFXAudioType.Button);
        GenericSingleton<CsvManager>.Instance.WriteSoundDataFile();
        this.gameObject.SetActive(false);
        _parent.SetActive(true);
        if(SceneManager.GetActiveScene().name != "Lobbby")
            GenericSingleton<UIManager>.Instance.IsSoundOption = false;
    }

    public void ChangeBGMSoundVolume()
    {
        _soundVolumnData.BgmSound = _bgmSlider.value;
        _soundController.BGM.volume = _bgmSlider.value;
    }

    public void ChangeSFXSoundVolume()
    {
        _soundVolumnData.SFXSound = _sfxSlider.value;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
        {
            _soundController.SFXAudio[i].volume = _sfxSlider.value;
        }
    }
}