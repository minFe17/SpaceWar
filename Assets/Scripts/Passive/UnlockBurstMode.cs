using UnityEngine;
using Utils;

public class UnlockBurstMode : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/UnlockBurstMode");
        _name = "���� ��� ����";
        _info = "���� ��尡 �����˴ϴ�";
        _index = 4;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockBurstMode = true;
    }
}