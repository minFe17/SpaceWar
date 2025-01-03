using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    // �̱���
    public GameObject Portal { get; set; }
    public int MapStage { get; set; }
    public int LevelStage { get; set; }
    public int KillEnemy { get; set; }
    public float PlayTime { get; set;  }
    public bool IsAddPassive { get; set;  }
    public bool IsClear { get; private set; }

    void Update()
    {
        AddPlayTime();
    }

    public async void NextStage()
    {
        if(MapStage >= 3 && LevelStage >= 5)
        {
            GenericSingleton<PlayerDataManager>.Instance.Player.GameOver();
            IsClear = true;
        }
        if (LevelStage >= 5)
        {
            await GenericSingleton<WorldManager>.Instance.NextWorld();
            LevelStage = 1;
        }
        else
        {
            LevelStage++;
        }

        if (IsAddPassive == true)
        {
            IsAddPassive = false;
            SelectPassive();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            IsAddPassive = true;
            CsvManager csvManager = GenericSingleton<CsvManager>.Instance;
            csvManager.WriteDataFile();
            while (csvManager.IsWriting == true || csvManager.CheckDataFiles() == false)
            {
                if (csvManager.IsWriting == false && csvManager.CheckDataFiles() == true)
                    break;
            }
            GenericSingleton<DoorManager>.Instance.ClearDoors();
            SceneManager.LoadScene($"{(EWorldType)MapStage}");
        }
    }

    public void AddKillEnemy()
    {
        KillEnemy++;
    }

    public void StageUI()
    {
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage();
    }

    void AddPlayTime()
    {
        PlayTime += Time.deltaTime;
    }

    public void SelectPassive()
    {
        List<string> playerPassive = GenericSingleton<PlayerDataManager>.Instance.Passive;
        List<IPassive> passiveList = GenericSingleton<PassiveManager>.Instance.Passive;
        GenericSingleton<UIManager>.Instance.SelectPassiveUI.gameObject.SetActive(true);

        List<int> passiveIndex = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int random;
            do
            {
                random = Random.Range(0, passiveList.Count);
            }
            while (passiveIndex.Contains(random) || playerPassive.Contains(passiveList[random].PassiveData.Name));

            passiveIndex.Add(random);
            GenericSingleton<UIManager>.Instance.SelectPassiveUI.PassiveButton[i].Passive = GenericSingleton<PassiveManager>.Instance.Passive[random];
            GenericSingleton<UIManager>.Instance.SelectPassiveUI.PassiveButton[i].Init();
        }
    }

    public void ResetData()
    {
        MapStage = 1;
        LevelStage = 1;
        KillEnemy = 0;
        PlayTime = 0f;
        IsAddPassive = true;
        IsClear = false;
    }
}