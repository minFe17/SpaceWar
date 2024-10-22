using UnityEngine;

public class MapCameraFactory : CameraFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _cameraType = ECameraType.MapCamera;
        _prefab = _cameraAssetManager.MapCamera;
        _factoryManager.AddFactorys(_cameraType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject camera = _objectPoolManager.Push(_cameraType, _prefab);
        return camera;
    }
}