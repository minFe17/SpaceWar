using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SelectPassiveData
{
    // 데이터 싱글턴
    [SerializeField] List<PassiveData> _passiveList = new List<PassiveData>();

    public List<PassiveData> PassiveList { get => _passiveList; }
}