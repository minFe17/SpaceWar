using Utils;

public abstract class CameraFactoryBase : FactoryBase
{
    protected ECameraType _cameraType;
    protected CameraAssetManager _cameraAssetManager;

    protected abstract void Init();

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _cameraAssetManager = GenericSingleton<CameraAssetManager>.Instance;
        Init();
    }
}