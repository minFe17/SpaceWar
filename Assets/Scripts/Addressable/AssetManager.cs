using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class AssetManager : MonoBehaviour
{
    // ╫л╠шео
    bool _isLoad;

    public bool IsLoad { get => _isLoad; }

    public async Task LoadAsset()
    {
        if (_isLoad) 
            return;
        _isLoad = true;
        await LoadPlayerAsset();
        await LoadCameraAsset();
        await LoadSoundAsset();
        await LoadUIAsset();
        await LoadPassiveAsset();
        await LoadGroundWorkAsset();
        await LoadFactory();
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

    async Task LoadPassiveAsset()
    {
        await GenericSingleton<PassiveSpriteManager>.Instance.LoadAsset();
        await GenericSingleton<PassiveManager>.Instance.Init();
    }

    async Task LoadGroundWorkAsset()
    {
        await GenericSingleton<MapAssetManager>.Instance.LoadAsset();
    }

    async Task LoadFactory()
    {
        await GenericSingleton<FactoryManager>.Instance.Init();
    }
}