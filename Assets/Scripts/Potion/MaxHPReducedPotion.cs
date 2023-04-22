using UnityEngine;
using Utils;

public class MaxHPReducedPotion : PotionBase
{
    public override void PotionEffect()
    {
        // 최대 Hp감소
        int random = Random.Range(1, 3);
        GenericSingleton<PlayerDataManager>.Instance.MaxHp -= random;
        if (GenericSingleton<PlayerDataManager>.Instance.CurHp > GenericSingleton<PlayerDataManager>.Instance.MaxHp)
            GenericSingleton<PlayerDataManager>.Instance.CurHp = GenericSingleton<PlayerDataManager>.Instance.MaxHp;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}