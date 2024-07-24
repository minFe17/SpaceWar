using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CoinManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;
    List<GameObject> _coinList = new List<GameObject>();

    public async void Init()
    {
        if (_coinList.Count == 0)
            return;
        _addressableManager = GenericSingleton<AddressableManager>.Instance;
        _coinList.Add(await _addressableManager.GetAddressableAsset<GameObject>("GoldCoin"));
        _coinList.Add(await _addressableManager.GetAddressableAsset<GameObject>("SilverCoin"));
    }

    public void MakeCoin(Vector3 pos)
    {
        int random = Random.Range(0, _coinList.Count);
        GameObject coin = Instantiate(_coinList[random]);
        coin.transform.position = pos;
    }
}