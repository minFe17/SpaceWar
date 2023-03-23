using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavePanel : MonoBehaviour
{
    [SerializeField] GameOverUI _gameOverUI;
    [SerializeField] Transform _startPos;

    [SerializeField] Transform _playerImage;
    [SerializeField] TMP_Text _dieWaveText;

    void Start()
    {
        _playerImage.position = _startPos.position;
    }

    public void ShowWave(int mapStage, int levelStage)
    {
        _playerImage.position = _gameOverUI._wavePos[mapStage][levelStage].position;
        _dieWaveText.text = $"{mapStage} - {levelStage}";
    }
}
