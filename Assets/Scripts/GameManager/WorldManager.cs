using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class WorldManager : MonoBehaviour
{
    // �̱���
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
        // ����
    }

    void ReleaseAsset()
    {
        GenericSingleton<MapAssetManager>.Instance.ReleaseAsset();
        GenericSingleton<ObstacleAssetManager>.Instance.ReleaseAsset();
        // ����

    }
}