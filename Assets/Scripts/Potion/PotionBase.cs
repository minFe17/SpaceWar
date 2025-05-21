using Utils;

public class PotionBase
{
    protected AudioClipManager _audiolipManager;
    protected UIManager _uiManager;
    protected PlayerData _playerData;

    void Start()
    {
        SetManager();
    }

    protected void SetManager()
    {
        _audiolipManager = GenericSingleton<AudioClipManager>.Instance;
        _uiManager = GenericSingleton<UIManager>.Instance;
        _playerData = DataSingleton<PlayerData>.Instance;
    }
}