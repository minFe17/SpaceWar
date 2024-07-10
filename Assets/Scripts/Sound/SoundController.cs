using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource _bgm;
    [SerializeField] List<AudioSource> _sfxAudio;

    int _index;

    public AudioSource BGM { get { return _bgm; } }
    public List<AudioSource> SFXAudio { get { return _sfxAudio; } }

    public void PlaySFXAudio(AudioClip audio)
    {
        _sfxAudio[_index].clip = audio;
        _sfxAudio[_index].Play();
        _index++;
        if (_index == _sfxAudio.Count)
            _index = 0;
    }

    public void StartBGM(AudioClip audio)
    {
        _bgm.clip = audio;
        _bgm.Play();
    }

    public void StopBGM()
    {
        _bgm.Stop();
    }
}