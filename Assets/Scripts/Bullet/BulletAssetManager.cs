using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class BulletAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableMaanger;

    public GameObject Bullet { get; private set; }
    public GameObject IceLance { get; private set; }
    public GameObject ThunderBall { get; private set; }
    public GameObject BlackHole { get; private set; }
    public GameObject ChainThunder {  get; private set; }

    public async Task LoadAsset()
    {
        if (_addressableMaanger == null)
            _addressableMaanger = GenericSingleton<AddressableManager>.Instance;

        Bullet = await _addressableMaanger.GetAddressableAsset<GameObject>("Bullet");
        IceLance = await _addressableMaanger.GetAddressableAsset<GameObject>("IceLance");
        ThunderBall = await _addressableMaanger.GetAddressableAsset<GameObject>("ThunderBall");
        BlackHole = await _addressableMaanger.GetAddressableAsset<GameObject>("BlackHole");
        ChainThunder = await _addressableMaanger.GetAddressableAsset<GameObject>("ChainThunder");
    }
}
