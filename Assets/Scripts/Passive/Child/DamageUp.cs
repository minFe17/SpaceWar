using Utils;

public class DamageUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("DamageUp");
        _name = "������ ����";
        _info = "�������� 3 ����";
        _index = 1;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BulletDamage += 3;
    }
}