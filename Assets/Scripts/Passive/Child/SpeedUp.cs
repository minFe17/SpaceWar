using Utils;

public class SpeedUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("SpeedUp");
        _name = "�̵� �ӵ� ����";
        _info = "�̵� �ӵ��� ���� �������ϴ�.";
        _index = 2;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MoveSpeed += 3f;
    }
}