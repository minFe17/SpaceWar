using Utils;

public class PotionBase
{
    protected AudioClipManager _audiolipManager;
    protected UIManager _uiManager;
    protected PlayerDataManager _playerDataManager;

    void Start()
    {
        SetManager();
    }

    protected void SetManager()
    {
        _audiolipManager = GenericSingleton<AudioClipManager>.Instance;
        _uiManager = GenericSingleton<UIManager>.Instance;
        _playerDataManager = GenericSingleton<PlayerDataManager>.Instance;
    }
}