using UnityEngine;

public class SecondWorldObstacleList : ObstacleListBase
{
    public override async void AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)ESecondWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"SecondWorld/{(ESecondWorldObstacleType)i}");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }
}