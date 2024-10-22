using UnityEngine;

public class MiniMapCameraFactory : CameraFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _cameraType = ECameraType.MiniMapCamera;
        _prefab = _cameraAssetManager.MiniMapCamera;
        _factoryManager.AddFactorys(_cameraType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject camera = _objectPoolManager.Push(_cameraType, _prefab);
        return camera;
    }
}