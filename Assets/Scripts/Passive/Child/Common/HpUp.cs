using Utils;

public class HpUp : IPassiveEffect
{
    int _hpAmount = 10;

    void IPassiveEffect.AddPassive()
    {
        PlayerData playerData = DataSingleton<PlayerData>.Instance;
        playerData.MaxHp += _hpAmount;
        playerData.CurHp += _hpAmount;

        UIManager uiManager = GenericSingleton<UIManager>.Instance;
        uiManager.IngameUI.ShowHp();
    }
}