using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class WorldManager : MonoBehaviour
{
    // ╫л╠шео
    public EWorldType WorldType { get; set; }

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

    async Task LoadAsset()
    {
        await GenericSingleton<MapAssetManager>.Instance.LoadAsset(WorldType);
        await GenericSingleton<EnemyManager>.Instance.LoadAsset(WorldType);
        await GenericSingleton<ObstacleAssetManager>.Instance.LoadAsset(WorldType);
    }

    void ReleaseAsset()
    {
        GenericSingleton<MapAssetManager>.Instance.ReleaseAsset();
        GenericSingleton<EnemyManager>.Instance.ReleaseAsset(WorldType);
        GenericSingleton<ObstacleAssetManager>.Instance.ReleaseAsset();
    }
}