using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ChoiceCharacterUI : MonoBehaviour
{
    [SerializeField] List<Sprite> _characterImage;
    [SerializeField] Text _characterName;
    [SerializeField] Text _gemText;
    [SerializeField] UnlockPanel _unlockPanel;
    [SerializeField] LockPanel _lockPanel;

    PlayerStatManager _playerStatManager;
    PlayerInfoData _playerInfoData;
    PlayerLevelData _playerLevelData;
    GemData _gemData;

    int _index = 0;

    public Sprite CharacterImage { get => _characterImage[_index]; }
    public int Index { get => _index; }

    void Start()
    {
        _playerStatManager = GenericSingleton<PlayerStatManager>.Instance;
        _gemData = DataSingleton<GemData>.Instance;
        ChangePage();
        ShowGem();
    }

    void CheckUnlock()
    {
        if (_playerLevelData.IsUnlock)
        {
            _lockPanel.gameObject.SetActive(false);
            _unlockPanel.gameObject.SetActive(true);
            _unlockPanel.ShowUI(_playerInfoData, _playerLevelData);
        }
        else
        {
            _unlockPanel.gameObject.SetActive(false);
            _lockPanel.gameObject.SetActive(true);
            _lockPanel.ShowUI(_playerInfoData, _playerLevelData);
        }
    }

    void ShowCharacterName()
    {
        _characterName.text = _playerInfoData.Name;
    }

    public void ChangePage(int direction = 0)
    {
        _index += direction;
        DataSingleton<PlayerData>.Instance.PlayerType = (EPlayerType)_index;
        _playerInfoData = _playerStatManager.StatData[_index];
        _playerLevelData = _playerStatManager.LevelDatas[_index];
        CheckUnlock();
        ShowCharacterName();
    }

    public void ShowGem()
    {
        _gemText.text = _gemData.Gem.ToString();
    }
}
