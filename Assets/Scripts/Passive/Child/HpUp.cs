using UnityEngine;

public class HpUp : PassiveBase, IPassive
{
    int _hpAmount = 10;

    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("HpUp");
        _name = "HP 증가";
        _info = "최대 HP가 10 증가";
        _index = 0;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.MaxHp += _hpAmount;
        _playerDataManager.CurHp += _hpAmount;
        _uiManager.IngameUI.ShowHp();
    }
}