using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PassiveDataList
{
    // 데이터 싱글턴
    [field: SerializeField]
    List<PassiveData> _passiveUIDataList = new List<PassiveData>();

    public List<PassiveData> UIDataList { get => _passiveUIDataList; }

    public void Check()
    {
        Debug.Log(_passiveUIDataList.Count);

        for (int i = 0; i < _passiveUIDataList.Count; i++)
        {
            Debug.Log(_passiveUIDataList[i].Name);
        }
    }
}