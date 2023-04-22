using Utils;

public class DamagePotion : PotionBase
{
    public override void PotionEffect()
    {
        int damageAmount = GenericSingleton<PlayerDataManager>.Instance.MaxHp / 10;
        if (damageAmount == 0)
            damageAmount = 1;
        GenericSingleton<PlayerDataManager>.Instance.CurHp -= damageAmount;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}