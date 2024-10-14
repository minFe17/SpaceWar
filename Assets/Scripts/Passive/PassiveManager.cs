using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PassiveManager : MonoBehaviour
{
    // ╫л╠шео
    CsvManager _csvManager;
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

    async Task ReadData()
    {
        if (_csvManager == null)
            _csvManager = GenericSingleton<CsvManager>.Instance;
        await _csvManager.ReadPassiveData();
    }

    public async Task Init()
    {
        AddPassive();
        await ReadData();
    }

    public void RemovePassive(IPassive passive)
    {
        _passive.Remove(passive);
    }
}