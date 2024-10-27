using System;
using System.Threading.Tasks;

interface IEnemyList
{
    Task AddEnemyList(EnemyManager enemyManager, AddressableManager addressableManager);
    Enum ConvertEnumToInt(int value);
    void ReleaseAsset();
    void MakePool(EnemyObjectPool enemyObjectPool);
}