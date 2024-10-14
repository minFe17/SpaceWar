public interface IPassive
{
    PassiveData PassiveData { get; }

    void AddPassive();
    void SetPassiveData(PassiveData passiveData);
}