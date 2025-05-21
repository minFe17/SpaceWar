using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SelectPassiveData
{
    // ������ �̱���
    [SerializeField] List<IPassive> _passiveList = new List<IPassive>();

    public List<IPassive> PassiveList { get => _passiveList; }
}