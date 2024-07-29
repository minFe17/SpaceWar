using Utils;

public class SpeedUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("SpeedUp");
        _name = "이동 속도 증가";
        _info = "이동 속도가 더욱 빨라집니다.";
        _index = 2;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MoveSpeed += 3f;
    }
}