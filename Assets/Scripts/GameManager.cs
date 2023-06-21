using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    // ½Ì±ÛÅæ
    int _mapStage = 1;
    int _levelStage = 1;
    int _killEnemy;
    float _playTime;
    bool _isAddPassive = true;
    bool _isClear;

    public GameObject Portal { get; set; }
    public int MapStage { get => _mapStage; set => _mapStage = value; }
    public int LevelStage { get => _levelStage; set => _levelStage = value; }
    public int KillEnemy { get => _killEnemy; set => _killEnemy = value; }
    public float PlayTime { get => _playTime; set => _playTime = value;  }
    public bool IsAddPassive { get => _isAddPassive; set => _isAddPassive = value;  }
    public bool IsClear { get => _isClear; }

    void Update()
    {
        AddPlayTime();
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
        if(_mapStage >= 3 && _levelStage >= 5)
        {
            GenericSingleton<PlayerDataManager>.Instance.Player.GameOver();
            _isClear = true;
        }
        if (_levelStage >= 5)
        {
            _mapStage++;
            _levelStage = 1;
            GenericSingleton<EnemyManager>.Instance.ClearWorldEnemy();
            GenericSingleton<EnemyManager>.Instance.WorldEnemyList();
            GenericSingleton<ObstacleManager>.Instance.ClearWorldObstacleList();
            GenericSingleton<ObstacleManager>.Instance.WorldObstacleList();
        }
        else
        {
            _levelStage++;
        }

        if (_isAddPassive == true)
        {
            _isAddPassive = false;
            SelectPassive();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _isAddPassive = true;
            CsvController csvController = GenericSingleton<CsvController>.Instance;
            csvController.WriteDataFile();
            while (csvController.IsWriting == true || csvController.CheckDataFile() == false)
            {
                if (csvController.IsWriting == false && csvController.CheckDataFile() == true)
                    break;
            }
            SceneManager.LoadScene($"{(EWorldType)_mapStage}");
        }
    }

    public void AddKillEnemy()
    {
        _killEnemy++;
    }

    public void StageUI()
    {
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage(_mapStage, _levelStage);
    }

    void AddPlayTime()
    {
        _playTime += Time.deltaTime;
    }

    public void SelectPassive()
    {
        List<string> playerPassive = GenericSingleton<PlayerDataManager>.Instance.Passive;
        List<PassiveBase> passiveList = GenericSingleton<PassiveManager>.Instance.Passive;
        GenericSingleton<UIManager>.Instance.SelectPassiveUI.gameObject.SetActive(true);

        List<int> passiveIndex = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int random;
            do
            {
                random = Random.Range(0, passiveList.Count);
            }
            while (passiveIndex.Contains(random) || playerPassive.Contains(passiveList[random].Name));

            passiveIndex.Add(random);
            GenericSingleton<UIManager>.Instance.SelectPassiveUI.PassiveButton[i].Passive = GenericSingleton<PassiveManager>.Instance.Passive[random];
            GenericSingleton<UIManager>.Instance.SelectPassiveUI.PassiveButton[i].Init();
        }
    }
}

public enum EWorldType
{
    None,
    FirstWorld,
    SecondWorld,
    ThirdWorld,
}