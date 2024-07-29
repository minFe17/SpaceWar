using Utils;

public class HpUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("HpUp");
        _name = "HP ����";
        _info = "�ִ� HP�� 10 ����";
        _index = 0;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MaxHp += 10;
        GenericSingleton<PlayerDataManager>.Instance.CurHp += 10;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}