using UnityEngine;

public class MainCameraFactory : CameraFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _cameraType = ECameraType.MainCamera;
        _prefab = _cameraAssetManager.MainCamera;
        _factoryManager.AddFactorys(_cameraType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject camera = _objectPoolManager.Push(_cameraType, _prefab);
        return camera;
    }
}