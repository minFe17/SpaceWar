using UnityEngine;

public abstract class ObstacleListBase : MonoBehaviour
{
    public abstract void AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager);
}