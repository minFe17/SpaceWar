using UnityEngine;

public class HpUpByMoney : PassiveBase, IPassive
{
    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("HpUpByMoney");
        _name = "돈 비례 HP 증가";
        _info = "보유하고 있는 돈이 50을 넘을 때마다 HP 1씩 증가";
        _index = 8;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.HPUpByMoney = true;
    }
}