using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱���
    SoundController _soundController;
    float _bgmSound = 0.5f;
    float _sfxSound = 0.5f;
    // ���� ���� ������ 0.5f
    // ������ ���� ���� �� ���
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
