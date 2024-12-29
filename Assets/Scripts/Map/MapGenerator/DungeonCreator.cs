using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

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

    MapAssetManager _mapAssetManager;
    FactoryManager _factoryManager;
    ObstacleAssetManager _obstacleAssetManager;
    ObjectPoolManager _objectPoolManager;

    WallCreator _wallCreator;
    DoorCreator _doorCreator;
    MeshCreator _meshCreator;
    ObstacleCreator _obstacleCreator;

    PlayerSpawn _playerSpawn;
    GameObject _deadZone;

    public List<IMap> Maps {  get => _maps; }
    public Dictionary<int, HashSet<int>> HorizontalDoorPos { get; set; }
    public Dictionary<int, HashSet<int>> VerticalDoorPos { get; set; }

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

    void InitCreator()
    {
        _wallCreator = new WallCreator();
        _wallCreator.Init(_wallWidth, _doorWidth, _doorThickness, this);
        _doorCreator = new DoorCreator();
        _doorCreator.Init(this);
        _meshCreator = new MeshCreator();
        _meshCreator.Init(_wallCreator);
        _obstacleCreator = new ObstacleCreator();
        _obstacleCreator.Init(_minObstacle, _maxObstacle);
    }

    void CreateDungeon()
    {
        InitCreator();
        DestroyAllChildren();
        CreateDeadZone();

        DungeonGenerator generator = new DungeonGenerator(_dungeonWidth, _dungeonLength);
        var listOfRooms = generator.CalculateDungeon(_maxIterations, _roomWidthMin, _roomLengthMin,
                                                     _roomBottomCornerModifier, _roomTopCornerModifier, _roomOffset);
        var listOfCorridors = generator.CalculateCorridors(_corridorWidth, _doorWidth);

        CreateCorridor(listOfCorridors);
        CreateRoom(listOfRooms);
    }

    void CreateCorridor(List<Node> corridors)
    {
        for (int i = 0; i < corridors.Count; i++)
        {
            _doorCreator.CreateDoors(corridors[i].BottomLeftAreaCorner, corridors[i].TopRightAreaCorner);
            _meshCreator.CreateMesh(corridors[i].BottomLeftAreaCorner, corridors[i].TopRightAreaCorner, true);
        }
    }

    void CreateRoom(List<Node> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject room = _meshCreator.CreateMesh(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, false);
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
                    _obstacleCreator.CreateObstacle(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, room.transform);
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
                _obstacleCreator.CreateObstacle(rooms[i].BottomLeftAreaCorner, rooms[i].TopRightAreaCorner, room.transform);
            }
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

    void DestroyAllChildren()
    {
        GenericSingleton<UIManager>.Instance.DestroyUI();
        if (_playerSpawn != null)
            _playerSpawn.DestroyPlayer();
        DestroyGroundWork();
        DestroyeventRoom();
        DestroyMap();
        GenericSingleton<CoinManager>.Instance.DestroyCoin();
        _obstacleCreator.DestroyObstacle();

        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
                DestroyImmediate(item.gameObject);
        }
    }
}