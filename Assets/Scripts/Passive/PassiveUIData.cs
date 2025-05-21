using UnityEngine;

[System.Serializable]
public class PassiveUIData
{
    // 데이터 싱글턴
    [SerializeField] int _index;
    [SerializeField] string _name;
    [SerializeField] string _info;
    [SerializeField] string _imageName;

    public PassiveUIData
        (int index, string name, string info, string imageName)
    {
        _index = index;
        _name = name;
        _info = info;
        _imageName = imageName;
    }
}