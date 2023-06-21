using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleListBase : MonoBehaviour
{
    protected List<GameObject> _obstacles = new List<GameObject>();
    public abstract List<GameObject> AddObstacleList();
}

public enum EFirstWorldObstacleType
{
    Battery,
    Capacitor,
    Container,
    Generator,
    Projector,
    Rotor,
    Cube_Big,
    Cube_Small,
    Max
}

public enum ESecondWorldObstacleType
{

}

public enum EThirdWorldObstacleType
{

}
