using Utils;

public class RecoveryPotion : PotionBase
{
    public override void PotionEffect()
    {
        int maxHp = GenericSingleton<PlayerDataManager>.Instance.MaxHp;
        int curHp = GenericSingleton<PlayerDataManager>.Instance.CurHp;
        int recoveryAmount = maxHp / 3;
        int hp = curHp + recoveryAmount;

        if (hp > maxHp)
        {
            recoveryAmount = maxHp - curHp;
            hp = maxHp;
        }
        GenericSingleton<PlayerDataManager>.Instance.CurHp = hp;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowVendingMachineResult($"현재 HP +{recoveryAmount} 회복");
    }
}