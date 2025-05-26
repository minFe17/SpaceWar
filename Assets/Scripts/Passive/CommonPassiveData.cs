using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CommonPassiveData
{
    // ������ �̱���
    [field: SerializeField]
    List<PassiveData> _commonPassiveDatas = new List<PassiveData>();

    public List<PassiveData> CommonPassiveDatas { get => _commonPassiveDatas; }
}