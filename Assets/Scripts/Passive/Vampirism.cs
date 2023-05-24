using UnityEngine;
using Utils;

public class Vampirism : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/Vampirism");
        _name = "뱀파이어";
        _info = "적 처치 시 일정 확률로 HP 회복";
        _index = 9;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.Vampirism = true;
    }
}
