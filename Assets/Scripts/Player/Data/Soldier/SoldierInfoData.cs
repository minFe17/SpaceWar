[System.Serializable]
public class SoldierInfoData : PlayerInfoData
{
    public override void Init()
    {
        _playerType = EPlayerType.Soldier;
        _name = "¼ÖÀú";
    }
}