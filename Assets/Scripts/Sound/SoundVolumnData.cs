using UnityEngine;

[System.Serializable]
public class SoundVolumnData
{
    // 데이터 싱글턴
    [SerializeField] float _bgmSound;
    [SerializeField] float _sfxSound;

    public float BgmSound { get { return _bgmSound; } set { _bgmSound = value; } }
    public float SFXSound { get { return _sfxSound; } set { _sfxSound = value; } }
}
