using UnityEngine;

public class DamageUp : PassiveBase, IPassive
{
    int _bulletDamage = 3;

    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("DamageUp");
        _name = "������ ����";
        _info = "�������� 3 ����";
        _index = 1;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.BulletDamage += _bulletDamage;
    }
}