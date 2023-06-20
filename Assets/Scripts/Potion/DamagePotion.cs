using UnityEngine;
using Utils;

public class DamagePotion : PotionBase
{
    public override void PotionEffect()
    {
        int curHp = GenericSingleton<PlayerDataManager>.Instance.CurHp;
        int damageAmount = curHp / 3;

        if (damageAmount == 0)
        {
            damageAmount = 1;
        }
        int hp = curHp - damageAmount;

        if (hp <= 0)
        {
            damageAmount = curHp - 1;
            hp = 1;
        }
        GenericSingleton<PlayerDataManager>.Instance.CurHp = hp;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowVendingMachineResult($"현재 HP -{damageAmount} 감소");
        AudioClip damageSound = Resources.Load("Prefabs/SoundClip/PotionDamage") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(damageSound);
    }
}