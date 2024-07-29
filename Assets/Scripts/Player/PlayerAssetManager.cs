using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PlayerAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableMaanger;

    public GameObject Player { get; set; }
    public GameObject Bullet { get; set; }

    public async Task LoadAsset()
    {
        if (_addressableMaanger == null)
            _addressableMaanger = GenericSingleton<AddressableManager>.Instance;

        Player = await _addressableMaanger.GetAddressableAsset<GameObject>("Player");
        Bullet = await _addressableMaanger.GetAddressableAsset<GameObject>("Bullet");
    }
}