public class RecoveryPotion : PotionBase, IPotion
{
    int _maxHp;
    int _curHp;
    int _hp;
    int _recoveryAmount;

    void IPotion.PotionEffect()
    {
        CalculatePotion();
        ShowResult();
    }

    void SetHp()
    {
        if (_playerData == null)
            SetManager();
        _maxHp = _playerData.MaxHp;
        _curHp = _playerData.CurHp;
        _recoveryAmount = _maxHp / 3;
        _hp = _curHp + _recoveryAmount;
    }

    void CalculatePotion()
    {
        SetHp();
        if (_maxHp < _hp)
        {
            _recoveryAmount = _maxHp - _curHp;
            _hp = _maxHp;
        }
        _playerData.CurHp = _hp;
    }

    void ShowResult()
    {
        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"현재 HP +{_recoveryAmount} 회복");
        _audiolipManager.PlaySFX(ESFXAudioType.Heal);
    }
}