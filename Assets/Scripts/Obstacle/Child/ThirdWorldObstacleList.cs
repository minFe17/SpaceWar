using UnityEngine;

public class ThirdWorldObstacleList : IObstacleList
{
    async void IObstacleList.AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)EThirdWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"ThirdWorld/{(EThirdWorldObstacleType)i}");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }
}