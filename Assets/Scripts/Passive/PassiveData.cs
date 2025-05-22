using UnityEngine;
using Utils;

[System.Serializable]
public class PassiveData
{
    // 데이터 싱글턴
    [SerializeField] string _name;
    [SerializeField] string _info;
    [SerializeField] string _imageName;

    PassiveSpriteManager _spriteManager;
    protected UIManager _uiManager;
    protected PlayerData _playerData = DataSingleton<PlayerData>.Instance;

    public string Name { get => _name; }
    public string Info { get => _info; }

    public Sprite Image
    {
        get
        {
            if (_spriteManager == null)
                _spriteManager = GenericSingleton<PassiveSpriteManager>.Instance;

            return _spriteManager.PassiveIconAtlas.GetSprite(_imageName);
        }
    }

    public virtual void AddPassive() { }
}