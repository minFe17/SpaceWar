using UnityEngine;
using Utils;

public class GetMoneyUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/GetMoneyUp");
        _name = "얻는 돈 증가";
        _info = "추가로 돈을 더 얻습니다";
        _index = 6;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.BonusMoney = 7;
    }
}
