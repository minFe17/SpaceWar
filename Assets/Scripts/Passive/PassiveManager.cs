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
            if (_passive == null)
                AddPassive();
            return _passive;
        }   
    }

    void AddPassive()
    {
        _passive.Add(new HPUp());
    }
}
