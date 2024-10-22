using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    protected FactoryManager _factoryManager;
    protected ObjectPoolManager _objectPoolManager;
    protected GameObject _prefab;
}