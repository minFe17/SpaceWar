using UnityEngine;
using Utils;

public class DamageUp : PassiveBase
{
    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BulletDamage += 3;
    }

    public override void Init()
    {
        _image = Resources.Load("Prefabs/PassiveIcon/DamageUp") as Sprite;
        _name = "������ ����";
        _info = "�������� 3 ����";
    }
}
