using Utils;

public class UnlockAutoMode : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("UnlockAutoMode");
        _name = "연사 모드 해제";
        _info = "연사 모드가 해제됩니다";
        _index = 5;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockAutoMode = true;
    }
}