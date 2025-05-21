using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class ReadData : MonoBehaviour
{
    AddressableManager _addressableManager;
    PassiveManager _passiveManager;
    CsvManager _csvManager;

    PassiveData _passiveData;

    public void Init(CsvManager csvManager)
    {
        _csvManager = csvManager;
    }

    public void ReadDataFile()
    {
        ReadPlayerData();
        ReadGameData();
        ReadSavePassiveData();
    }

    void BaseReadOnlyData(out string[] data, TextAsset readData)
    {
        using (StringReader stringReader = new StringReader(readData.text))
        {
            string baseData = stringReader.ReadToEnd();
            data = baseData.Split("\r\n");
        }
        if (data.Length < 2)
            data = null;
    }

    string[] BaseReadData(string dataFilePath)
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

    public async Task ReadPassiveInfoData()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
        if (_passiveManager == null)
            _passiveManager = GenericSingleton<PassiveManager>.Instance;

        TextAsset passiveDatas = await _addressableManager.GetAddressableAsset<TextAsset>("PassiveData");
        string[] data;
        BaseReadOnlyData(out data, passiveDatas);
        for (int i = 1; i < data.Length; i++)
        {
            string[] values = data[i].Split(",");
            if (values.Length == 0 || string.IsNullOrEmpty(values[0]))
                continue;

            _passiveData.Index = int.Parse(values[0]);
            _passiveData.Name = values[1];
            _passiveData.Info = values[2];
            _passiveData.ImageName = values[3];

            _passiveManager.Passive[i - 1].SetPassiveData(_passiveData);
        }
    }

    void ReadPlayerData()
    {
        if (!_csvManager.CheckDataFile(_csvManager.PlayerDataFilePath))
            return;

        PlayerDataManager playerData = GenericSingleton<PlayerDataManager>.Instance;
        string[] value = BaseReadData(_csvManager.PlayerDataFilePath);
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
        if (!_csvManager.CheckDataFile(_csvManager.GameDataFilePath))
            return;

        GameManager gameData = GenericSingleton<GameManager>.Instance;
        string[] value = BaseReadData(_csvManager.GameDataFilePath);
        if (value == null)
            return;

        gameData.MapStage = int.Parse(value[0]);
        gameData.LevelStage = int.Parse(value[1]);
        gameData.KillEnemy = int.Parse(value[2]);
        gameData.PlayTime = float.Parse(value[3]);
        gameData.IsAddPassive = bool.Parse(value[4]);
    }

    void ReadSavePassiveData()
    {
        if (!_csvManager.CheckDataFile(_csvManager.PassiveDataFilePath))
            return;

        List<string> passive = GenericSingleton<PlayerDataManager>.Instance.Passive;
        string[] value = BaseReadData(_csvManager.PassiveDataFilePath);
        if (value == null)
            return;

        passive.Clear();
        for (int i = 0; i < value.Length; i++)
        {
            passive.Add(value[i]);
        }
    }

    public bool ReadSoundData()
    {
        if (!_csvManager.CheckDataFile(_csvManager.SoundDataFilePath))
            return false;

        string[] value = BaseReadData(_csvManager.SoundDataFilePath);
        if(value == null)
            return false;

        GenericSingleton<SoundManager>.Instance.BgmSound = float.Parse(value[0]);
        GenericSingleton<SoundManager>.Instance.SFXSound = float.Parse(value[1]);

        return true;
    }
}