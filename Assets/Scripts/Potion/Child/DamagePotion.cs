public class DamagePotion : PotionBase, IPotion
{
    int _curHp;
    int _hp;
    int _damageAmount;

    void IPotion.PotionEffect()
    {
        CalculatePotion();
        ShowResult();
    }

    void SetHp()
    {
        if (_playerDataManager == null)
            SetManager();
        _curHp = _playerDataManager.CurHp;
        _damageAmount = _curHp / 3;
    }

    void CalculatePotion()
    {
        SetHp();
        if (_damageAmount == 0)
        {
            _damageAmount = 1;
        }
        _hp = _curHp - _damageAmount;

        if (_hp <= 0)
        {
            _hp = 1;
            _damageAmount = _curHp - _hp;
        }
    }

    void ShowResult()
    {
        _playerDataManager.CurHp = _hp;
        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"현재 HP -{_damageAmount} 감소");
        _audiolipManager.PlaySFX(ESFXAudioType.Damage);
    }
}