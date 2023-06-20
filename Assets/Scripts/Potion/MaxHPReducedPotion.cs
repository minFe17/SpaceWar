using UnityEngine;
using Utils;

public class MaxHPReducedPotion : PotionBase
{
    public override void PotionEffect()
    {
        int MaxHPReducedAmount = Random.Range(1, 3);
        int maxHp = GenericSingleton<PlayerDataManager>.Instance.MaxHp;
        int hp = maxHp - MaxHPReducedAmount;
        if (hp <= 0)
        {
            MaxHPReducedAmount = maxHp - 1;
            hp = 1;
        }
        GenericSingleton<PlayerDataManager>.Instance.MaxHp = hp;
        if (GenericSingleton<PlayerDataManager>.Instance.CurHp > hp)
            GenericSingleton<PlayerDataManager>.Instance.CurHp = hp;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowVendingMachineResult($"최대 HP -{MaxHPReducedAmount} 감소");
        AudioClip damageSound = Resources.Load("Prefabs/SoundClip/PotionDamage") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(damageSound);
    }
}