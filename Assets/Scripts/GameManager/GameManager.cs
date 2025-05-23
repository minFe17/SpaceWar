using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    // ╫л╠шео

    GameData _gameData = DataSingleton<GameData>.Instance;

    public GameObject Portal { get; set; }
    public bool IsClear { get; private set; }

    void Update()
    {
        AddPlayTime();
    }

    public async void NextStage()
    {
        if (_gameData.MapStage >= 3 && _gameData.LevelStage >= 5)
        {
            GenericSingleton<PlayerDataManager>.Instance.Player.GameOver();
            IsClear = true;
        }
        if (_gameData.LevelStage >= 5)
        {
            await GenericSingleton<WorldManager>.Instance.NextWorld();
            _gameData.LevelStage = 1;
            _gameData.MapStage++;
        }
        else
            _gameData.LevelStage++;

        if (_gameData.IsAddPassive == true)
        {
            _gameData.IsAddPassive = false;
            SelectPassive();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _gameData.IsAddPassive = true;
            JsonManager jsonManager = GenericSingleton<JsonManager>.Instance;
            jsonManager.WriteDataFile();
            while (jsonManager.IsWriting == true || jsonManager.CheckDataFiles() == false)
            {
                if (jsonManager.IsWriting == false && jsonManager.CheckDataFiles() == true)
                    break;
            }
            GenericSingleton<DoorManager>.Instance.ClearDoors();
            SceneManager.LoadScene($"{(EWorldType)_gameData.MapStage}");
        }
    }

    public void AddKillEnemy()
    {
        _gameData.KillEnemy++;
    }

    public void StageUI()
    {
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage();
    }

    void AddPlayTime()
    {
        _gameData.PlayTime += Time.deltaTime;
    }

    public void SelectPassive()
    {
        SelectPassiveData selectPassiveData = DataSingleton<SelectPassiveData>.Instance;
        PassiveDataList dataList = DataSingleton<PassiveDataList>.Instance;
        GenericSingleton<UIManager>.Instance.SelectPassiveUI.gameObject.SetActive(true);

        List<int> passiveIndex = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int random;
            do
            {
                random = Random.Range(0, dataList.UIDataList.Count);
            }
            while (passiveIndex.Contains(random) || selectPassiveData.PassiveList.Contains(dataList.UIDataList[random]));

            passiveIndex.Add(random);
            GenericSingleton<UIManager>.Instance.SelectPassiveUI.PassiveButton[i].Passive = dataList.UIDataList[random];
            GenericSingleton<UIManager>.Instance.SelectPassiveUI.PassiveButton[i].Init();
        }
    }

    public void ResetData()
    {
        _gameData.MapStage = 1;
        _gameData.LevelStage = 1;
        _gameData.KillEnemy = 0;
        _gameData.PlayTime = 0f;
        _gameData.IsAddPassive = true;
        IsClear = false;
    }
}