using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PlayerAssetManager : MonoBehaviour
{
    // �̱���
    AddressableManager _addressableMaanger;

    public GameObject Soldier { get; private set; }
    public GameObject Witch { get; private set; }

    public async Task LoadAsset()
    {
        if (_addressableMaanger == null)
            _addressableMaanger = GenericSingleton<AddressableManager>.Instance;

        Soldier = await _addressableMaanger.GetAddressableAsset<GameObject>("Soldier");
        Witch = await _addressableMaanger.GetAddressableAsset<GameObject>("Witch");
    }
}