using Utils;

public class UnlockAutoMode : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.UnlockSecondSkill = true;
    }
}