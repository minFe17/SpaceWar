using Utils;

public class SpeedUp : IPassiveEffect
{
    float _moveSpeed = 3f;

    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.MoveSpeed += _moveSpeed;
    }
}