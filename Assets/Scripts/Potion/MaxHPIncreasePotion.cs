using UnityEngine;

public class MaxHPIncreasePotion : PotionBase
{
    public override void PotionEffect()
    {
        int random = Random.Range(1, 4);
        _playerDataManager.MaxHp += random;

        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowVendingMachineResult($"최대 HP +{random} 증가");
        _audiolipManager.PlaySFX(ESFXAudioType.Heal);
    }
}