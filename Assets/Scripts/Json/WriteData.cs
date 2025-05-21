using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Utils;

public class WriteData : MonoBehaviour
{
    CsvManager _csvManager;

    public void Init(CsvManager csvManager)
    {
        _csvManager = csvManager;
    }

    public void WriteDataFile()
    {
        _csvManager.IsWriting = true;
        _csvManager.DestroyDataFiles();
        WirtePlayerData();
        WirteGameData();
        WirtePassiveData();

        _csvManager.IsWriting = false;
    }

    void WriteJsonDataBase(object data, string filePath)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    void WirtePlayerData()
    {
        PlayerData playerData = DataSingleton<PlayerData>.Instance;
        WriteJsonDataBase(playerData, _csvManager.PlayerDataFilePath);
    }

    void WirteGameData()
    {
        GameData data = DataSingleton<GameData>.Instance;
        WriteJsonDataBase(data, _csvManager.GameDataFilePath);
    }

    void WirtePassiveData()
    {
        SelectPassiveData passiveData = DataSingleton<SelectPassiveData>.Instance;
        WriteJsonDataBase(passiveData, _csvManager.PassiveDataFilePath);
    }

    public void WriteSound()
    {
        SoundVolumnData data = DataSingleton<SoundVolumnData>.Instance;
        WriteJsonDataBase(data, _csvManager.SoundDataFilePath);
    }
}