using UnityEngine;

public class FireStoneFactory : ThirdWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.FireStone;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject fireStone = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return fireStone;
    }
}