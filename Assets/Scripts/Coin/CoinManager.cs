using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CoinManager : MonoBehaviour
{
    // ╫л╠шео
    FactoryManager _factoryManager;
    List<Coin> _coins = new List<Coin>();

    public List<Coin> Coins { get => _coins; }

    void Awake()
    {
        if (_factoryManager == null)
            _factoryManager = GenericSingleton<FactoryManager>.Instance;
    }

    public void MakeCoin(Vector3 pos)
    {
        int random = Random.Range(0, (int)ECoinType.Max);
        GameObject coin = _factoryManager.MakeObject<ECoinType, GameObject>((ECoinType)random);
        coin.transform.position = pos;
        _coins.Add(coin.GetComponent<Coin>());
    }

    public void DestroyCoin()
    {
        for (int i = 0; i < _coins.Count; i++)
        {
            if (_coins[i] != null)
                _coins[i].DestroyCoin();
        }
        _coins.Clear();
    }
}