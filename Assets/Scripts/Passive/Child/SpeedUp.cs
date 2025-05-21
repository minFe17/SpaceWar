public class SpeedUp : PassiveBase, IPassive
{
    float _moveSpeed = 3f;

    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerData.MoveSpeed += _moveSpeed;
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}