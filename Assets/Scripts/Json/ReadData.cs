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

    public async Task ReadPassiveInfoData()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
        if (_passiveManager == null)
            _passiveManager = GenericSingleton<PassiveManager>.Instance;

        TextAsset passiveDatas = await _addressableManager.GetAddressableAsset<TextAsset>("PassiveData.json");
        PassiveDataList dataList = DataSingleton<PassiveDataList>.Instance;
        string data = passiveDatas.text;
        JsonUtility.FromJsonOverwrite(data, dataList);
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