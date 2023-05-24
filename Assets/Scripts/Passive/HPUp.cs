using UnityEngine;
using Utils;

public class HPUp : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/HPUp");
        _name = "HP ����";
        _info = "�ִ� HP�� 10 ����";
        _index = 0;
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MaxHp += 10;
        GenericSingleton<PlayerDataManager>.Instance.CurHp += 10;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
    }
}
