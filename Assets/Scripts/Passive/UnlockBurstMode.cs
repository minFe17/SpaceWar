using UnityEngine;
using Utils;

public class UnlockBurstMode : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/UnlockBurstMode");
        _name = "점사 모드 해제";
        _info = "점사 모드가 해제됩니다";
        _index = 4;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockBurstMode = true;
    }
}