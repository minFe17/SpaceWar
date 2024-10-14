using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using Utils;

public class PassiveSpriteManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;

    public SpriteAtlas PassiveIconAtlas { get; private set; }

    public async Task LoadAsset()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        PassiveIconAtlas = await _addressableManager.GetAddressableAsset<SpriteAtlas>("PassiveIcon");
    }
}