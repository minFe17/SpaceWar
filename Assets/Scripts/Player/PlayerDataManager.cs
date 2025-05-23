using UnityEngine;
using Utils;

public class PlayerDataManager : MonoBehaviour
{
    // ╫л╠шео
    PlayerData _playerData = DataSingleton<PlayerData>.Instance;
    SelectPassiveData _passiveData = DataSingleton<SelectPassiveData>.Instance;
    
    public PlayerBase Player { get; set; }

    public void SettingPlayerData()
    {
        JsonManager jsonManager = GenericSingleton<JsonManager>.Instance;
        if (jsonManager.CheckDataFiles())
            jsonManager.ReadDataFile();
        else
            ResetData();
        GenericSingleton<AudioClipManager>.Instance.PlayBGM(EBGMAudioType.BGM);
    }

    void ResetData()
    {
        if (_passiveData.PassiveList.Count != 0)
            _passiveData.PassiveList.Clear();
        _playerData.ResetData();
        GenericSingleton<GameManager>.Instance.ResetData();
    }
}