using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : MonoBehaviour
{
    // ╫л╠шео
    List<IPassive> _passive = new List<IPassive>();
    public List<IPassive> Passive 
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
        _passive.Add(new HpUp());
        _passive.Add(new DamageUp());
        _passive.Add(new SpeedUp());
        _passive.Add(new BulletUp());
        _passive.Add(new UnlockBurstMode());
        _passive.Add(new UnlockAutoMode());
        _passive.Add(new GetMoneyUp());
        _passive.Add(new DoubleHp());
        _passive.Add(new HpUpByMoney());
        _passive.Add(new Vampirism());
    }

    public void RemovePassive(IPassive passive)
    {
        _passive.Remove(passive);
    }
}