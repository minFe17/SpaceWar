using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public GameObject Portal { get; set; }

    int _mapStage = 1;
    int _levelStage = 1;
    int _killEnemy;
    float _playTime;
    bool _isAddPassive = true;

    public int MapStage { get => _mapStage; }
    public int LevelStage { get => _levelStage; }

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

        if (_isAddPassive == true)
        {
            _isAddPassive = false;
            SelectPassive();
        }
        else
            _isAddPassive = true;

        // 데이터 쓰기
        SceneManager.LoadScene($"{(EWorldType)_mapStage}");
    }

    public void AddKillEnemy()
    {
        _killEnemy++;
    }

    public void StageUI()
    {
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage(_mapStage, _levelStage);
    }

    void PlayTime()
    {
        _playTime += Time.deltaTime;
    }

    public void SelectPassive()
    {
        // ui 띄우기
        // 데이터 받아서 3가지 보여주기
        // 선택할 때까지 씬이동 x
        // 이름, 설명 가운데에 보이기
        // 선택 후 ui 숨기고 씬 이동
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