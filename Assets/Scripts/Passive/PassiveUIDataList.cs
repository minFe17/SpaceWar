using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PassiveUIDataList
{
    // ������ �̱���
    [SerializeField]
    List<PassiveUIData> _passiveUIDataList = new List<PassiveUIData>();

    public List<PassiveUIData> UIDataList { get => _passiveUIDataList; }
}