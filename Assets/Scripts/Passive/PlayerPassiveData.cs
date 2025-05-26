using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerPassiveData
{
    // ������ �̱���
    [field: SerializeField]
    List<PassiveData> _playerPassiveDatas = new List<PassiveData>();

    public List<PassiveData> PlayerPassiveDatas { get => _playerPassiveDatas; }
}