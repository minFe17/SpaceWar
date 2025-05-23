using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PlayerAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableMaanger;

    public GameObject Soldier { get; private set; }
    public GameObject Bullet { get; private set; }

    public async Task LoadAsset()
    {
        if (_addressableMaanger == null)
            _addressableMaanger = GenericSingleton<AddressableManager>.Instance;

        Soldier = await _addressableMaanger.GetAddressableAsset<GameObject>("Soldier");
        Bullet = await _addressableMaanger.GetAddressableAsset<GameObject>("Bullet");
    }
}