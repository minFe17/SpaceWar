using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Transform[] _firstMap;
    [SerializeField] Transform[] _secondMap;
    [SerializeField] Transform[] _thirdMap;

    [SerializeField] Transform _playerImage;
    [SerializeField] TMP_Text _dieWaveText;

    [SerializeField] TMP_Text _playTimeText;
    [SerializeField] TMP_Text _killEnemyText;
    [SerializeField] TMP_Text _moneyText;

    List<Transform[]> _wavePos = new List<Transform[]>();

    void Start()
    {

        _wavePos.Add(_firstMap);
        _wavePos.Add(_secondMap);
        _wavePos.Add(_thirdMap);

        //Vector3 pos = new Vector3(_wavePos[0][3].transform.position.x, _playerImage.transform.position.y, _playerImage.transform.position.z);
        //_playerImage.transform.position = pos;
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

    public void ShowWave()
    {

    }

    public void RegameButton()
    {
        //SceneManager.LoadScene();
    }
}
