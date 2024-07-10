using UnityEngine;
using Utils;

public class HPUpByMoney : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/HPUpByMoney");
        _name = "�� ��� HP ����";
        _info = "�����ϰ� �ִ� ���� 50�� ���� ������ HP 1�� ����";
        _index = 8;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.HPUpByMoney = true;
    }
}