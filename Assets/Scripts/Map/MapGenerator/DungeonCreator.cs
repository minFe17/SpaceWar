using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Random = UnityEngine.Random;

public class DungeonCreator : MonoBehaviour
{
    [SerializeField] int _dungeonWidth;
    [SerializeField] int _dungeonLength;
    [SerializeField] int _roomWidthMin;
    [SerializeField] int _roomLengthMin;
    [SerializeField] int _maxIterations;

    [SerializeField] int _wallWidth;
    [SerializeField] int _doorWidth;
    [SerializeField] int _enemyControllerOffset;

    [SerializeField] int _minObstacle;
    [SerializeField] int _maxObstacle;

    [Range(5, 15)]
    [SerializeField] int _corridorWidth;
    [Range(0.0f, 0.3f)]
    [SerializeField] float _roomBottomCornerModifier;
    [Range(0.7f, 1.0f)]
    [SerializeField] float _roomTopCornerModifier;
    [Range(6, 10)]
    [SerializeField] int _roomOffset;

    List<Vector3Int> _possibleWallVerticalPosition = new List<Vector3Int>();
    List<Vector3Int> _possibleWallHorizontalPosition = new List<Vector3Int>();

    Material _material;

    GameObject _wallHorizontal;
    GameObject _wallVertical;
    GameObject _doorVertical;
    GameObject _doorHorizontal;

    GameObject _enemyController;
    PlayerSpawn _playerSpawn;

    void Start()
    {
        Init();
        CreateDungeon();
    }

    void Init()
    {
        Scene scene = SceneManager.GetActiveScene();
        _material = Resources.Load($"Prefabs/Map/{scene.name}/FloorMaterial") as Material;
        _wallHorizontal = Resources.Load($"Prefabs/Map/{scene.name}/Wall/WallHorizontal") as GameObject;
        _wallVertical = Resources.Load($"Prefabs/Map/{scene.name}/Wall/WallVertical") as GameObject;
        _doorHorizontal = Resources.Load($"Prefabs/Map/{scene.name}/Door/DoorHorizontal") as GameObject;
        _doorVertical = Resources.Load($"Prefabs/Map/{scene.name}/Door/DoorVertical") as GameObject;
        _enemyController = Resources.Load($"Prefabs/EnemyController") as GameObject;
    }

    void CreateDungeon()
    {
        DestroyAllChildren();
        CreateDeadZone();
        DungeonGenerator generator = new DungeonGenerator(_dungeonWidth, _dungeonLength);
        var listOfRooms = generator.CalculateDungeon(_maxIterations, _roomWidthMin, _roomLengthMin,
                                                      _roomBottomCornerModifier, _roomTopCornerModifier, _roomOffset);

        var listOfCorridors = generator.CalculateCorridors(_corridorWidth);

        GameObject wallParent = new GameObject("wallParent");
        wallParent.transform.parent = transform;
        DoorList doorList = CreateDoorList();

        for (int i = 0; i < listOfRooms.Count; i++)
        {
            GameObject room = CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, i);
            Vector3 createPos = CalculateCreatePosition(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);

            if (i == 0)
            {
                CreatePlayerSpawnPos(createPos, room);
                room.AddComponent<ClearRoom>();
            }
            else if (i == listOfRooms.Count - 1)
            {
                if (GenericSingleton<GameManager>.Instance.LevelStage == 5)
                {
                    CreateEnemyController(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room, doorList, true);
                    CreateObstacle(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room.transform);
                }
                CreatePortal(createPos, room);
            }
            else if (listOfRooms.Count / 2 == i)
            {
                CreateShop(createPos, room);
            }
            else
            {
                CreateEnemyController(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room, doorList, false);
                CreateObstacle(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room.transform);
            }
        }

        for (int i = 0; i < listOfCorridors.Count; i++)
        {
            CreateMesh(listOfCorridors[i].BottomLeftAreaCorner, listOfCorridors[i].TopRightAreaCorner, i);
        }

        CreateWalls(wallParent);

