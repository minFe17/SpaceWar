using Utils;

public class RecoveryPotion : PotionBase
{
    public override void PotionEffect()
    {
        int recoveryAmount = GenericSingleton<PlayerDataManager>.Instance.MaxHp / 3;
        GenericSingleton<PlayerDataManager>.Instance.CurHp = recoveryAmount;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}