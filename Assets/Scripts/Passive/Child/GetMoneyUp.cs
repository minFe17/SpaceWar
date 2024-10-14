using UnityEngine;

public class GetMoneyUp : PassiveBase, IPassive
{
    int _bonusMoney = 10;

    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("GetMoneyUp");
        _name = "��� �� ����";
        _info = "�߰��� ���� �� ����ϴ�";
        _index = 6;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.BonusMoney = _bonusMoney;
    }
}