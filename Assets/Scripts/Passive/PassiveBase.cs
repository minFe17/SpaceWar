using UnityEngine;

public abstract class PassiveBase : MonoBehaviour
{
    protected Sprite _image;
    protected string _name;
    protected string _info;
    protected int _index;

    public Sprite Image { get => _image; }
    public string Name { get => _name; }
    public string Info { get => _info; }
    public int Index {  get => _index; }

    public abstract void Init();
    public abstract void AddPassive();
}
