using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ObstacleManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    List<ObstacleListBase> _worldList = new List<ObstacleListBase>();
    List<GameObject> _obstacle = new List<GameObject>();
    public List<GameObject> Obstacle
    {
        get
        {
            if (_obstacle.Count == 0)
            {
                WorldObstacleList();
            }
            return _obstacle;
        }
    }

    public void WorldObstacleList()
    {
        if (_worldList.Count == 0)
            AddWorldList();
        Scene scene = SceneManager.GetActiveScene();
        int stage = GenericSingleton<GameManager>.Instance.MapStage - 1;
        _obstacle = _worldList[stage].AddObstacleList();
    }

    public void ClearWorldObstacleList()
    {
        _worldList.Clear();
    }

    void AddWorldList()
    {
        _worldList.Add(new FirstWorldObstacleList());
        _worldList.Add(new SecondWorldObstacleList());
        _worldList.Add(new ThirdWorldObstacleList());
    }
}