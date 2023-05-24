using UnityEngine;
using Utils;

public class BulletUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/BulletUp");
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
