using UnityEngine;
using Utils;

public class CoinManager : MonoBehaviour
{
    // ╫л╠шео
    FactoryManager _factoryManager;

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
    }
}