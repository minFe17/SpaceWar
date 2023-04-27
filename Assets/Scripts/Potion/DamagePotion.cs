using Utils;

public class DamagePotion : PotionBase
{
    public override void PotionEffect()
    {
        int damageAmount = GenericSingleton<PlayerDataManager>.Instance.CurHp / 3;
        if (damageAmount == 0)
            damageAmount = 1;
        int hp = GenericSingleton<PlayerDataManager>.Instance.CurHp - damageAmount;
        if (hp <= 0)
            hp = 1;
        GenericSingleton<PlayerDataManager>.Instance.CurHp = hp;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}