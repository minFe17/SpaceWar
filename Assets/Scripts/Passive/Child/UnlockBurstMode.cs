using UnityEngine;

public class UnlockBurstMode : PassiveBase, IPassive
{
    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("UnlockBurstMode");
        _name = "점사 모드 해제";
        _info = "점사 모드가 해제됩니다";
        _index = 4;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.UnlockBurstMode = true;
    }
}