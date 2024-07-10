using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    List<PassiveBase> _passive = new List<PassiveBase>();
    public List<PassiveBase> Passive 
    { 
        get
        {
            if (_passive.Count == 0)
                AddPassive();
            return _passive;
        }   
    }

    void AddPassive()
    {
        _passive.Add(new HPUp());
        _passive.Add(new DamageUp());
        _passive.Add(new SpeedUp());
        _passive.Add(new BulletUp());
        _passive.Add(new UnlockBurstMode());
        _passive.Add(new UnlockAutoMode());
        _passive.Add(new GetMoneyUp());
        _passive.Add(new DoubleHp());
        _passive.Add(new HPUpByMoney());
        _passive.Add(new Vampirism());
    }

    public void RemovePassive(PassiveBase passive)
    {
        _passive.Remove(passive);
    }
}