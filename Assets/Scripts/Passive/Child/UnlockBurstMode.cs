using Utils;

public class UnlockBurstMode : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("UnlockBurstMode");
        _name = "점사 모드 해제";
        _info = "점사 모드가 해제됩니다";
        _index = 4;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockBurstMode = true;
    }
}