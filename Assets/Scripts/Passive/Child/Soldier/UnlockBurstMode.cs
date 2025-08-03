using Utils;

public class UnlockBurstMode : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.UnlockFirstSkill = true;
    }
}