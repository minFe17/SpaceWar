using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Transform[] _firstMap;
    [SerializeField] Transform[] _secondMap;
    [SerializeField] Transform[] _thirdMap;

    [SerializeField] Transform _playerImage;
    [SerializeField] TMP_Text _dieWaveText;

    [SerializeField] TMP_Text _playTimeText;
    [SerializeField] TMP_Text _killEnemyText;
    [SerializeField] TMP_Text _coinText;

    List<Transform[]> _wavePos = new List<Transform[]>();

    void Start()
    {

        _wavePos.Add(_firstMap);
        _wavePos.Add(_secondMap);
        _wavePos.Add(_thirdMap);

        Vector3 pos = new Vector3(_wavePos[0][3].transform.position.x, _playerImage.transform.position.y, _playerImage.transform.position.z);
        _playerImage.transform.position = pos;
    }

    public void RegameButton()
    {

    }
}
