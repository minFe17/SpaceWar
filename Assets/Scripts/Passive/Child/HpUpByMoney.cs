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
        _name = "�� ��� HP ����";
        _info = "�����ϰ� �ִ� ���� 50�� ���� ������ HP 1�� ����";
        _index = 8;
    }

    void IPassive.AddPassive()
    {
        _playerDataManager.HPUpByMoney = true;
    }
}