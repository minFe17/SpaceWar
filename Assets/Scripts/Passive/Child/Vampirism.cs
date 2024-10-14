using UnityEngine;

public class Vampirism : PassiveBase, IPassive
{
    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("Vampirism");
        _name = "뱀파이어";
        _info = "적 처치 시 일정 확률로 HP 회복";
        _index = 9;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.Vampirism = true;
    }
}