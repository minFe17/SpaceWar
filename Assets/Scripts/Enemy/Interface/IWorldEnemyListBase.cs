using System.Threading.Tasks;

interface IWorldEnemyListBase
{
    public Task AddEnemyList(EnemyManager enemyManager, AddressableManager addressableManager);
    public void ReleaseAsset();
}