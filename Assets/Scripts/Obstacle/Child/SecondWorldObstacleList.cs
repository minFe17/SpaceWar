using UnityEngine;

public class SecondWorldObstacleList : IObstacleList
{
    async void IObstacleList.AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)ESecondWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"SecondWorld/{(ESecondWorldObstacleType)i}");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }
}