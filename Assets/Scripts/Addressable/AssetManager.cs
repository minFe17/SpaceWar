using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // ╫л╠шео
    public async Task LoadAsset()
    {
        await LoadUIAsset();
        await LoadPlayerAsset();
        await LoadCameraAsset();
        await LoadSoundAsset();
        LoadCoinAsset();
        LoadPassiveAsset();
    }

    void LoadCoinAsset()
    {
        GenericSingleton<CoinManager>.Instance.Init();
    }

    void LoadPassiveAsset()
    {
        GenericSingleton<PassiveSpriteManager>.Instance.LoadAsset();
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

    async Task LoadSoundAsset()
    {
        await GenericSingleton<SoundManager>.Instance.LoadAsset();
        await GenericSingleton<AudioClipManager>.Instance.LoadAsset();
    }
}