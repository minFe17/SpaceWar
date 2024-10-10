using System.Threading.Tasks;
using UnityEngine;

public class ThirdWorldObstacleList : IObstacleList
{
    async Task IObstacleList.AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager)
    {
        for (int i = 0; i < (int)EThirdWorldObstacleType.Max; i++)
        {
            GameObject temp = await addressableManager.GetAddressableAsset<GameObject>($"ThirdWorld/{(EThirdWorldObstacleType)i}.prefab");
            obstacleAssetManager.Obstacles.Add(temp);
        }
    }
}