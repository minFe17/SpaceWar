using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Utils;

public class CsvController : MonoBehaviour
{
    // ╫л╠шео
    bool _isWriting;

    string _playerDataFilePath;
    string _gameDataFilePath;
    string _passiveDataFilePath;

    public bool IsWriting { get => _isWriting; set => _isWriting = value; }

    public string PlayerDataFilePath
    {
        get
        {
            if (_playerDataFilePath == null)
            {
                string playerDataFileName = "SavePlayerDataFile.csv";
                _playerDataFilePath = Application.persistentDataPath + playerDataFileName;
            }
            return _playerDataFilePath;
        }
    }

    public string GameDataFilePath
    {
        get
        {
            if (_gameDataFilePath == null)
            {
                string gameDataFileName = "SaveGameDataFile.csv";
                _gameDataFilePath = Application.persistentDataPath + gameDataFileName;
            }
            return _gameDataFilePath;
        }
    }

    public string PassiveDataFilePath
    {
        get
        {
            if (_passiveDataFilePath == null)
            {
                string passiveDataFileName = "PassiveDataFile.csv";
                _passiveDataFilePath = Application.persistentDataPath + passiveDataFileName;
            }
            return _passiveDataFilePath;
        }
    }

    public void ReadDataFile()
    {
        ReadPlayerData();
        ReadGameData();
        ReadPassiveData();
    }

    public void WriteDataFile()
    {
        _isWriting = true;
        DestroyDataFile();
        WirtePlayerData();
        WirteGameData();
        WirtePassiveData();

        _isWriting = false;
    }

    public bool CheckDataFile()
    {
        if (File.Exists(PlayerDataFilePath) == false)
            return false;
        if (File.Exists(GameDataFilePath) == false)
            return false;
        if (File.Exists(PassiveDataFilePath) == false)
            return false;

        return true;
    }

    public void DestroyDataFile()
    {
        if (CheckDataFile())
        {
            File.Delete(PlayerDataFilePath);
            File.Delete(GameDataFilePath);
            File.Delete(PassiveDataFilePath);
        }
    }

    string[] BaseReadData(string dataFilePath)
    {
        if (!CheckDataFile())
            return null;
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
        string[] value = BaseReadData(PlayerDataFilePath);
        if (value == null)
            return;

        playerData.MaxHp = int.Parse(value[0]);
        playerData.CurHp = int.Parse(value[1]);
        playerData.ShootMode = (EShootModeType)Enum.Parse(typeof(EShootModeType), value[2]);
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
        string[] value = BaseReadData(GameDataFilePath);
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
        string[] value = BaseReadData(PassiveDataFilePath);
        if (value == null)
            return;

        passive.Clear();
        for (int i = 0; i < value.Length; i++)
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

        string[] datas = new string[] { "MaxHp", "CurHp", "ShootMode", "MaxBullet", "CurBullet", "BulletDamage", "MoveSpeed", "SplintSpeed", "Money", "BonusMoney", "UnlockBurstMode", "UnlockAutoMode", "HPUpByMoney", "Vampirism" };
        lists.Add(datas);
        lists.Add(PlayerDataToString());

        BaseWriteData(lists, PlayerDataFilePath);
    }

    void WirteGameData()
    {
        List<string[]> lists = new List<string[]>();

        string[] datas = new string[] { "MapStage", "LevelStage", "KillEnemy", "PlayTime", "IsAddPassive" };
        lists.Add(datas);
        lists.Add(GameDataToString());

        BaseWriteData(lists, GameDataFilePath);
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

        BaseWriteData(lists, PassiveDataFilePath);
    }

    string[] PlayerDataToString()
    {
        PlayerDataManager data = GenericSingleton<PlayerDataManager>.Instance;
        List<string> value = new List<string>
        {
            data.MaxHp.ToString(),
            data.CurHp.ToString(),
            data.ShootMode.ToString(),
            data.MaxBullet.ToString(),
            data.CurBullet.ToString(),
            data.BulletDamage.ToString(),
            data.MoveSpeed.ToString(),
            data.SplintSpeed.ToString(),
            data.Money.ToString(),
            data.BonusMoney.ToString(),
            data.UnlockBurstMode.ToString(),
            data.UnlockAutoMode.ToString(),
            data.HPUpByMoney.ToString(),
            data.Vampirism.ToString()
        };

        return value.ToArray();
    }

    string[] GameDataToString()
    {
        GameManager data = GenericSingleton<GameManager>.Instance;
        List<string> value = new List<string>
        {
            data.MapStage.ToString(),
            data.LevelStage.ToString(),
            data.KillEnemy.ToString(),
            data.PlayTime.ToString(),
            data.IsAddPassive.ToString()
        };

        return value.ToArray();
    }
}