using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] IngameUI _uiManager;
    [SerializeField] GameOverUI _gameOverUI; 

    int _mapStage;
    int _levelStage;
    int _killEnemy;

    float _playTime;


    void Start()
    {
        _mapStage = 1;
        _levelStage = 1;
        _uiManager.ShowStage(_mapStage, _levelStage);
    }

    void Update()
    {
        PlayTime();
    }

    public void Battle(DoorList doorList)
    {
        doorList.LockDoor();
    }

    public void Clear(DoorList doorList)
    {
        doorList.UnlockDoor();
    }

    public void NextStage()
    {
        //SceneManager.LoadScene();
        if(_levelStage >= 5)
        {
            _mapStage++;
            _levelStage = 1;
        }
        else
        {
            _levelStage++;
        }
        _uiManager.ShowStage(_mapStage, _levelStage);
    }

    public void AddKillEnemy()
    {
        _killEnemy++;
    }

    void PlayTime()
    {
        _playTime += Time.deltaTime;
    }

    public void GameOver()
    {
        _gameOverUI.ShowPlayTime(_playTime);
        _gameOverUI.ShowKillEnemy(_killEnemy);
        _gameOverUI.ShowWave(_mapStage, _levelStage);
    }
}
