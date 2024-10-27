using UnityEngine;
using Utils;

public class SecondObstacle : MonoBehaviour, IObstacle
{
    [SerializeField] ESecondWorldObstacleType _obstacleType;

    ObjectPoolManager _objectPoolManager;

    void IObstacle.DestroyObstacle()
    {
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _objectPoolManager.Pull(_obstacleType, gameObject);
    }
}