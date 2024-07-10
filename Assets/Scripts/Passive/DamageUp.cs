using UnityEngine;
using Utils;

public class DamageUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/DamageUp");
        _name = "데미지 증가";
        _info = "데미지가 3 증가";
        _index = 1;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BulletDamage += 3;
    }
}