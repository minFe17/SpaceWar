using Utils;

public class Vampirism : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("Vampirism");
        _name = "�����̾�";
        _info = "�� óġ �� ���� Ȯ���� HP ȸ��";
        _index = 9;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.Vampirism = true;
    }
}