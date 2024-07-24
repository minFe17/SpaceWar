using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class WorldManager : MonoBehaviour
{
    // 싱글턴
    EWorldType _worldType;
    public EWorldType WorldType { get; set; }

    public async Task NextWorld()
    {
        ReleaseAsset();
        _worldType++;
        await LoadAsset();
    }

    async Task LoadAsset()
    {
        await GenericSingleton<MapAssetManager>.Instance.LoadAsset(_worldType);
        GenericSingleton<ObstacleAssetManager>.Instance.LoadAsset(_worldType);
        // 몬스터
    }

    void ReleaseAsset()
    {
        GenericSingleton<MapAssetManager>.Instance.ReleaseAsset();
        GenericSingleton<ObstacleAssetManager>.Instance.ReleaseAsset();
        // 몬스터

    }
}