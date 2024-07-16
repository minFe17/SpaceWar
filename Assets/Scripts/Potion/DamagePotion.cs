public class DamagePotion : PotionBase
{
    public override void PotionEffect()
    {
        int curHp = _playerDataManager.CurHp;
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
        _playerDataManager.CurHp = hp;

        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"현재 HP -{damageAmount} 감소");
        _audiolipManager.PlaySFX(ESFXAudioType.Damage);
    }
}