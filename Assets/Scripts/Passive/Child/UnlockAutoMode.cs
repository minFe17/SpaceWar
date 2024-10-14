using UnityEngine;

public class UnlockAutoMode : PassiveBase, IPassive
{
    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("UnlockAutoMode");
        _name = "���� ��� ����";
        _info = "���� ��尡 �����˴ϴ�";
        _index = 5;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.UnlockAutoMode = true;
    }
}