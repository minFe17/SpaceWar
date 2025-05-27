using UnityEngine;
using UnityEngine.UI;

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
        _stringBuilder.Append("��� ����");
        _unlockText.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
        _unlockCostText.text = $"��� : {_playerInfoData.UnlockCost}";
    }

    public void Unlock()
    {

    }
}
