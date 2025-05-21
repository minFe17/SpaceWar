using UnityEngine;
using Utils;

public class PlayerDataManager : MonoBehaviour
{
    // ╫л╠шео
    PlayerData _playerData = DataSingleton<PlayerData>.Instance;
    SelectPassiveData _passiveData = DataSingleton<SelectPassiveData>.Instance;
    
    public Player Player { get; set; }

    public void SettingPlayerData()
    {
        CsvManager csvManager = GenericSingleton<CsvManager>.Instance;
        if (csvManager.CheckDataFiles())
            csvManager.ReadDataFile();
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