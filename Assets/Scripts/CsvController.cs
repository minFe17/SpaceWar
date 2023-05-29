using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Utils;

public class CsvController : MonoBehaviour
{
    // ½Ì±ÛÅæ
    string _playerDataFilePath;
    string _gameDataFilePath;
    string _passiveDataFilePath;
    
    bool _nullDataFile;

    public string PlayerDataFilePath { get => _playerDataFilePath; }
    public string GameDataFilePath {  get => _gameDataFilePath; }
    public string PassiveDataFilePath {  get => _passiveDataFilePath; }

    void DataFilePath()
    {
        string playerDataFileName = "SavePlayerDataFile.csv";
        _playerDataFilePath = Application.dataPath + "/Resources/Datas/" + playerDataFileName;

        string gameDataFileName = "SaveGameDataFile.csv";
        _gameDataFilePath = Application.dataPath + "/Resources/Datas/" + gameDataFileName;

        string passiveDataFileName = "PassiveDataFile.csv";
        _passiveDataFilePath = Application.dataPath + "/Resources/Datas/" + passiveDataFileName;
    }

    public bool ReadDataFile()
    {
        DataFilePath();
        ReadPlayerData();
        ReadGameData();
        ReadPassiveData();

        if (_nullDataFile)
            return false;
        else
            return true;
    }

    public bool WriteDataFile()
    {
        DataFilePath();
        WirtePlayerData();
        WirteGameData();
        WirtePassiveData();

        return true;
    }

    string[] BaseReadData(string dataFilePath)
    {
        if (File.Exists(dataFilePath) == false)
        {
            Debug.Log(dataFilePath);
            _nullDataFile = true;
            return null;
        }
        else
        {
            string source;
            using (StreamReader sr = new StreamReader(dataFilePath))
            {
                string[] lines;
                source = sr.ReadToEnd();
                lines = Regex.Split(source, @"\r\n|\n\r|\n|\r");
                string[] header = Regex.Split(lines[0], ",");
                string[] value = Regex.Split(lines[1], ",");

                return value;
            }
        }
    }

    void ReadPlayerData()
    {
        PlayerDataManager playerData = GenericSingleton<PlayerDataManager>.Instance;
        string[] value = BaseReadData(_playerDataFilePath);
        if (value == null)
            return;

        playerData.MaxHp = int.Parse(value[0]);
        playerData.CurHp = int.Parse(value[1]);
        playerData.ShotMode = (EShotModeType)Enum.Parse(typeof(EShotModeType), value[2]);
        playerData.MaxBullet = int.Parse(value[3]);
        playerData.CurBullet = int.Parse(value[4]);
        playerData.BulletDamage = int.Parse(value[5]);
        playerData.MoveSpeed = float.Parse(value[6]);
        playerData.SplintSpeed = float.Parse(value[7]);
        playerData.Money = int.Parse(value[8]);
        playerData.BonusMoney = int.Parse(value[9]);
        playerData.UnlockBurstMode = bool.Parse(value[10]);
        playerData.UnlockAutoMode = bool.Parse(value[11]);
        playerData.HPUpByMoney = bool.Parse(value[12]);
        playerData.Vampirism = bool.Parse(value[13]);
    }

    void ReadGameData()
    {
        GameManager gameData = GenericSingleton<GameManager>.Instance;
        string[] value = BaseReadData(_gameDataFilePath);
        if (value == null)
            return;

        gameData.MapStage = int.Parse(value[0]);
        gameData.LevelStage = int.Parse(value[1]);
        gameData.KillEnemy = int.Parse(value[2]);
        gameData.PlayTime = float.Parse(value[3]);
        gameData.IsAddPassive = bool.Parse(value[4]);
    }

    void ReadPassiveData()
    {
        List<string> passive = GenericSingleton<PlayerDataManager>.Instance.Passive;
        string[] value = BaseReadData(_passiveDataFilePath);
        if (value == null)
            return;

        passive.Clear();
        for (int i = 0; i < passive.Count; i++)
        {
            passive.Add(value[i]);
        }
    }

    void BaseWriteData(List<string[]> lists, string dataFilePath)
    {
        string delimiter = ",";
        string[][] outputs = lists.ToArray();

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < outputs.Length; i++)
        {
            stringBuilder.AppendLine(string.Join(delimiter, outputs[i]));
        }
        using (StreamWriter outStream = File.CreateText(dataFilePath))
            outStream.Write(stringBuilder);
    }


    void WirtePlayerData()
    {
        List<string[]> lists = new List<string[]>();

        string[] datas = new string[] { "MaxHp", "CurHp", "ShotMode", "MaxBullet", "CurBullet", "BulletDamage", "MoveSpeed", "SplintSpeed", "Money", "BonusMoney", "UnlockBurstMode", "UnlockAutoMode", "HPUpByMoney", "Vampirism" };
        lists.Add(datas);
        lists.Add(PlayerDataToString());

        BaseWriteData(lists, _playerDataFilePath);
    }

    void WirteGameData()
    {
        List<string[]> lists = new List<string[]>();

        string[] datas = new string[] { "MapStage", "LevelStage", "KillEnemy", "PlayTime", "IsAddPassive" };
        lists.Add(datas);
        lists.Add(GameDataToString());

        BaseWriteData(lists, _gameDataFilePath);
    }

    void WirtePassiveData()
    {
        List<string> passiveData = GenericSingleton<PlayerDataManager>.Instance.Passive;
        List<string[]> lists = new List<string[]>();

        string[] datas = new string[passiveData.Count];
        for (int i = 0; i < passiveData.Count; i++)
        {
            datas[i] = $"Passive{i + 1}";
        }
        lists.Add(datas);

        string[] value = new string[passiveData.Count];
        for (int i = 0; i < passiveData.Count; i++)
        {
            value[i] = passiveData[i];
        }
        lists.Add(value);

        BaseWriteData(lists, _passiveDataFilePath);
    }

    string[] PlayerDataToString()
    {
        PlayerDataManager data = GenericSingleton<PlayerDataManager>.Instance;
        List<string> value = new List<string>();
        value.Add(data.MaxHp.ToString());
        value.Add(data.CurHp.ToString());
        value.Add(data.ShotMode.ToString());
        value.Add(data.MaxBullet.ToString());
        value.Add(data.CurBullet.ToString());
        value.Add(data.BulletDamage.ToString());
        value.Add(data.MoveSpeed.ToString());
        value.Add(data.SplintSpeed.ToString());
        value.Add(data.Money.ToString());
        value.Add(data.BonusMoney.ToString());
        value.Add(data.UnlockBurstMode.ToString());
        value.Add(data.UnlockAutoMode.ToString());
        value.Add(data.HPUpByMoney.ToString());
        value.Add(data.Vampirism.ToString());

        return value.ToArray();
    }

    string[] GameDataToString()
    {
        GameManager data = GenericSingleton<GameManager>.Instance;
        List<string> value = new List<string>();
        value.Add(data.MapStage.ToString());
        value.Add(data.LevelStage.ToString());
        value.Add(data.KillEnemy.ToString());
        value.Add(data.PlayTime.ToString());
        value.Add(data.IsAddPassive.ToString());

        return value.ToArray();
    }
}