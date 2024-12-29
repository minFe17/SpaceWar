using UnityEngine;

public class MaxHPIncreasePotion : PotionBase, IPotion
{
    int _minHpIncrease = 1;
    int _maxHpIncrease = 6;
    int _randomHpIncrease;


    void IPotion.PotionEffect()
    {
        CalculatePotion();
        ShowResult();
    }

    void CalculatePotion()
    {
        _randomHpIncrease = Random.Range(_minHpIncrease, _maxHpIncrease);
        if (_playerDataManager == null)
            SetManager();
        _playerDataManager.MaxHp += _randomHpIncrease;
    }

    void ShowResult()
    {
        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"최대 HP +{_randomHpIncrease} 증가");
        _audiolipManager.PlaySFX(ESFXAudioType.Heal);
    }
}