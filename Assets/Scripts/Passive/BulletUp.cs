using UnityEngine;
using Utils;

public class BulletUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/BulletUp");
        _name = "탄창 용량 증가";
        _info = "탄창에 총알이 30발 더 장전됩니다";
        _index = 3;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MaxBullet += 30;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
    }
}
