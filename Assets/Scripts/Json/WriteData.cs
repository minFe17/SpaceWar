using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;

public class WriteData : MonoBehaviour
{
    JsonManager _jsonManager;

    public void Init(JsonManager jsonManager)
    {
        _jsonManager = jsonManager;
    }

    public void WriteDataFile()
    {
        _jsonManager.IsWriting = true;
        _jsonManager.DestroyDataFiles();
        WirtePlayerData();
        WirteGameData();
        WirtePassiveData();

        _jsonManager.IsWriting = false;
    }

    void WriteJsonDataBase(object data, string filePath)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    void WirtePlayerData()
    {
        PlayerData playerData = DataSingleton<PlayerData>.Instance;
        WriteJsonDataBase(playerData, _jsonManager.PlayerDataFilePath);
    }

    void WirteGameData()
    {
        GameData data = DataSingleton<GameData>.Instance;
        WriteJsonDataBase(data, _jsonManager.GameDataFilePath);
    }

    void WirtePassiveData()
    {
        SelectPassiveData passiveData = DataSingleton<SelectPassiveData>.Instance;
        WriteJsonDataBase(passiveData, _jsonManager.PassiveDataFilePath);
    }

    public void WriteSound()
    {
        SoundVolumnData data = DataSingleton<SoundVolumnData>.Instance;
        WriteJsonDataBase(data, _jsonManager.SoundDataFilePath);
    }

    public void WritePlayerLevelData()
    {
        List<PlayerLevelData> levelData = GenericSingleton<PlayerStatManager>.Instance.LevelDatas;
        for(int i=0; i<levelData.Count; i++)
            WriteJsonDataBase(levelData[i], _jsonManager.PlayerLevelDataFilePath[i]);
    }
}