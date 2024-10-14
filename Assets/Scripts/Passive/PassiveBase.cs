using UnityEngine;
using Utils;

public abstract class PassiveBase : MonoBehaviour
{
    protected Sprite _image;
    protected string _name;
    protected string _info;
    protected int _index;

    protected PlayerDataManager _playerDataManager;
    protected UIManager _uiManager;
    protected PassiveSpriteManager _passiveSpriteManager;

    public virtual void Init()
    {
        _playerDataManager = GenericSingleton<PlayerDataManager>.Instance;
        _uiManager = GenericSingleton<UIManager>.Instance;
        _passiveSpriteManager = GenericSingleton<PassiveSpriteManager>.Instance;
    }
}