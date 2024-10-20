using System.Threading.Tasks;

interface IObstacleList
{
    Task AddObstacle(ObstacleAssetManager obstacleAssetManager, AddressableManager addressableManager);
    void MakePool(ObstacleObjectPool obstacleObjectPool);
}