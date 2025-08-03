using Utils;

public class BulletUp : IPassiveEffect
{
    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.MaxBullet *= 2;
        UIManager uiManager = GenericSingleton<UIManager>.Instance;
        uiManager.IngameUI.ShowBullet();
    }
}