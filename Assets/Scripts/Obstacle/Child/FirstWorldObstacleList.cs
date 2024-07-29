using UnityEngine;

public class FirstWorldObstacleList : IObstacleList
{
    async void IObstacleList.AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)EFirstWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"FirstWorld/{(EFirstWorldObstacleType)i}");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }
}