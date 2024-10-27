using UnityEngine;

public class FireStoneFactory : ThirdWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.FireStone;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject fireStone = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return fireStone.GetComponent<Enemy>();
    }
}