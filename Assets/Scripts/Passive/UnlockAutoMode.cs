using UnityEngine;
using Utils;

public class UnlockAutoMode : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/UnlockAutoMode");
        _name = "연사 모드 해제";
        _info = "연사 모드가 해제됩니다";
        _index = 5;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.UnlockAutoMode = true;
    }
}
