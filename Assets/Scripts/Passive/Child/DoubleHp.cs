using UnityEngine;

public class DoubleHp : PassiveBase, IPassive
{
    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("DoubleHp");
        _name = "Hp 두 배";
        _info = "현재 Hp가 두 배로 증가됩니다";
        _index = 7;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.MaxHp *= 2;
        _playerDataManager.CurHp *= 2;
        _uiManager.IngameUI.ShowHp();
    }
}