using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    //문
    //문은 도어리스트 자식으로
    public int _dungeonWidth;   // 맵 가로 길이
    public int _dungeonLength;  // 맵 세로 길이
    public int _roomWidthMin;   // 방 최소 가로 길이
    public int _roomLengthMin;  // 방 최소 세로 길이
    public int _maxIterations;  // 최대 반복?
    public Material _material;

    public GameObject _wallVertical;
    public GameObject _wallHorizontal;
    public int _wallWidth;
    public GameObject _doorVertical;
    public GameObject _doorHorizontal;
    public GameObject _enemyController;
    public int _enemyControllerOffset;

    [Range(5, 15)]
    public int _corridorWidth;  // 복도 넓이
    [Range(0.0f, 0.3f)] //슬라이더 범위 지정
    public float _roomBottomCornerModifier;
    [Range(0.7f, 1.0f)] //슬라이더 범위 지정
    public float _roomTopCornerModifier;
    [Range(0, 10)]       //슬라이더 범위 지정
    public int _roomOffset;

    List<Vector3Int> _possibleWallVerticalPosition;
    List<Vector3Int> _possibleWallHorizontalPosition;
    List<Vector3Int> _possibleDoorVerticalPosition;
    List<Vector3Int> _possibleDoorHorizontalPosition;

    PlayerSpawn _playerSpawn;

    void Start()
    {
        CreateDungeon();
    }

    public void CreateDungeon()
    {
        DestroyAllChildren();
        DungeonGenerator generator = new DungeonGenerator(_dungeonWidth, _dungeonLength);
        var listOfRooms = generator.CalculateDungeon(_maxIterations, _roomWidthMin, _roomLengthMin,
                                                      _roomBottomCornerModifier, _roomTopCornerModifier,
                                                      _roomOffset);

        var listOfCorridors = generator.CalculateCorridors(_corridorWidth);

        GameObject wallParent = new GameObject("wallParent");
        wallParent.transform.parent = transform;
        _possibleWallVerticalPosition = new List<Vector3Int>();
        _possibleDoorVerticalPosition = new List<Vector3Int>();
        _possibleWallHorizontalPosition = new List<Vector3Int>();
        _possibleDoorHorizontalPosition = new List<Vector3Int>();

        DoorList doorList = CreateDoorList(transform);

        for (int i = 0; i < listOfRooms.Count; i++)     // 방 생성
        {
            GameObject room = CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, i);
            Vector3 createPos = CalculateCreatePosition(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);

            if (i == 0)
            {
                CreatePlayerSpawnPos(createPos, room);
            }
            else if (i == listOfRooms.Count - 1)
            {
                CreatePortal(createPos, room);
            }
            else
            {
                CreateEnemyController(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room, doorList);
            }
        }

        for (int i = 0; i < listOfCorridors.Count; i++) // 복도 생성
        {
            CreateMesh(listOfCorridors[i].BottomLeftAreaCorner, listOfCorridors[i].TopRightAreaCorner, i);
        }
        CreateWalls(wallParent);

        for (int i = 0; i < listOfCorridors.Count; i++)  // 문 생성, 벽 생성 후 하기 위해서
        {
            CreateDoors2(listOfCorridors[i].BottomLeftAreaCorner, listOfCorridors[i].TopRightAreaCorner, doorList.gameObject);
        }
        CreateDoors(doorList.gameObject);
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
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _material;
        dungeonFloor.AddComponent<MeshCollider>();
        dungeonFloor.tag = "Ground";
        dungeonFloor.transform.parent = transform;

        for (int row = (int)bottomLeft.x; row < (int)bottomRight.x; row += _wallWidth)
        {
            var wallPosition = new Vector3(row, 0, bottomLeft.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition, _possibleDoorHorizontalPosition);
        }
        for (int row = (int)topLeft.x; row < (int)topRightCorner.x; row += _wallWidth)
        {
            var wallPosition = new Vector3(row, 0, topRight.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition, _possibleDoorHorizontalPosition);
        }

        for (int col = (int)bottomLeft.z; col < (int)topLeft.z; col += _wallWidth)
        {
            var wallPosition = new Vector3(bottomLeft.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition, _possibleDoorVerticalPosition);
        }
        for (int col = (int)bottomRight.z; col < (int)topRight.z; col += _wallWidth)
        {
            var wallPosition = new Vector3(bottomRight.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition, _possibleDoorVerticalPosition);
        }
        return dungeonFloor;
    }

    void CreatePlayerSpawnPos(Vector3 createPos, GameObject parent)
    {
        GameObject playerSpawnPos = Resources.Load("Prefabs/PlayerSpawnPos") as GameObject;
        GameObject temp = Instantiate(playerSpawnPos, parent.transform);
        temp.transform.position = createPos;
        _playerSpawn = temp.GetComponent<PlayerSpawn>();
        _playerSpawn.Spawn();
    }

    void CreatePortal(Vector3 createPos, GameObject parent)
    {
        GameObject portal = Resources.Load("Prefabs/Portal") as GameObject;
        GameObject temp = Instantiate(portal, parent.transform);
        temp.transform.position = createPos;
        temp.GetComponent<Portal>().SetPlayer(_playerSpawn.GetPlayer());
    }

    void CreateEnemyController(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject parent, DoorList doorList)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);
        Vector3 createPos = CalculateCreatePosition(bottomLeftCorner, topRightCorner);
        GameObject temp = Instantiate(_enemyController, parent.transform);
        temp.transform.position = createPos;
        temp.GetComponent<BoxCollider>().size = new Vector3((topRight.x - bottomLeft.x) - _enemyControllerOffset, 7, (topRight.z - bottomLeft.z) - _enemyControllerOffset);
        temp.GetComponent<EnemyController>().Init(createPos, doorList);
    }

    DoorList CreateDoorList(Transform parent)
    {
        GameObject temp = new GameObject("DoorList");
        temp.transform.parent = parent;
        temp.AddComponent<DoorList>();
        return temp.GetComponent<DoorList>();
    }

    void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList, List<Vector3Int> doorList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPosition);
        if (wallList.Contains(point))
        {
            doorList.Add(point);
            wallList.Remove(point);
        }
        else
        {
            wallList.Add(point);
        }
    }

    void CreateWalls(GameObject wallParent)
    {
        foreach (var wallPosition in _possibleWallHorizontalPosition)
            CreateWall(wallParent, wallPosition, _wallHorizontal);

        foreach (var wallPosition in _possibleWallVerticalPosition)
            CreateWall(wallParent, wallPosition, _wallVertical);
    }

    void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject wallPrefab)
    {
        Instantiate(wallPrefab, wallPosition, Quaternion.identity, wallParent.transform);
    }

    void CreateDoors(GameObject doorParent)
    {
        foreach (var doorPosition in _possibleDoorHorizontalPosition)
            CreateDoor(doorParent, doorPosition, _doorHorizontal);
        foreach (var doorPosition in _possibleDoorVerticalPosition)
            CreateDoor(doorParent, doorPosition, _doorVertical);
    }

    void CreateDoor(GameObject doorParent, Vector3Int doorPosition, GameObject doorPrefab)
    {
        GameObject door = Instantiate(doorPrefab, doorPosition, Quaternion.identity, doorParent.transform);
        doorParent.GetComponent<DoorList>().AddDoor(door);
    }

    void CreateDoors2(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject doorParent)
    {
        if ((topRightCorner.x - bottomLeftCorner.x) < (topRightCorner.y - bottomLeftCorner.y))  // z축이 긴 경우
        {
            Vector3 createBottomPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, 0, bottomLeftCorner.y);
            Vector3 createTopPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, 0, topRightCorner.y);
            //CreateDoor2(createBottomPos, createTopPos, _doorHorizontal, doorParnet);
        }
        else // x축이 긴 경우
        {
            Vector3 createLeftPos = new Vector3(bottomLeftCorner.x, 0, (bottomLeftCorner.y + topRightCorner.y) / 2);
            Vector3 createRightPos = new Vector3(topRightCorner.x, 0, (bottomLeftCorner.y + topRightCorner.y) / 2);
            //CreateDoor2(createLeftPos, createRightPos, _doorVertical, doorParent);
        }
    }

    void CreateDoor2()
    {

    }

    Vector3 CalculateCreatePosition(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);
        return (bottomLeft + topRight) / 2;
    }

    void DestroyAllChildren()
    {
        GenericSingleton<UIManager>.GetInstance().DestroyUI();
        if (_playerSpawn != null)
            _playerSpawn.DestroyPlayer();

        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
                DestroyImmediate(item.gameObject);
        }
    }
}
