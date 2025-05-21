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

        BaseWriteData(lists, _csvManager.PlayerDataFilePath);
    }

    void WirteGameData()
    {
        List<string[]> lists = new List<string[]>();

        string[] datas = new string[] { "MapStage", "LevelStage", "KillEnemy", "PlayTime", "IsAddPassive" };
        lists.Add(datas);
        lists.Add(GameDataToString());

        BaseWriteData(lists, _csvManager.GameDataFilePath);
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

        BaseWriteData(lists, _csvManager.PassiveDataFilePath);
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

    public void WriteSound()
    {
        SoundVolumnData data = DataSingleton<SoundVolumnData>.Instance;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_csvManager.SoundDataFilePath, json);
    }
}