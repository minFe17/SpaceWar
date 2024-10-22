using UnityEngine;

public class SliverCoinFactory : CoinFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _coinType = ECoinType.SliverCoin;
        _factoryManager.AddFactorys(_coinType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject coin = _objectPoolManager.Push(_coinType, _prefab);
        coin.GetComponent<Coin>().Init();
        return coin;
    }
}