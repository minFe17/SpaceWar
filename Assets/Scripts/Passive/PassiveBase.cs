using UnityEngine;
using UnityEngine.UI;

public abstract class PassiveBase : MonoBehaviour
{
    protected Image _image;
    protected string _name;
    protected string _info;

    public abstract void Init();
    public abstract void AddPassive();
}
