using UnityEngine;

public class MaxHPReducedPotion : PotionBase
{
    public override void PotionEffect()
    {
        int MaxHPReducedAmount = Random.Range(1, 3);
        int maxHp = _playerDataManager.MaxHp;
        int hp = maxHp - MaxHPReducedAmount;
        if (hp <= 0)
        {
            MaxHPReducedAmount = maxHp - 1;
            hp = 1;
        }
        _playerDataManager.MaxHp = hp;
        if (_playerDataManager.CurHp > hp)
            _playerDataManager.CurHp = hp;

        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"최대 HP -{MaxHPReducedAmount} 감소");
        _audiolipManager.PlaySFX(ESFXAudioType.Damage);
    }
}