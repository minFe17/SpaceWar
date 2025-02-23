using UnityEngine;

public class GoldCoinFactory : CoinFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _coinType = ECoinType.GoldCoin;
        _factoryManager.AddFactorys(_coinType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject coin = _objectPoolManager.Push(_coinType, _prefab);
        coin.GetComponentInChildren<Coin>().Init();
        return coin;
    }
}