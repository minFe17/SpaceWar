using Utils;

public class Vampirism : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.Vampirism = true;
    }
}