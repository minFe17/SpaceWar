using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    SoundController _soundController;
    float _bgmSound = 0.5f;
    float _sfxSound = 0.5f;
    // 파일 쓴게 없으면 0.5f
    // 있으면 파일 읽은 값 사용
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
}
