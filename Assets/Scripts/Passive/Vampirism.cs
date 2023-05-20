using UnityEngine;
using Utils;

public class Vampirism : PassiveBase
{
    public override void Init()
    {
        _image = Resources.Load<Sprite>("Prefabs/PassiveIcon/Vampirism");
        _name = "�����̾�";
        _info = "�� óġ �� ���� Ȯ���� HP ȸ��";
    }

    public override void AddPassive()
    {
        GenericSingleton<PlayerDataManager>.Instance.Vampirism = true;
    }
}
