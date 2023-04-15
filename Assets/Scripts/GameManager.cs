using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    int _mapStage;
    public int MapStage { get { return _mapStage; } }
    int _levelStage;
    int _killEnemy;

    float _playTime;

    void Start()
    {
        _mapStage = 1;
        _levelStage = 1;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage(_mapStage, _levelStage);
        GenericSingleton<EnemyManager>.Instance.WorldEnemyList();
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
        if (_levelStage >= 5)
        {
            _mapStage++;
            _levelStage = 1;
            GenericSingleton<EnemyManager>.Instance.ClearWorldEnemy();
            GenericSingleton<EnemyManager>.Instance.WorldEnemyList();
        }
        else
        {
            _levelStage++;
        }

        SceneManager.LoadScene($"{(EWorldType)_mapStage}");
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage(_mapStage, _levelStage);
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
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowPlayTime(_playTime);
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowKillEnemy(_killEnemy);
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowWave(_mapStage, _levelStage);
    }
}

public enum EWorldType
{
    None,
    FirstWorld,
    SecondWorld,
    ThirdWorld,
}