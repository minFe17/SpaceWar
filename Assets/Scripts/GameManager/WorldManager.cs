using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class WorldManager : MonoBehaviour
{
    // ╫л╠шео
    FactoryManager _factoryManager;
    ObjectPoolManager _objectPoolManager;

    public EWorldType WorldType { get; set; }

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
    }

    async Task LoadAsset()
    {
        await GenericSingleton<MapAssetManager>.Instance.LoadAsset(WorldType);
        await GenericSingleton<EnemyManager>.Instance.LoadAsset();
        await GenericSingleton<ObstacleAssetManager>.Instance.LoadAsset();

        MakeWorldFactory();
    }

    void ReleaseAsset()
    {
        GenericSingleton<MapAssetManager>.Instance.ReleaseAsset();
        GenericSingleton<EnemyManager>.Instance.ReleaseAsset();
        GenericSingleton<ObstacleAssetManager>.Instance.ReleaseAsset();
    }

    void MakeWorldFactory()
    {
        _factoryManager.ChangeWorld();
        _objectPoolManager.ChangeWorldPool();
    }

    public async Task ResetWorld()
    {
        ReleaseAsset();
        WorldType = EWorldType.FirstWorld;
        await LoadAsset();
    }

    public async Task NextWorld()
    {
        ReleaseAsset();
        WorldType++;
        if (WorldType == EWorldType.Max)
            return;
        await LoadAsset();
    }
}