using UnityEngine;
using Utils;

public class GetMoneyUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/GetMoneyUp");
        _name = "��� �� ����";
        _info = "�߰��� ���� �� ����ϴ�";
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BonusMoney = 7;
    }
}