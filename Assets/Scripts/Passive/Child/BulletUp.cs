using UnityEngine;

public class BulletUp : PassiveBase, IPassive
{
    int _bulletUp = 30;

    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("BulletUp");
        _name = "źâ �뷮 ����";
        _info = "źâ�� �Ѿ��� 30�� �� �����˴ϴ�";
        _index = 3;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.MaxBullet += _bulletUp;
        _uiManager.IngameUI.ShowBullet();
    }
}