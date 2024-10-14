public class UnlockAutoMode : PassiveBase, IPassive
{
    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerDataManager.UnlockAutoMode = true;
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}