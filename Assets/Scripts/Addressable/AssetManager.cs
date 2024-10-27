using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // ╫л╠шео
    public async Task LoadAsset()
    {
        LoadFactory();
        await LoadPlayerAsset();
        await LoadCameraAsset();
        await LoadSoundAsset();
        await LoadUIAsset();
        await LoadWorldAsset();
        await LoadPassiveAsset();
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

    async Task LoadWorldAsset()
    {
        await GenericSingleton<WorldManager>.Instance.ResetWorld();
    }

    async Task LoadPassiveAsset()
    {
        await GenericSingleton<PassiveSpriteManager>.Instance.LoadAsset();
        await GenericSingleton<PassiveManager>.Instance.Init();
    }

    void LoadFactory()
    {
        GenericSingleton<FactoryManager>.Instance.Init();
    }
}