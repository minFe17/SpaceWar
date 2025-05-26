using UnityEngine;

public class LockPanel : MonoBehaviour
{
    PlayerInfoData _playerInfoData;
    PlayerLevelData _playerLevelData;

    public void ShowUI(PlayerInfoData playerInfoData, PlayerLevelData playerLevelData)
    {
        _playerInfoData = playerInfoData;
        _playerLevelData = playerLevelData;
    }
}
