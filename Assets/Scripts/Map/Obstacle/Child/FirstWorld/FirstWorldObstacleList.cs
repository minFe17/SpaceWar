using System;
using System.Threading.Tasks;
using UnityEngine;

public class FirstWorldObstacleList : IObstacleList
{
    async Task IObstacleList.AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)EFirstWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"FirstWorld/{(EFirstWorldObstacleType)i}.prefab");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }

    void IObstacleList.MakePool(ObstacleObjectPool obstacleObjectPool)
    {
        obstacleObjectPool.CreatePool<EFirstWorldObstacleType>();
    }

    Enum IObstacleList.ConvertEnumToInt(int value)
    {
        return (EFirstWorldObstacleType)value;
    }
}