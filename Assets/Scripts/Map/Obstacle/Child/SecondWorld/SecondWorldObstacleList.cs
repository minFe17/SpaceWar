using System;
using System.Threading.Tasks;
using UnityEngine;

public class SecondWorldObstacleList : IObstacleList
{
    async Task IObstacleList.AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)ESecondWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"SecondWorld/{(ESecondWorldObstacleType)i}.prefab");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }

    void IObstacleList.MakePool(ObstacleObjectPool obstacleObjectPool)
    {
        obstacleObjectPool.CreatePool<ESecondWorldObstacleType>();
    }

    Enum IObstacleList.ConvertEnumToInt(int value)
    {
        return (ESecondWorldObstacleType)value;
    }
}