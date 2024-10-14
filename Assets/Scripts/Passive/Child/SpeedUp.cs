using UnityEngine;

public class SpeedUp : PassiveBase, IPassive
{
    float _moveSpeed = 3f;

    int IPassive.Index { get => _index; }
    string IPassive.Name { get => _name; }
    string IPassive.Info { get => _info; }
    Sprite IPassive.Image { get => _image; }

    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("SpeedUp");
        _name = "�̵� �ӵ� ����";
        _info = "�̵� �ӵ��� ���� �������ϴ�.";
        _index = 2;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.MoveSpeed += _moveSpeed;
    }
}