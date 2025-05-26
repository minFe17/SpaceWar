public class SpeedUp : PassiveData
{
    float _moveSpeed = 3f;

    public override void AddPassive()
    {
        _playerData.MoveSpeed += _moveSpeed;
    }
}