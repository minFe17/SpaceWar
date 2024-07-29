using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class WorldManager : MonoBehaviour
{
    // 싱글턴
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
        await LoadAsset();
    }

    async Task LoadAsset()
    {
        await GenericSingleton<MapAssetManager>.Instance.LoadAsset(WorldType);
        GenericSingleton<ObstacleAssetManager>.Instance.LoadAsset(WorldType);
        // 몬스터
    }

    void ReleaseAsset()
    {
        GenericSingleton<MapAssetManager>.Instance.ReleaseAsset();
        GenericSingleton<ObstacleAssetManager>.Instance.ReleaseAsset();
        // 몬스터

    }
}