using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Transform[] _firstMap;
    [SerializeField] Transform[] _secondMap;
    [SerializeField] Transform[] _thirdMap;
    [SerializeField] Sprite[] _playerSprite;

    [SerializeField] Transform _startPos;

    [SerializeField] Image _playerImage;
    [SerializeField] Transform _playerIcon;
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

        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;
        _playerImage.sprite = _playerSprite[(int)playerType];
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
        _playerIcon.position = _startPos.position;
        _dieWavePos = _wavePos[mapStage - 1][levelStage - 1].position;

        if (!gameManager.IsClear)
            _dieWaveText.text = $"{mapStage} - {levelStage}";
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
        int money = DataSingleton<PlayerData>.Instance.Money;
        _moneyText.text = string.Format("{0:D3}", money);
    }

    public async void RegameButton()
    {
        GenericSingleton<JsonManager>.Instance.DestroyDataFiles();
        await GenericSingleton<WorldManager>.Instance.ResetWorld();
        SceneManager.LoadScene("FirstWorld");
        GenericSingleton<AudioClipManager>.Instance.PlaySFX(ESFXAudioType.Button);
    }

    void MovePlayerIcon()
    {
        _playerIcon.Translate((_dieWavePos - _playerIcon.position) * Time.deltaTime * 2f);
    }
}