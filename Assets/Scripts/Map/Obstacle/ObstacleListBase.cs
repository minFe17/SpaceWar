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
    Bone,
    Cactus,
    Cactus_01,
    Cactus_02,
    Rock,
    Rock_02,
    Rock_03,
    Tree,
    Tree_02,
    Well,
    Max
}

public enum EThirdWorldObstacleType
{

}
