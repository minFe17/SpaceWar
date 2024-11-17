using System;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] int _doorThickness;
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

    List<GameObject> _enemyControllers = new List<GameObject>();
    List<EventRoom> _eventRooms = new List<EventRoom>();
    List<IMap> _maps = new List<IMap>();
    List<IObstacle> _obstacles = new List<IObstacle>();

    Dictionary<int, HashSet<int>> _horizontalDoorPos = new Dictionary<int, HashSet<int>>();
    Dictionary<int, HashSet<int>> _verticalDoorPos = new Dictionary<int, HashSet<int>>();

    GameObject _floorParent;
    GameObject _wallParent;
    GameObject _doorParent;

    MapAssetManager _mapAssetManager;
    FactoryManager _factoryManager;
    ObstacleAssetManager _obstacleAssetManager;
    ObjectPoolManager _objectPoolManager;
    WallCreator _wallCreator;

    PlayerSpawn _playerSpawn;
    GameObject _deadZone;

    public GameObject WallParent { get => _wallParent; }
    public List<IMap> Maps {  get => _maps; }
    public Dictionary<int, HashSet<int>> HorizontalDoorPos { get => _horizontalDoorPos; }
    public Dictionary<int, HashSet<int>> VerticalDoorPos { get => _verticalDoorPos; }

    void Start()
    {
        Init();
        CreateDungeon();
    }

    void Init()
    {
        if (_mapAssetManager == null)
            _mapAssetManager = GenericSingleton<MapAssetManager>.Instance;
        if (_factoryManager == null)
            _factoryManager = GenericSingleton<FactoryManager>.Instance;
        if (_obstacleAssetManager == null)
            _obstacleAssetManager = GenericSingleton<ObstacleAssetManager>.Instance;
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
    }

    void InitWallCreator()
    {
        _wallCreator = new WallCreator();
        _wallCreator.Init(_wallWidth, _doorWidth, _doorThickness, this);
    }

    void CreateParent()
    {
        _floorParent = new GameObject("MeshParent");
        _floorParent.transform.parent = transform;
        _wallParent = new GameObject("WallParent");
        _wallParent.transform.parent = transform;
        _doorParent = new GameObject("DoorParent");
        _doorParent.transform.parent = transform;
    }

    void CreateDungeon()
    {
        DestroyAllChildren();
        CreateDeadZone();
        CreateParent();
        InitWallCreator();

        DungeonGenerator generator = new DungeonGenerator(_dungeonWidth, _dungeonLength);
        var listOfRooms = generator.CalculateDungeon(_maxIterations, _roomWidthMin, _roomLengthMin,
                                                     _roomBottomCornerModifier, _roomTopCornerModifier, _roomOffset);
        var listOfCorridors = generator.CalculateCorridors(_corridorWidth);

        CreateCorridor(listOfCorridors);
        CreateRoom(listOfRooms);
    }

    void CreateCorridor(List<Node> corridors)
    {
        for (int i = 0; i < corridors.Count; i++)
        {
            CreateDoors(corridors[i].BottomLeftAreaCorner, corridors[i].TopRightAreaCorner);
            CreateMesh(corridors[i].BottomLeftAreaCorner, corridors[i].TopRightAreaCorner, true);
        }
    }

    void CreateRoom(List<Node> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject room = CreateMesh(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, false);
            Vector3 createPos = CalculateCreatePosition(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner);

            if (i == 0)
            {
                CreatePlayerSpawnPos(createPos, room);
                room.AddComponent<ClearRoom>();
            }
            else if (i == rooms.Count - 1)
            {
                if (GenericSingleton<GameManager>.Instance.LevelStage == 5)
                {
                    CreateEnemyController(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, room, true);
                    CreateObstacle(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, room.transform);
                }
                CreatePortal(createPos, room);
            }
            else if (rooms.Count / 2 == i)
            {
                CreateShop(createPos, room);
            }
            else
            {
                CreateEnemyController(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, room, false);
                CreateObstacle(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, room.transform);
            }
        }
    }

    GameObject CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner, bool isCorridor)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRight = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeft = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[] { topLeft, topRight, bottomLeft, bottomRight };

        Mesh mesh;
        InitMesh(out mesh, vertices);

        GameObject dungeonFloor = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        InitDungeonFloor(dungeonFloor, mesh);

        if (isCorridor)
            _wallCreator.CalculateCorridorWallPosition(bottomLeft, bottomRight, topLeft, topRight);
        else
            _wallCreator.CalculateRoomWallPosition(bottomLeft, bottomRight, topLeft, topRight);

        return dungeonFloor;
    }

    void InitMesh(out Mesh mesh, Vector3[] vertices)
    {
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);

        int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void InitDungeonFloor(GameObject dungeonFloor, Mesh mesh)
    {
        dungeonFloor.transform.position = Vector3.zero;
        dungeonFloor.transform.localScale = Vector3.one;
        dungeonFloor.transform.parent = _floorParent.transform;
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _mapAssetManager.FloorMaterial;
        dungeonFloor.AddComponent<MeshCollider>();
        dungeonFloor.AddComponent<BoxCollider>().isTrigger = true;
        dungeonFloor.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dungeonFloor.GetComponent<Rigidbody>().isKinematic = true;
        dungeonFloor.tag = "Ground";
    }

    void CreateObstacle(Vector2 bottomLeftCorner, Vector2 topRightCorner, Transform parent)
    {
        List<GameObject> obstacles = GenericSingleton<ObstacleAssetManager>.Instance.Obstacles;
        int obstacleCount = Random.Range(_minObstacle, _maxObstacle);
        for (int i = 0; i < obstacleCount; i++)
        {
            int random = Random.Range(0, obstacles.Count);
            Vector3 position = new Vector3(Random.Range(bottomLeftCorner.x + 10, topRightCorner.x - 10), 0, Random.Range(bottomLeftCorner.y + 10, topRightCorner.y - 10));
            Enum type = _obstacleAssetManager.ConvertEnumToInt(random);
            GameObject obstacle = _factoryManager.ObstacleFactory.MakeObject(type);
            obstacle.transform.position = position;
            obstacle.transform.parent = parent;
            _obstacles.Add(obstacle.GetComponent<IObstacle>());
        }
    }

    void CreatePlayerSpawnPos(Vector3 createPos, GameObject parent)
    {
        GameObject temp = Instantiate(_mapAssetManager.PlayerSpawnPos, parent.transform);
        temp.transform.position = createPos;
        _playerSpawn = temp.GetComponent<PlayerSpawn>();
        _playerSpawn.Spawn();
        GenericSingleton<PlayerDataManager>.Instance.SettingPlayerData();
    }

    void CreateShop(Vector3 createPos, GameObject parent)
    {
        GameObject shop = _factoryManager.MakeObject<EEventRoomType, GameObject>(EEventRoomType.VendingMachine);
        shop.transform.position = createPos;
        _eventRooms.Add(shop.GetComponent<EventRoom>());
    }

    void CreatePortal(Vector3 createPos, GameObject parent)
    {
        GameObject portal = _factoryManager.MakeObject<EEventRoomType, GameObject>(EEventRoomType.Portal);
        GenericSingleton<GameManager>.Instance.Portal = portal;
        portal.transform.position = createPos;
        _eventRooms.Add(portal.GetComponent<EventRoom>());
    }

    void CreateEnemyController(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject parent, bool isBossRoom)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);
        Vector3 createPos = CalculateCreatePosition(bottomLeftCorner, topRightCorner);
        GameObject enemyController = _factoryManager.MakeObject<EGroundWorkType, GameObject>(EGroundWorkType.EnemyController);
        enemyController.transform.position = createPos;
        enemyController.transform.parent = parent.transform;
        enemyController.GetComponent<BoxCollider>().size = new Vector3((topRight.x - bottomLeft.x) - _enemyControllerOffset, 7, (topRight.z - bottomLeft.z) - _enemyControllerOffset);
        enemyController.GetComponent<EnemyController>().Init(createPos, isBossRoom);
        _enemyControllers.Add(enemyController);
    }

    void CreateDoors(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        if ((topRightCorner.x - bottomLeftCorner.x) < (topRightCorner.y - bottomLeftCorner.y))
        {
            Vector3 createBottomPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, -0.2f, bottomLeftCorner.y);
            Vector3 createTopPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, -0.2f, topRightCorner.y);

            CreateDoor(EMapPoolType.HorizontalDoor, createBottomPos);
            CreateDoor(EMapPoolType.HorizontalDoor, createTopPos);

            AddDoorPosition(_horizontalDoorPos, (int)createBottomPos.z, (int)createBottomPos.x);
            AddDoorPosition(_horizontalDoorPos, (int)createTopPos.z, (int)createTopPos.x);
        }
        else
        {
            Vector3 createLeftPos = new Vector3(bottomLeftCorner.x, -0.2f, (bottomLeftCorner.y + topRightCorner.y) / 2);
            Vector3 createRightPos = new Vector3(topRightCorner.x, -0.2f, (bottomLeftCorner.y + topRightCorner.y) / 2);

            CreateDoor(EMapPoolType.VerticalDoor, createLeftPos);
            CreateDoor(EMapPoolType.VerticalDoor, createRightPos);

            AddDoorPosition(_verticalDoorPos, (int)createLeftPos.x, (int)createLeftPos.z);
            AddDoorPosition(_verticalDoorPos, (int)createRightPos.x, (int)createRightPos.z);
        }
    }

    void CreateDoor(Enum doorType, Vector3 pos)
    {
        GameObject door = _factoryManager.MapFactory.MakeObject((EMapPoolType)doorType);
        door.transform.position = pos;
        door.transform.parent = _doorParent.transform;
        _maps.Add(door.GetComponent<IMap>());
    }

    void AddDoorPosition(Dictionary<int, HashSet<int>> target, int key, int value)
    {
        if (!target.ContainsKey(key))
            target[key] = new HashSet<int>();
        target[key].Add(value);
    }

    Vector3 CalculateCreatePosition(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);
        return (bottomLeft + topRight) / 2;
    }

    void CreateDeadZone()
    {
        _deadZone = _factoryManager.MakeObject<EGroundWorkType, GameObject>(EGroundWorkType.DeadZone);
        _deadZone.transform.position = new Vector3(_dungeonWidth / 2, -10, _dungeonLength / 2);
        _deadZone.transform.localScale = new Vector3(_dungeonWidth + 50, 1, _dungeonLength + 50);
    }

    void DestroyGroundWork()
    {
        if (_deadZone != null)
            _objectPoolManager.Pull(EGroundWorkType.DeadZone, _deadZone);
        for (int i = 0; i < _enemyControllers.Count; i++)
            _objectPoolManager.Pull(EGroundWorkType.EnemyController, _enemyControllers[i]);
        _enemyControllers.Clear();
    }

    void DestroyeventRoom()
    {
        for (int i = 0; i < _eventRooms.Count; i++)
            _eventRooms[i].DestroyEventRoom();
        _eventRooms.Clear();
    }

    void DestroyMap()
    {
        for (int i = 0; i < _maps.Count; i++)
            _maps[i].DestroyMap();
        _maps.Clear();
    }

    void DestroyObstacle()
    {
        for (int i = 0; i < _obstacles.Count; i++)
            _obstacles[i].DestroyObstacle();
        _obstacles.Clear();
    }

    void DestroyAllChildren()
    {
        GenericSingleton<UIManager>.Instance.DestroyUI();
        if (_playerSpawn != null)
            _playerSpawn.DestroyPlayer();
        DestroyGroundWork();
        DestroyeventRoom();
        DestroyMap();
        DestroyObstacle();

        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
                DestroyImmediate(item.gameObject);
        }
    }
}