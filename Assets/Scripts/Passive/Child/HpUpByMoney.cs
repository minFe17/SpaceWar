using Utils;

public class HpUpByMoney : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("HpUpByMoney");
        _name = "�� ��� HP ����";
        _info = "�����ϰ� �ִ� ���� 50�� ���� ������ HP 1�� ����";
        _index = 8;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.HPUpByMoney = true;
    }
}