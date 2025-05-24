using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PassiveDataList
{
    // 데이터 싱글턴
    [field: SerializeField]
    List<PassiveData> _passiveUIDataList = new List<PassiveData>();

    public List<PassiveData> UIDataList { get => _passiveUIDataList; }
}