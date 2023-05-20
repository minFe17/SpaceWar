using UnityEngine;
using Utils;

public class UnlockAutoMode : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/UnlockAutoMode");
        _name = "���� ��� ����";
        _info = "���� ��尡 �����˴ϴ�";
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockAutoMode = true;
    }
}
