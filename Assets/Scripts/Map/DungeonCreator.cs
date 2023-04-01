using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    //��
    //���� �����Ʈ �ڽ�����
    // 0��° ���� �÷��̾� ���� ��, ������ ���� ��Ż ��
    public int _dungeonWidth;   // �� ���� ����
    public int _dungeonLength;  // �� ���� ����
    public int _roomWidthMin;   // �� �ּ� ���� ����
    public int _roomLengthMin;  // �� �ּ� ���� ����
    public int _maxIterations;  // �ִ� �ݺ�?
    public Material _material;

    public GameObject _wallVertical;
    public GameObject _wallHorizontal;
    public int _wallWidth;
    public GameObject _door;
    public GameObject _enemyController;
    public int _enemyControllerOffset;


    [Range(5, 10)]
    public int _corridorWidth;  // ���� ����
    [Range(0.0f, 0.3f)] //�����̴� ���� ����
    public float _roomBottomCornerModifier;
    [Range(0.7f, 1.0f)] //�����̴� ���� ����
    public float _roomTopCornerModifier;
    [Range(0, 10)]       //�����̴� ���� ����
    public int _roomOffset;


    List<Vector3Int> _possibleWallVerticalPosition;
    List<Vector3Int> _possibleWallHorizontalPosition;
    List<Vector3Int> _possibleDoorVerticalPosition;
    List<Vector3Int> _possibleDoorHorizontalPosition;

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

        for (int i = 0; i < listOfRooms.Count; i++)     //�� ����
        {
            GameObject room = CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, i);
            DoorList doorList = CreateDoorList(room);
            if (i == 0)
            {
                CreatePlayerSpawnPos(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room);
            }
            else if (i == listOfRooms.Count - 1)
            {
                CreatePortal(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room);
            }
            else
            {
                CreateEnemyController(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner, room, doorList);

            }
        }

        for (int i = 0; i < listOfCorridors.Count; i++) //���� ����
        {
            CreateMesh(listOfCorridors[i].BottomLeftAreaCorner, listOfCorridors[i].TopRightAreaCorner, i);
        }

        CreateWalls(wallParent);
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

    void CreatePlayerSpawnPos(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject parent)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        GameObject playerSpawnPos = Resources.Load("Prefabs/PlayerSpawnPos") as GameObject;
        GameObject temp = Instantiate(playerSpawnPos, parent.transform);
        temp.transform.position = (bottomLeft + topRight) / 2;
        temp.GetComponent<PlayerSpawn>().Spawn();
    }

    void CreatePortal(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject parent)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        GameObject portal = Resources.Load("Prefabs/Portal") as GameObject;
        GameObject temp = Instantiate(portal, parent.transform);
        temp.transform.position = (bottomLeft + topRight) / 2;
    }

    void CreateEnemyController(Vector2 bottomLeftCorner, Vector2 topRightCorner, GameObject parent, DoorList doorList)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        GameObject temp = Instantiate(_enemyController, parent.transform);
        temp.transform.position = (bottomLeft + topRight) / 2;
        temp.GetComponent<BoxCollider>().size = new Vector3((topRight.x - bottomLeft.x) - _enemyControllerOffset, 7, (topRight.z - bottomLeft.z) - _enemyControllerOffset);
        temp.GetComponent<EnemyController>().Init(doorList);
    }

    DoorList CreateDoorList(GameObject parent)
    {
        GameObject temp = new GameObject("DoorList");
        temp.transform.SetParent(parent.transform);
        temp.AddComponent<DoorList>();
        CreateDoor();   //�� ����
        return temp.GetComponent<DoorList>();
    }

    void CreateDoor()
    {

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

    void DestroyAllChildren()
    {
        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
                DestroyImmediate(item.gameObject);
        }
    }
}
