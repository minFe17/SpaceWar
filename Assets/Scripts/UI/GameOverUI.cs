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

    GameData _gameData = DataSingleton<GameData>.Instance;
    Vector3 _dieWavePos;

    void Awake()
    {
        _wavePos.Add(_firstMap);
        _wavePos.Add(_secondMap);
        _wavePos.Add(_thirdMap);
    }

    void Update()
    {
        MovePlayerIcon();
    }

    public void GameOver()
    {
        ShowWave();
        ShowPlayTime();
        ShowKillEnemy();
        ShowMoney();
    }

    void ShowWave()
    {
        GameManager gameManager = GenericSingleton<GameManager>.Instance;
        int mapStage = _gameData.MapStage;
        int levelStage = _gameData.LevelStage;
        _playerImage.position = _startPos.position;
        _dieWavePos = _wavePos[mapStage - 1][levelStage - 1].position;
        if (!gameManager.IsClear)
        {
            _dieWaveText.text = $"{mapStage} - {levelStage}";
        }
        else
        {
            _dieWaveText.gameObject.SetActive(false);
            _clearPanel.SetActive(true);
        }
    }

    void ShowPlayTime()
    {
        float time = _gameData.PlayTime;
        int minute = (int)time / 60;
        int sec = (int)time % 60;
        _playTimeText.text = string.Format("{0:D2} : {1:D2}", minute, sec);
    }

    void ShowKillEnemy()
    {
        int killEnemy = _gameData.KillEnemy;
        _killEnemyText.text = string.Format("{0:D3}", killEnemy);
    }

    void ShowMoney()
    {
        int money = GenericSingleton<PlayerDataManager>.Instance.Money;
        _moneyText.text = string.Format("{0:D3}", money);
    }

    public async void RegameButton()
    {
        GenericSingleton<CsvManager>.Instance.DestroyDataFiles();
        await GenericSingleton<WorldManager>.Instance.ResetWorld();
        SceneManager.LoadScene("FirstWorld");
        GenericSingleton<AudioClipManager>.Instance.PlaySFX(ESFXAudioType.Button);
    }

    void MovePlayerIcon()
    {
        _playerImage.Translate((_dieWavePos - _playerImage.position) * Time.deltaTime * 2f);
    }
}