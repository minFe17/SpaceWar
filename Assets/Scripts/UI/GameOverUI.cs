using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Transform[] _firstMap;
    [SerializeField] Transform[] _secondMap;
    [SerializeField] Transform[] _thirdMap;

    [SerializeField] Transform _startPos;

    [SerializeField] Transform _playerImage;
    [SerializeField] TMP_Text _dieWaveText;

    [SerializeField] TMP_Text _playTimeText;
    [SerializeField] TMP_Text _killEnemyText;
    [SerializeField] TMP_Text _moneyText;

    public List<Transform[]> _wavePos = new List<Transform[]>();

    Vector3 _dieWavePos;

    void Awake()
    {
        _wavePos.Add(_firstMap);
        _wavePos.Add(_secondMap);
        _wavePos.Add(_thirdMap);
    }

    public void ShowPlayTime(float time)
    {
        int minute = (int)time / 60;
        int sec = (int)time % 60;
        _playTimeText.text = string.Format("{0:D2} : {1:D2}", minute, sec);
    }

    public void ShowKillEnemy(int killEnemy)
    {
        _killEnemyText.text = string.Format("{0:D3}", killEnemy);
    }

    public void ShowMoney(int money)
    {
        _moneyText.text = string.Format("{0:D3}", money);
    }

    public void ShowWave(int mapStage, int levelStage)
    {
        _playerImage.position = _startPos.position;
        _dieWavePos = _wavePos[mapStage - 1][levelStage - 1].position;
        _dieWaveText.text = $"{mapStage} - {levelStage}";
    }

    public void RegameButton()
    {
        SceneManager.LoadScene("Main");
    }

    void Update()
    {
        MovePlayerIcon();
    }

    void MovePlayerIcon()
    {
        _playerImage.Translate((_dieWavePos - _playerImage.position) * Time.deltaTime * 2f);
    }
}
