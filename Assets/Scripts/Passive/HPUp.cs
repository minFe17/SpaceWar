using UnityEngine;
using Utils;

public class HPUp : PassiveBase
{
    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.MaxHp += 10;
    }

    public override void Init()
    {
        _image = Resources.Load("Prefabs/PassiveIcon/HPUp") as Sprite;
        _name = "HP ����";
        _info = "�ִ� HP�� 10 ����";
    }
}
