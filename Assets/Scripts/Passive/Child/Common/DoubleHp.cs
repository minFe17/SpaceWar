using Utils;

public class DoubleHp : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        PlayerData playerData = DataSingleton<PlayerData>.Instance;
        playerData.MaxHp *= 2;
        playerData.CurHp *= 2;

        UIManager uiManager = GenericSingleton<UIManager>.Instance;
        uiManager.IngameUI.ShowHp();
    }
}