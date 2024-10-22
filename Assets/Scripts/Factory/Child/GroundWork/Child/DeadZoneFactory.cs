using UnityEngine;

public class DeadZoneFactory : GroundWorkFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _groundWorkType = EGroundWorkType.DeadZone;
        _prefab = _mapAssetMaanger.DeadZone;
        _factoryManager.AddFactorys(_groundWorkType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject deadZone = _objectPoolManager.Push(_groundWorkType, _prefab);
        return deadZone;
    }
}