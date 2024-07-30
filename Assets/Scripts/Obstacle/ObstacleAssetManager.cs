using System.Collections.Generic;
using UnityEngine;

public class ObstacleAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;

    List<IObstacleList> _worldList;
    public List<GameObject> Obstacles { get; }

    void AddWorldList()
    {
        _worldList.Add(new FirstWorldObstacleList());
        _worldList.Add(new SecondWorldObstacleList());
        _worldList.Add(new ThirdWorldObstacleList());
    }

    public void LoadAsset(EWorldType worldType)
    {
        if (_worldList == null)
            AddWorldList();
        _worldList[(int)worldType].AddObstacle(this, _addressableManager);
    }


    public void ReleaseAsset()
    {
        for (int i = 0; i < Obstacles.Count; i++)
            _addressableManager.Release(Obstacles[i]);
    }
}