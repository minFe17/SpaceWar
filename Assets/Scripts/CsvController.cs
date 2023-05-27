using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Utils;

public class CsvController : MonoBehaviour
{
    // ΩÃ±€≈Ê
    string _playerDataFilePath;
    string _gameDataFilePath;
    string _passiveDataFilePath;

    public void ReadDataFile()
    {
        ReadPlayerData();
        ReadGameData();
        ReadPassiveData();
    }

    public void WriteDataFile()
    {
        WirtePlayerData();
        WirteGameData();
        WirtePassiveData();
    }

    void ReadPlayerData()
    {
        
    }

    void ReadGameData()
    {

    }

    void ReadPassiveData()
    {

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
        string fileName = "SavePlayerDataFile.csv";
        List<string[]> lists = new List<string[]>();
        _playerDataFilePath = Application.dataPath + "/Resources/Datas/" + fileName;

        string[] datas = new string[] { "MaxHp", "CurHp","ShotMode", "MaxBullet", "CurBullet", "BulletDamage", "MoveSpeed", "SplintSpeed", "Money", "BonusMoney", "UnlockBurstMode", "UnlockAutoMode", "HPUpByMoney", "Vampirism" };
        lists.Add(datas);
        lists.Add(PlayerDataToString());

        BaseWriteData(lists, _playerDataFilePath);

    }

    void WirteGameData()
    {
        string fileName = "SaveGameDataFile.csv";
        List<string[]> lists = new List<string[]>();
        _gameDataFilePath = Application.dataPath + "/Resources/Datas/" + fileName;

        string[] datas = new string[] { "MapStage", "LevelStage", "KillEnemy", "PlayTime", "IsAddPassive" };
        lists.Add(datas);
        lists.Add(GameDataToString());

        BaseWriteData(lists, _gameDataFilePath);
    }

    void WirtePassiveData()
    {
        List<PassiveBase> passiveData = GenericSingleton<PlayerDataManager>.Instance.Passive;
        string fileName = "PassiveDataFile.csv";
        List<string[]> lists = new List<string[]>();
        _passiveDataFilePath = Application.dataPath + "/Resources/Datas/" + fileName;

        string[] datas = new string[passiveData.Count];
        for (int i = 0; i < passiveData.Count; i++)
        {
            datas[i] = $"Passive{i + 1}";
        }
        lists.Add(datas);

        string[] value = new string[passiveData.Count];
        for (int i = 0; i < passiveData.Count; i++)
        {
            value[i] = passiveData[i].Name;
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