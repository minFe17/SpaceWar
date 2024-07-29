using Utils;

public class UnlockAutoMode : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("UnlockAutoMode");
        _name = "���� ��� ����";
        _info = "���� ��尡 �����˴ϴ�";
        _index = 5;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockAutoMode = true;
    }
}