using Utils;

public class DamagePotion : PotionBase
{
    public override void PotionEffect()
    {
        int damageAmount = GenericSingleton<PlayerDataManager>.Instance.CurHp / 3;
        if (damageAmount == 0)
            damageAmount = 1;
        if (GenericSingleton<PlayerDataManager>.Instance.CurHp - damageAmount == 0)
            damageAmount--;
        GenericSingleton<PlayerDataManager>.Instance.CurHp -= damageAmount;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}