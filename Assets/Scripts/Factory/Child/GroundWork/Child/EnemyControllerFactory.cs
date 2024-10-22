using UnityEngine;

public class EnemyControllerFactory : GroundWorkFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _groundWorkType = EGroundWorkType.EnemyController;
        _prefab = _mapAssetMaanger.EnemyController;
        _factoryManager.AddFactorys(_groundWorkType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject deadZone = _objectPoolManager.Push(_groundWorkType, _prefab);
        return deadZone;
    }
}