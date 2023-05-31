using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Transform[] _firstMap;
    [SerializeField] Transform[] _secondMap;
    [SerializeField] Transform[] _thirdMap;

    [SerializeField] Transform _startPos;

    [SerializeField] Transform _playerImage;
    [SerializeField] TMP_Text _dieWaveText;
    [SerializeField] GameObject _clearPanel;

    [SerializeField] TMP_Text _playTimeText;
    [SerializeField] TMP_Text _killEnemyText;
    [SerializeField] TMP_Text _moneyText;

    List<Transform[]> _wavePos = new List<Transform[]>();

    Vector3 _dieWavePos;

    void Awake()
    {
        _wavePos.Add(_firstMap);
        _wavePos.Add(_secondMap);
        _wavePos.Add(_thirdMap);
        _clearPanel.SetActive(false);
    }

    void Update()
    {
        MovePlayerIcon();
    }

    public void ShowPlayTime()
    {
        float time = GenericSingleton<GameManager>.Instance.PlayTime;
        int minute = (int)time / 60;
        int sec = (int)time % 60;
        _playTimeText.text = string.Format("{0:D2} : {1:D2}", minute, sec);
    }

    public void ShowKillEnemy()
    {
        int killEnemy = GenericSingleton<GameManager>.Instance.KillEnemy;
        _killEnemyText.text = string.Format("{0:D3}", killEnemy);
    }

    public void ShowMoney()
    {
        int money = GenericSingleton<PlayerDataManager>.Instance.Money;
        _moneyText.text = string.Format("{0:D3}", money);
    }

    public void ShowWave()
    {
        int mapStage = GenericSingleton<GameManager>.Instance.MapStage;
        int levelStage = GenericSingleton<GameManager>.Instance.LevelStage;
        _playerImage.position = _startPos.position;
        _dieWavePos = _wavePos[mapStage - 1][levelStage - 1].position;
        _dieWaveText.text = $"{mapStage} - {levelStage}";
    }

    public void RegameButton()
    {
        SceneManager.LoadScene("FirstWorld");
    }

    void MovePlayerIcon()
    {
        _playerImage.Translate((_dieWavePos - _playerImage.position) * Time.deltaTime * 2f);
    }
}