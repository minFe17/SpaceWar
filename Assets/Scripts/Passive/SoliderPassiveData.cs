using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Sol
{
    // 데이터 싱글턴
    [field: SerializeField]
    List<PassiveData> _commonPassiveDatas = new List<PassiveData>();

    public List<PassiveData> CommonPassiveDatas { get => _commonPassiveDatas; }
}