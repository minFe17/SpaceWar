using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    // �̱���
    List<IPotion> _potions = new List<IPotion>();

    public void ApplyPotion()
    {
        if (_potions.Count == 0)
            AddPotion();
        int random = Random.Range(0, _potions.Count);
        _potions[random].PotionEffect();
    }

    public void AddPotion()
    {
        _potions.Add(new RecoveryPotion());
        _potions.Add(new DamagePotion());
        _potions.Add(new MaxHPIncreasePotion());
        _potions.Add(new MaxHPReducedPotion());
    }
}