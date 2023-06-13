using UnityEngine;
using Utils;

public class MaxHPIncreasePotion : PotionBase
{
    public override void PotionEffect()
    {
        int random = Random.Range(1, 4);
        GenericSingleton<PlayerDataManager>.Instance.MaxHp += random;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowVendingMachineResult($"최대 HP +{random} 증가");
    }
}