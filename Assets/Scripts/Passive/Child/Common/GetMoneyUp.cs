using Utils;

public class GetMoneyUp : IPassiveEffect
{
    int _bonusMoney = 10;

    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.BonusMoney = _bonusMoney;
    }
}