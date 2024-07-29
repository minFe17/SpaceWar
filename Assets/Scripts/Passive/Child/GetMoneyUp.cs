using Utils;

public class GetMoneyUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("GetMoneyUp");
        _name = "얻는 돈 증가";
        _info = "추가로 돈을 더 얻습니다";
        _index = 6;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BonusMoney = 10;
    }
}