using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class ReadData : MonoBehaviour
{
    List<string> _playerInfoDataPath = new List<string>();
    List<string> _passiveDataPath = new List<string>();
    AddressableManager _addressableManager;
    PassiveManager _passiveManager;
    JsonManager _jsonManager;

    public void Init(JsonManager jsonManager)
    {
        _jsonManager = jsonManager;
        if(_playerInfoDataPath.Count == 0)
        {
            _playerInfoDataPath.Add("SoldierInfoData.json");
            _playerInfoDataPath.Add("WitchInfoData.json.json");
        }
        if(_passiveDataPath.Count == 0)
        {
            _passiveDataPath.Add("SoldierPassiveData.json");
            _passiveDataPath.Add("WitchPassiveData.json");
        }
    }

    public void ReadDataFile()
    {
        ReadPlayerData();
        ReadGameData();
        ReadSavePassiveData();
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

    async Task ReadPlayerInfoData()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        List<PlayerInfoData> infoData = GenericSingleton<PlayerStatManager>.Instance.StatData;
        for(int i=0; i<infoData.Count; i++)
        {
            TextAsset temp = await _addressableManager.GetAddressableAsset<TextAsset>(_playerInfoDataPath[i]);
            string data = temp.text;
            JsonUtility.FromJsonOverwrite(data, infoData);
        }
    }

    void ReadPlayerLevelData()
    {
        List<PlayerLevelData> levelData = GenericSingleton<PlayerStatManager>.Instance.LevelDatas;

        for (int i = 0; i < _jsonManager.PlayerLevelDataFilePath.Count; i++)
        {
            if (!_jsonManager.CheckDataFile(_jsonManager.GameDataFilePath))
                GenericSingleton<PlayerStatManager>.Instance.SetLock(i);

            ReadJsonData(_jsonManager.GameDataFilePath, levelData[i]);
        }
    }

    async Task CharacterPassiveInfoData()
    {
        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;
        PlayerPassiveData dataList = _passiveManager.PlayerPassiveDatas[(int)playerType];
        TextAsset passiveData = await _addressableManager.GetAddressableAsset<TextAsset>(_passiveDataPath[(int)playerType]);
        string data = passiveData.text;
        JsonUtility.FromJsonOverwrite(data, dataList);
    }

    public async Task ReadPassiveInfoData()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
        if (_passiveManager == null)
            _passiveManager = GenericSingleton<PassiveManager>.Instance;

        TextAsset passiveDatas = await _addressableManager.GetAddressableAsset<TextAsset>("PassiveData.json");
        CommonPassiveData dataList = DataSingleton<CommonPassiveData>.Instance;
        string data = passiveDatas.text;
        JsonUtility.FromJsonOverwrite(data, dataList);
        await CharacterPassiveInfoData();
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

    public async Task ReadPlayerStatData()
    {
        await ReadPlayerInfoData();
        ReadPlayerLevelData();
    }
}