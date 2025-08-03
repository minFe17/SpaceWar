using UnityEngine;
using Utils;

[System.Serializable]
public class PassiveData
{
    // 데이터 싱글턴
    [SerializeField] int _type;
    [SerializeField] string _name;
    [SerializeField] string _info;
    [SerializeField] string _imageName;

    protected UIManager _uiManager;
    
    PassiveSpriteManager _spriteManager;

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

    public void AddPassive() 
    {
        GenericSingleton<PassiveManager>.Instance.GetPassive((EPassiveType)_type).AddPassive();
    }
}