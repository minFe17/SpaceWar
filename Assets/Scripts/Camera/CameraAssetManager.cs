using UnityEngine;
using Utils;

public class CameraAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;

    public GameObject MainCamera { get; set; }
    public GameObject FollowCamera { get; set; }
    public GameObject MiniMapCamera { get; set; }
    public GameObject MapCamera { get; set; }

    public async void LoadAsset()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
        MainCamera = await _addressableManager.GetAddressableAsset<GameObject>("Main Camera");
        FollowCamera = await _addressableManager.GetAddressableAsset<GameObject>("Follow Camera");
        MiniMapCamera = await _addressableManager.GetAddressableAsset<GameObject>("MiniMap Camera");
        MapCamera = await _addressableManager.GetAddressableAsset<GameObject>("Map Camera");
    }
}
