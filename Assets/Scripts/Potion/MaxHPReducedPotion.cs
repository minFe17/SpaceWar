using UnityEngine;
using Utils;

public class MaxHPReducedPotion : PotionBase
{
    public override void PotionEffect()
    {
        // 최대 Hp감소
        int MaxHPReducedAmount = Random.Range(1, 3);
        int hp = GenericSingleton<PlayerDataManager>.Instance.MaxHp - MaxHPReducedAmount;
        if (hp <= 0)
            hp = 1;
        GenericSingleton<PlayerDataManager>.Instance.MaxHp = hp;
        if (GenericSingleton<PlayerDataManager>.Instance.CurHp > hp)
            GenericSingleton<PlayerDataManager>.Instance.CurHp = hp;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}