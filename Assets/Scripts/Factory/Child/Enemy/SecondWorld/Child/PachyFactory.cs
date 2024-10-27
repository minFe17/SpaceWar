using UnityEngine;

public class PachyFactory : SecondWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = ESecondWorldEnemyType.Pachy;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject pachy = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return pachy.GetComponent<Enemy>();
    }
}