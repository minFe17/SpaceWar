using UnityEngine;

public class FollowCameraFactory : CameraFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _cameraType = ECameraType.FollowCamera;
        _prefab = _cameraAssetManager.FollowCamera;
        _factoryManager.AddFactorys(_cameraType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject camera = _objectPoolManager.Push(_cameraType, _prefab);
        return camera;
    }
}