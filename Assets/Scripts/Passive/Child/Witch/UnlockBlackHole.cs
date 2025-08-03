using Utils;

public class UnlockBlackHole : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.UnlockSecondSkill = true;
    }
}