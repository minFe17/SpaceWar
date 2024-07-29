using Utils;

public class BulletUp : PassiveBase
{
    public override void Init()
    {
        base.Init();
        _image = _passiveSpriteManager.PassiveIconAtlas.GetSprite("BulletUp");
        _name = "źâ �뷮 ����";
        _info = "źâ�� �Ѿ��� 30�� �� �����˴ϴ�";
        _index = 3;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MaxBullet += 30;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
    }
}