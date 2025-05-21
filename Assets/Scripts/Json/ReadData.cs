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

    void ReadJsonData(string path, object dataClass)
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, dataClass);
    }

    void ReadPlayerData()
    {
        if (!_csvManager.CheckDataFile(_csvManager.PlayerDataFilePath))
            return;
        PlayerDataManager playerData = GenericSingleton<PlayerDataManager>.Instance;
        ReadJsonData(_csvManager.PlayerDataFilePath, playerData);
    }

    void ReadGameData()
    {
        if (!_csvManager.CheckDataFile(_csvManager.GameDataFilePath))
            return;
        GameData gamedata = DataSingleton<GameData>.Instance;
        ReadJsonData(_csvManager.GameDataFilePath, gamedata);
    }

    void ReadSavePassiveData()
    {
        if (!_csvManager.CheckDataFile(_csvManager.PassiveDataFilePath))
            return;
        SelectPassiveData passiveData = DataSingleton<SelectPassiveData>.Instance;
        ReadJsonData(_csvManager.PassiveDataFilePath, passiveData);
    }

    public void ReadSoundData()
    {
        SoundVolumnData volumnData = DataSingleton<SoundVolumnData>.Instance;
        if (!_csvManager.CheckDataFile(_csvManager.SoundDataFilePath))
        {
            volumnData.BgmSound = 0.5f;
            volumnData.SFXSound = 0.5f;
            return;
        }
        ReadJsonData(_csvManager.SoundDataFilePath, volumnData);
    }
}