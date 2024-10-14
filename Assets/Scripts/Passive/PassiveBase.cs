using UnityEngine;
using Utils;

public abstract class PassiveBase : MonoBehaviour
{
    protected PassiveData _passiveData;
    protected PlayerDataManager _playerDataManager;
    protected UIManager _uiManager;
    protected PassiveSpriteManager _passiveSpriteManager;

    void Init()
    {
        _playerDataManager = GenericSingleton<PlayerDataManager>.Instance;
        _uiManager = GenericSingleton<UIManager>.Instance;
        _passiveSpriteManager = GenericSingleton<PassiveSpriteManager>.Instance;
    }

    protected void SetPassiveData(PassiveData passiveData)
    {
        Init();
        _passiveData = passiveData;
        string imageName = _passiveData.ImageName;
        _passiveData.Image = _passiveSpriteManager.PassiveIconAtlas.GetSprite(imageName);
    }
}