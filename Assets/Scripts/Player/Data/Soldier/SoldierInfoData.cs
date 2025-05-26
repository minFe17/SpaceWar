using Utils;

[System.Serializable]
public class SoldierInfoData : PlayerInfoData
{
    public override void Init()
    {
        DataSingleton<PlayerData>.Instance.PlayerType = EPlayerType.Soldier;
        _name = "¼ÖÀú";
    }
}