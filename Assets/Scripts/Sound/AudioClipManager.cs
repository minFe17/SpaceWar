using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AudioClipManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManaegr;
    SoundController _soundController;
    AudioClip _bgmAudio;
    List<AudioClip> _sfxAudio = new List<AudioClip>();

    public void Init()
    {
        CreateSoundController();
    }

    public async Task LoadAsset()
    {
        if (_addressableManaegr == null)
            _addressableManaegr = GenericSingleton<AddressableManager>.Instance;
        SetBGMAudio();
        await SetSFXAudio();
    }

    void CreateSoundController()
    {
        if (_soundController == null)
            _soundController = GenericSingleton<SoundManager>.Instance.SoundController;
    }

    async void SetBGMAudio()
    {
        _bgmAudio = await _addressableManaegr.GetAddressableAsset<AudioClip>("BGM");
    }

    async Task SetSFXAudio()
    {
        for (int i = 0; i < (int)ESFXAudioType.Max; i++)
        {
            ESFXAudioType audio = (ESFXAudioType)i;
            AudioClip clip = await _addressableManaegr.GetAddressableAsset<AudioClip>(audio.ToString());
            _sfxAudio.Add(clip);
        }
    }

    public void PlaySFX(ESFXAudioType audioType)
    {
        CreateSoundController();
        _soundController.PlaySFXAudio(_sfxAudio[(int)audioType]);
    }

    public void PlayBGM(EBGMAudioType audioType)
    {
        CreateSoundController();
        _soundController.StartBGM(_bgmAudio);
    }
}