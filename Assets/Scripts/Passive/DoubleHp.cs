using UnityEngine;
using Utils;

public class DoubleHp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/DoubleHp");
        _name = "Hp 두 배";
        _info = "현재 Hp가 두 배로 증가됩니다";
        _index = 7;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MaxHp *= 2;
        GenericSingleton<PlayerDataManager>.Instance.CurHp *= 2;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}