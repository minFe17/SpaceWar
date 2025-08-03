using Utils;

public class HpUpByMoney : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.HPUpByMoney = true;
    }
}