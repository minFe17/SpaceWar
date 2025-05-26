using Utils;

[System.Serializable]
public class WitchInfoData : PlayerInfoData
{
    public override void Init()
    {
        DataSingleton<PlayerData>.Instance.PlayerType = EPlayerType.Witch;
        _name = "À§Ä¡";
    }
}