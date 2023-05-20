using UnityEngine;
using Utils;

public class SpeedUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/SpeedUp");
        _name = "�̵� �ӵ� ����";
        _info = "�̵� �ӵ��� ���� �������ϴ�.";
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MoveSpeed += 3f;
    }
}
