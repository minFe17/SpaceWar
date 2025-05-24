using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PassiveDataList
{
    // ������ �̱���
    [field: SerializeField]
    List<PassiveData> _passiveUIDataList = new List<PassiveData>();

    public List<PassiveData> UIDataList { get => _passiveUIDataList; }
}