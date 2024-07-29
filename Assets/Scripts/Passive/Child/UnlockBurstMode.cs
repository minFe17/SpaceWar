using Utils;

public class UnlockBurstMode : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("UnlockBurstMode");
        _name = "���� ��� ����";
        _info = "���� ��尡 �����˴ϴ�";
        _index = 4;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockBurstMode = true;
    }
}