        for (int i = 0; i < listOfCorridors.Count; i++)
        {
            CreateDoors(listOfCorridors[i].BottomLeftAreaCorner, listOfCorridors[i].TopRightAreaCorner, doorList.gameObject, wallParent);
        }

    }

    GameObject CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner, int j)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRight = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeft = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[] { topLeft, topRight, bottomLeft, bottomRight };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GameObject dungeonFloor = new GameObject("Mesh " + j, typeof(MeshFilter), typeof(MeshRenderer));
        dungeonFloor.transform.position = Vector3.zero;
        dungeonFloor.transform.localScale = Vector3.one;
        dungeonFloor.transform.parent = transform;
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _material;
        dungeonFloor.AddComponent<MeshCollider>();
        dungeonFloor.AddComponent<BoxCollider>().isTrigger = true;
        dungeonFloor.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dungeonFloor.GetComponent<Rigidbody>().isKinematic = true;
        dungeonFloor.tag = "Ground";

        CalculateWallPosition(bottomLeft, bottomRight, topLeft, topRight);

        return dungeonFloor;
    }

    void CreateObstacle(Vector2 bottomLeftCorner, Vector2 topRightCorner, Transform parent)
    {
        List<GameObject> obstacle = GenericSingleton<ObstacleManager>.Instance.Obstacle;
        int obstacleCount = Random.Range(_minObstacle, _maxObstacle);
        for (int i = 0; i < obstacleCount; i++)
        {
            int index = Random.Range(0, (int)EFirstWorldObstacleType.Max);
            GameObject temp = obstacle[index];
            Vector3 position = new Vector3(Random.Range(bottomLeftCorner.x + 10, topRightCorner.x - 10), 0, Random.Range(bottomLeftCorner.y + 10, topRightCorner.y - 10));
            Instantiate(temp, position, Quaternion.identity, parent);
        }
    }

    void CreatePlayerSpawnPos(Vector3 createPos, GameObject parent)
    {
        GameObject playerSpawnPos = Resources.Load("Prefabs/PlayerSpawnPos") as GameObject;
        GameObject temp = Instantiate(playerSpawnPos, parent.transform);
        temp.transform.position = createPos;
        _playerSpawn = temp.GetComponent<PlayerSpawn>();
        _playerSpawn.Spawn();
        GenericSingleton<PlayerDataManager>.Instance.SettingPlayerData();
    }

    void CreateShop(Vector3 createPos, GameObject parent)
    {
        GameObject temp = Resources.Load("Prefabs/VendingMachine") as GameObject;
        GameObject shop = Instantiate(temp, parent.transform);
        shop.transform.position = createPos;
    }

    void CreatePortal(Vector3 createPos, GameObject parent)
    {
        GameObject temp = Resources.Load("Prefabs/Portal") as GameObject;
        GameObject portal = Instantiate(temp, parent.transform);
        GenericSingleton<GameManager>.Instance.Portal = portal;
        portal.transform.position = createPos;
    }

    void CreateEnemyController(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject parent, DoorList doorList, bool isBossRoom)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);
        Vector3 createPos = CalculateCreatePosition(bottomLeftCorner, topRightCorner);
        GameObject temp = Instantiate(_enemyController, parent.transform);
        temp.transform.position = createPos;
        temp.GetComponent<BoxCollider>().size = new Vector3((topRight.x - bottomLeft.x) - _enemyControllerOffset, 7, (topRight.z - bottomLeft.z) - _enemyControllerOffset);
        temp.GetComponent<EnemyController>().Init(createPos, doorList, isBossRoom);
    }

    DoorList CreateDoorList()
    {
        GameObject temp = new GameObject("DoorList");
        temp.transform.parent = transform;
        return temp.AddComponent<DoorList>();
    }

    void CalculateWallPosition(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
    {
        for (int row = (int)bottomLeft.x; row < (int)bottomRight.x; row += _wallWidth)
        {
            var wallPosition = new Vector3(row, 0, bottomLeft.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition);
        }
        for (int row = (int)topLeft.x; row < (int)topRight.x; row += _wallWidth)
        {
            var wallPosition = new Vector3(row, 0, topRight.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition);
        }

        for (int col = (int)bottomLeft.z; col < (int)topLeft.z; col += _wallWidth)
        {
            var wallPosition = new Vector3(bottomLeft.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition);
        }
        for (int col = (int)bottomRight.z; col < (int)topRight.z; col += _wallWidth)
        {
            var wallPosition = new Vector3(bottomRight.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition);
        }
    }

    void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPosition);
        if (!wallList.Contains(point))
        {
            wallList.Add(point);
        }
    }

    void CreateWalls(GameObject wallParent)
    {
        foreach (var wallPosition in _possibleWallHorizontalPosition)
        {
            CreateWall(wallParent, wallPosition, _wallHorizontal);
        }

        foreach (var wallPosition in _possibleWallVerticalPosition)
        {
            CreateWall(wallParent, wallPosition, _wallVertical);
        }
    }

    void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject wallPrefab)
    {
        GameObject temp = Instantiate(wallPrefab, wallPosition, Quaternion.identity, wallParent.transform);
    }

    void CreateDoors(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject doorParent, GameObject wallParent)
    {
        if ((topRightCorner.x - bottomLeftCorner.x) < (topRightCorner.y - bottomLeftCorner.y))
        {
            Vector3 createBottomPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, -0.2f, bottomLeftCorner.y);
            Vector3 createTopPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, -0.2f, topRightCorner.y);
            CreateDoor(createBottomPos, createTopPos, _doorHorizontal, doorParent);
        }
        else
        {
            Vector3 createLeftPos = new Vector3(bottomLeftCorner.x, -0.2f, (bottomLeftCorner.y + topRightCorner.y) / 2);
            Vector3 createRightPos = new Vector3(topRightCorner.x, -0.2f, (bottomLeftCorner.y + topRightCorner.y) / 2);
            CreateDoor(createLeftPos, createRightPos, _doorVertical, doorParent);
        }
    }

    void CreateDoor(Vector3 doorPos1, Vector3 doorPos2, GameObject doorPrefab, GameObject doorParent)
    {
        GameObject firstDoor = Instantiate(doorPrefab, doorPos1, Quaternion.identity, doorParent.transform);
        GameObject secondDoor = Instantiate(doorPrefab, doorPos2, Quaternion.identity, doorParent.transform);
    }

    Vector3 CalculateCreatePosition(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);
        return (bottomLeft + topRight) / 2;
    }

    void CreateDeadZone()
    {
        GameObject deadZone = Resources.Load("Prefabs/DeadZone") as GameObject;
        GameObject temp = Instantiate(deadZone, transform);
        temp.transform.position = new Vector3(_dungeonWidth / 2, -10, _dungeonLength / 2);
        temp.transform.localScale = new Vector3(_dungeonWidth + 50, 1, _dungeonLength + 50);
    }

    void DestroyAllChildren()
    {
        GenericSingleton<UIManager>.Instance.DestroyUI();
        if (_playerSpawn != null)
            _playerSpawn.DestroyPlayer();

        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
                DestroyImmediate(item.gameObject);
        }
    }
}