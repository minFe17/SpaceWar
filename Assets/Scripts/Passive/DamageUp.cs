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
        _name = "데미지 증가";
        _info = "데미지가 3 증가";
    }
}
