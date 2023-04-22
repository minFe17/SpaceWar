using UnityEngine;
using Utils;

public class MaxHPIncreasePotion : PotionBase
{
    public override void PotionEffect()
    {
        // 최대 Hp증가
        int random = Random.Range(1, 4);
        GenericSingleton<PlayerDataManager>.Instance.MaxHp += random;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}