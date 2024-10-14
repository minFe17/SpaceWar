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
        _name = "얻는 돈 증가";
        _info = "추가로 돈을 더 얻습니다";
        _index = 6;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.BonusMoney = _bonusMoney;
    }
}