using UnityEngine;
using UnityEngine.UI;
using Utils;

public class LockPanel : CharacterPanelBase
{
    [SerializeField] Text _unlockText;
    [SerializeField] Text _unlockCostText;

    public override void ShowUI(PlayerInfoData playerInfoData, PlayerLevelData playerLevelData)
    {
        base.ShowUI(playerInfoData, playerLevelData);
        ShowUnlockText();
    }

    void ShowUnlockText()
    {
        _stringBuilder.Append($"{_playerInfoData.Name}\n");
        _stringBuilder.Append("잠금 해제");
        _unlockText.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
        _unlockCostText.text = $"비용 : {_playerInfoData.UnlockCost}";
    }

    public void Unlock()
    {
        GemData gemData = DataSingleton<GemData>.Instance;
        if (_playerInfoData.UnlockCost > gemData.Gem)
            return;
        _playerLevelData.IsUnlock = true;
        _parentPanel.ChangePage();
        gemData.UseGem(_playerInfoData.UnlockCost);
        _parentPanel.ShowGem();
        GenericSingleton<JsonManager>.Instance.WrtiePlayerLevelDataFile();
    }
}