using UnityEngine;
using Utils;

public class DamageUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/DamageUp");
        _name = "������ ����";
        _info = "�������� 3 ����";
        _index = 1;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BulletDamage += 3;
    }
}