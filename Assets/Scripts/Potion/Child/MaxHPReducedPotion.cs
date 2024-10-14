using UnityEngine;

public class MaxHPReducedPotion : PotionBase, IPotion
{
    int _minHpReduced = 1;
    int _maxHpReduced = 4;
    int _maxHp;
    int _hp;
    int _randomHpReduced;

    void IPotion.PotionEffect()
    {
        CalculatePotion();
        ShowResult();
    }

    void SetHp()
    {
        _randomHpReduced = Random.Range(_minHpReduced, _maxHpReduced);
        _maxHp = _playerDataManager.MaxHp;
        _hp = _maxHp - _randomHpReduced;
    }

    void CalculatePotion()
    {
        SetHp();
        if (_hp <= 0)
        {
            _hp = 1;
            _randomHpReduced = _maxHp - _hp;
        }

        _playerDataManager.MaxHp = _hp;

        if (_playerDataManager.CurHp > _hp)
            _playerDataManager.CurHp = _hp;
    }

    void ShowResult()
    {
        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"최대 HP -{_randomHpReduced} 감소");
        _audiolipManager.PlaySFX(ESFXAudioType.Damage);
    }
}