using UnityEngine;
using Utils;

public class Wall : MonoBehaviour, IMap
{
    [SerializeField] EMapPoolType _mapPoolType;

    ObjectPoolManager _objectPoolManager;

    void Start()
    {
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
    }

    void IMap.DestroyMap()
    {
        ChangeWallSize(Vector3.one);
        _objectPoolManager.Pull(_mapPoolType, gameObject);
    }

    public void ChangeWallSize(Vector3 size)
    {
        transform.localScale = size;
    }
}