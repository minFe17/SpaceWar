using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SelectPassiveData
{
    // ������ �̱���
    [SerializeField] List<PassiveData> _passiveList = new List<PassiveData>();

    public List<PassiveData> PassiveList { get => _passiveList; }
}