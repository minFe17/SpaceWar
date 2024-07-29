using Utils;

public class GetMoneyUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("GetMoneyUp");
        _name = "��� �� ����";
        _info = "�߰��� ���� �� ����ϴ�";
        _index = 6;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BonusMoney = 10;
    }
}