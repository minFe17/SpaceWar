using Utils;

public class HpUpByMoney : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("HpUpByMoney");
        _name = "돈 비례 HP 증가";
        _info = "보유하고 있는 돈이 50을 넘을 때마다 HP 1씩 증가";
        _index = 8;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.HPUpByMoney = true;
    }
}