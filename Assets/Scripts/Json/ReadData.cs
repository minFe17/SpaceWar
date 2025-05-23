using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class ReadData : MonoBehaviour
{
    AddressableManager _addressableManager;
    PassiveManager _passiveManager;
    JsonManager _jsonManager;

    public void Init(JsonManager jsonManager)
    {
        _jsonManager = jsonManager;
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
        if (!_jsonManager.CheckDataFile(_jsonManager.PlayerDataFilePath))
            return;
        PlayerDataManager playerData = GenericSingleton<PlayerDataManager>.Instance;
        ReadJsonData(_jsonManager.PlayerDataFilePath, playerData);
    }

    void ReadGameData()
    {
        if (!_jsonManager.CheckDataFile(_jsonManager.GameDataFilePath))
            return;
        GameData gamedata = DataSingleton<GameData>.Instance;
        ReadJsonData(_jsonManager.GameDataFilePath, gamedata);
    }

    void ReadSavePassiveData()
    {
        if (!_jsonManager.CheckDataFile(_jsonManager.PassiveDataFilePath))
            return;
        SelectPassiveData passiveData = DataSingleton<SelectPassiveData>.Instance;
        ReadJsonData(_jsonManager.PassiveDataFilePath, passiveData);
    }

    public void ReadSoundData()
    {
        SoundVolumnData volumnData = DataSingleton<SoundVolumnData>.Instance;
        if (!_jsonManager.CheckDataFile(_jsonManager.SoundDataFilePath))
        {
            volumnData.BgmSound = 0.5f;
            volumnData.SFXSound = 0.5f;
            return;
        }
        ReadJsonData(_jsonManager.SoundDataFilePath, volumnData);
    }
}