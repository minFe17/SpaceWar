using UnityEngine;
using Utils;

public class HPUpByMoney : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/HPUpByMoney");
        _name = "돈 비례 HP 증가";
        _info = "보유하고 있는 돈이 50을 넘을 때마다 HP 1씩 증가";
        _index = 8;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.HPUpByMoney = true;
    }
}