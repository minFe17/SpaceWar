using UnityEngine;

public class SilverCoinCoinFactory : CoinFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _coinType = ECoinType.SilverCoin;
        _factoryManager.AddFactorys(_coinType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject coin = _objectPoolManager.Push(_coinType, _prefab);
        coin.GetComponent<Coin>().Init();
        return coin;
    }
}