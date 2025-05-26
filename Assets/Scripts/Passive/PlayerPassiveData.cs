using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerPassiveData
{
    // 데이터 싱글턴
    [field: SerializeField]
    List<PassiveData> _playerPassiveDatas = new List<PassiveData>();

    public List<PassiveData> PlayerPassiveDatas { get => _playerPassiveDatas; }
}