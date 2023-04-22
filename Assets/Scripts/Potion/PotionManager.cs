using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    List<PotionBase> _potions = new List<PotionBase>();

    public void ApplyPotion()
    {
        Debug.Log(2);
        if (_potions.Count == 0)
            AddPotion();
        int random = Random.Range(0, _potions.Count);
        _potions[random].PotionEffect();
    }

    public void AddPotion()
    {
        Debug.Log(1);
        _potions.Add(new RecoveryPotion());
        _potions.Add(new DamagePotion());
        _potions.Add(new MaxHPIncreasePotion());
        _potions.Add(new MaxHPReducedPotion());

        for (int i = 0; i < _potions.Count; i++)
        {
            if (_potions[i] != null)
                Debug.Log(_potions[i]);
        }
    }
}
