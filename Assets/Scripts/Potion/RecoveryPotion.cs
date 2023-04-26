using Utils;

public class RecoveryPotion : PotionBase
{
    public override void PotionEffect()
    {
        int recoveryAmount = GenericSingleton<PlayerDataManager>.Instance.MaxHp / 3;
        int hp = GenericSingleton<PlayerDataManager>.Instance.CurHp + recoveryAmount;
        if (hp > GenericSingleton<PlayerDataManager>.Instance.MaxHp)
            hp = GenericSingleton<PlayerDataManager>.Instance.MaxHp;
        GenericSingleton<PlayerDataManager>.Instance.CurHp = hp;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}