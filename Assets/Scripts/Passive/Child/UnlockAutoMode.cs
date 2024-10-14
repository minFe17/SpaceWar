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
        _name = "연사 모드 해제";
        _info = "연사 모드가 해제됩니다";
        _index = 5;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.UnlockAutoMode = true;
    }
}