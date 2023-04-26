using UnityEngine;
using Utils;

public class MaxHPReducedPotion : PotionBase
{
    public override void PotionEffect()
    {
        // 최대 Hp감소
        int MaxHPReducedAmount = Random.Range(1, 3);
        if (GenericSingleton<PlayerDataManager>.Instance.MaxHp - MaxHPReducedAmount == 0)
            MaxHPReducedAmount--;
        GenericSingleton<PlayerDataManager>.Instance.MaxHp -= MaxHPReducedAmount;

        if (GenericSingleton<PlayerDataManager>.Instance.CurHp > GenericSingleton<PlayerDataManager>.Instance.MaxHp)
            GenericSingleton<PlayerDataManager>.Instance.CurHp = GenericSingleton<PlayerDataManager>.Instance.MaxHp;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}