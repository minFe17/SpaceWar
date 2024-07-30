using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class CameraAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;

    public GameObject MainCamera { get; private set; }
    public GameObject FollowCamera { get; private set; }
    public GameObject MiniMapCamera { get; private set; }
    public GameObject MapCamera { get; private set; }

    public async Task LoadAsset()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        MainCamera = await _addressableManager.GetAddressableAsset<GameObject>("Main Camera");
        FollowCamera = await _addressableManager.GetAddressableAsset<GameObject>("Follow Camera");
        MiniMapCamera = await _addressableManager.GetAddressableAsset<GameObject>("MiniMap Camera");
        MapCamera = await _addressableManager.GetAddressableAsset<GameObject>("Map Camera");
    }
}