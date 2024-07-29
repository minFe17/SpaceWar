using Utils;

public class DamageUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("DamageUp");
        _name = "데미지 증가";
        _info = "데미지가 3 증가";
        _index = 1;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BulletDamage += 3;
    }
}