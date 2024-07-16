public class RecoveryPotion : PotionBase
{
    public override void PotionEffect()
    {
        int maxHp = _playerDataManager.MaxHp;
        int curHp = _playerDataManager.CurHp;
        int recoveryAmount = maxHp / 3;
        int hp = curHp + recoveryAmount;

        if (hp > maxHp)
        {
            recoveryAmount = maxHp - curHp;
            hp = maxHp;
        }
        _playerDataManager.CurHp = hp;

        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"현재 HP +{recoveryAmount} 회복");
        _audiolipManager.PlaySFX(ESFXAudioType.Heal);
    }
}