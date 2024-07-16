using System.Collections.Generic;
using UnityEngine;
using Utils;

public class AudioClipManager : MonoBehaviour
{
    // ╫л╠шео
    SoundController _soundController;
    List<AudioClip> _sfxAudio = new List<AudioClip>();
    List<AudioClip> _bgmAudio = new List<AudioClip>();

    public void Init()
    {
        _soundController = GenericSingleton<SoundManager>.Instance.SoundController;
        SetSFXAudio();
        SetBGMAudio();
    }

    void SetSFXAudio()
    {
        for (int i = 0; i < (int)ESFXAudioType.Max; i++)
            _sfxAudio.Add(Resources.Load($"Prefabs/SoundClip/{(ESFXAudioType)i}") as AudioClip);
    }

    void SetBGMAudio()
    {
        for (int i = 0; i < (int)EBGMAudioType.Max; i++)
            _bgmAudio.Add(Resources.Load($"Prefabs/SoundClip/{(EBGMAudioType)i}") as AudioClip);
    }

    public void PlaySFX(ESFXAudioType audioType)
    {
        _soundController.PlaySFXAudio(_sfxAudio[(int)audioType]);
    }

    public void PlayBGM(EBGMAudioType audioType)
    {
        _soundController.StartBGM(_bgmAudio[(int)audioType]);
    }
}