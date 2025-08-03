using Utils;

public class UnlockThunderBall : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.UnlockFirstSkill = true;
    }
}