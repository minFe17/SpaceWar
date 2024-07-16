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
    SoundController _soundController;
    AudioClipManager _audioClipManager;

    void Awake()
    {
        _soundManager = GenericSingleton<SoundManager>.Instance;
        _soundController = _soundManager.SoundController;
        _audioClipManager = GenericSingleton<AudioClipManager>.Instance;

        _bgmSlider.value = _soundManager.BgmSound;
        _sfxSlider.value = _soundManager.SFXSound;

        _soundController.BGM.volume = _soundManager.BgmSound;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
            _soundController.SFXAudio[i].volume = _soundManager.SFXSound;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseButton();
    }

    public void CloseButton()
    {
        _audioClipManager.PlaySFX(ESFXAudioType.Button);
        GenericSingleton<SoundCsv>.Instance.WriteSound();
        this.gameObject.SetActive(false);
        _parent.SetActive(true);
        if(SceneManager.GetActiveScene().name != "Lobbby")
            GenericSingleton<UIManager>.Instance.IsSoundOption = false;
    }

    public void ChangeBGMSoundVolume()
    {
        _soundManager.BgmSound = _bgmSlider.value;
        _soundController.BGM.volume = _bgmSlider.value;
    }

    public void ChangeSFXSoundVolume()
    {
        _soundManager.SFXSound = _sfxSlider.value;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
        {
            _soundController.SFXAudio[i].volume = _sfxSlider.value;
        }
    }
}