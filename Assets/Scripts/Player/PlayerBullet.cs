using Utils;

public class PlayerBullet : Bullet
{
    private void Start()
    {
        _damage = GenericSingleton<PlayerDataManager>.Instance.BulletDamage;
    }

    void Update()
    {
        Move();
    }
}
