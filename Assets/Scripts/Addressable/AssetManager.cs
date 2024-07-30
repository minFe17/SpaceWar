using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // ╫л╠шео
    public async Task LoadAsset()
    {
        await LoadPlayerAsset();
        await LoadCameraAsset();
        await LoadSoundAsset();
        await LoadUIAsset();
        LoadCoinAsset();
        LoadPassiveAsset();
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

    async Task LoadUIAsset()
    {
        await GenericSingleton<UIManager>.Instance.LoadAsset();
    }

    void LoadCoinAsset()
    {
        GenericSingleton<CoinManager>.Instance.Init();
    }

    void LoadPassiveAsset()
    {
        GenericSingleton<PassiveSpriteManager>.Instance.LoadAsset();
    }
}