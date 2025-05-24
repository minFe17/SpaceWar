[System.Serializable]
public class WitchInfoData : PlayerInfoData
{
    public override void Init()
    {
        _playerType = EPlayerType.Witch;
        _name = "À§Ä¡";
    }
}