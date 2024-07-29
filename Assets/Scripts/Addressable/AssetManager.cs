using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // 싱글턴
    public async Task LoadAsset()
    {
        await LoadUIAsset();
        await LoadPlayerAsset();
        await LoadCameraAsset();
        LoadCoinAsset();
    }

    void LoadCoinAsset()
    {
        GenericSingleton<CoinManager>.Instance.Init();
    }

    async Task LoadUIAsset()
    {
        await GenericSingleton<UIManager>.Instance.LoadAsset();
    }

    async Task LoadCameraAsset()
    {
        await GenericSingleton<CameraAssetManager>.Instance.LoadAsset();
    }

    async Task LoadPlayerAsset()
    {
        await GenericSingleton<PlayerAssetManager>.Instance.LoadAsset();
    }

    //async Task SoundAsset()
    //{
    //    // 사운드
    //}
}