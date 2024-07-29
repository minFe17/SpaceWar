using UnityEngine;
using Utils;

public abstract class PassiveBase : MonoBehaviour
{
    protected Sprite _image;
    protected string _name;
    protected string _info;
    protected int _index;

    protected PassiveSpriteManager _passiveSpriteManager;

    public Sprite Image { get => _image; }
    public string Name { get => _name; }
    public string Info { get => _info; }
    public int Index {  get => _index; }

    public virtual void Init()
    {
        _passiveSpriteManager = GenericSingleton<PassiveSpriteManager>.Instance;
    }
    public abstract void AddPassive();
}