using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class ObstacleAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;

    List<IObstacleList> _worldList = new List<IObstacleList>();
    List<GameObject> _obstacles = new List<GameObject>();
    public List<GameObject> Obstacles { get => _obstacles; }

    void Awake()
    {
        if(_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
    }

    void AddWorldList()
    {
        _worldList.Add(new FirstWorldObstacleList());
        _worldList.Add(new SecondWorldObstacleList());
        _worldList.Add(new ThirdWorldObstacleList());
    }

    public async Task LoadAsset(EWorldType worldType)
    {
        if (_worldList.Count == 0)
            AddWorldList();
        await _worldList[(int)worldType].AddObstacle(this, _addressableManager);
    }


    public void ReleaseAsset()
    {   
        if (_obstacles.Count == 0)
            return;
        for (int i = 0; i < _obstacles.Count; i++)
            _addressableManager.Release(_obstacles[i]);
    }
}