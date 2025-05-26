using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ChoiceCharacterUI : MonoBehaviour
{
    [SerializeField] List<Sprite> _characterImage;
    [SerializeField] UnlockPanel _unlockPanel;
    [SerializeField] LockPanel _lockPanel;

    PlayerStatManager _playerStatManager;
    PlayerInfoData _playerInfoData;
    PlayerLevelData _playerLevelData;

    int _index = 0;

    public Sprite CharacterImage { get => _characterImage[_index]; }
    public int Index { get => _index; }

    void Start()
    {
        _playerStatManager = GenericSingleton<PlayerStatManager>.Instance;
        Init();
    }

    void Init()
    {
        _index = 0;
        _playerInfoData = _playerStatManager.StatData[_index];
        _playerLevelData = _playerStatManager.LevelDatas[_index];
        CheckUnlock();
    }

    public void ChangePage(int direction)
    {
        _index += direction;
        _playerInfoData = _playerStatManager.StatData[_index];
        _playerLevelData = _playerStatManager.LevelDatas[_index];
        CheckUnlock();
    }

    void CheckUnlock()
    {
        if(_playerLevelData.IsUnlock)
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
}
