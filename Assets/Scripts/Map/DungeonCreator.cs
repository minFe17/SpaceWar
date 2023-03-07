using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    // �����ϰ� ������ ��
    // �ٴ�, ����, �� ���� = ���ӿ�����Ʈ�� Ÿ��ó��
    // �ٴ� �ּ� ���� = �ٴ�ũ�� * ���� ���� or ���� ����
    // ���� ����, ���� = ����ũ�� * ���� ����
    // �� ���� = �� ũ�� * ������

    // �ٴ�, ����, �� ���� �� ���� ��ġ�� ������Ʈ ��ġ + ũ�� -> ���� ��ġ�� Ÿ��ó�� ������
    
    // ���� �� Room1ó�� ���� -> �� �θ� �ȿ� ��, ��Ʋ�ʵ�, EnemyController �ֱ�
    // �� �� ��� �Ŵ� �� �θ� �ȿ� �ִ� ����

    // �ٴ�, ���� = (6, 0, 6)
    // ��� = (2,0,2)
    // �� = (���� �� : 6, ���� �� : 2)
    // �� = (���� �� : 10, ���� �� : 2)
    
    // ���� ��ȯ ������ �߽� ���???
    //      1. �ٴ� ������ Ȧ���� �߰����� ����, �ٴ� ������ ¦���� �ٴ�ũ�� / 2�� �簢������ ����?
    //          ex) �ٴ� ũ�Ⱑ 2,2�� (1,1), (1, -1), (-1, 1), (-1, -1)
    //      2. �ٴ� ���� ���ؼ� /2 -> (�ٴ� ���� - �ٴ� ��) / 2     (�ٴ� ���� ������ �ʰ� ���⼭ �������ִ°�?)
    // ���� ���� ������ �ٴ� ���� or offset�־
    // Nav mesh Agent�� ���???


    public int _dungeonWidth;   // �� ���� ����
    public int _dungeonLength;  // �� ���� ����
    public int _roomWidthMin;   // �� �ּ� ���� ����
    public int _roomLengthMin;  // �� �ּ� ���� ����
    public int _maxIterations;  // �ִ� �ݺ�?
    public int _corridorWidth;  // ���� ����?
    public Material _material;

    [Range(0.0f, 0.3f)] //�����̴� ���� ����
    public float _roomBottomCornerModifier;
    [Range(0.7f, 1.0f)] //�����̴� ���� ����
    public float _roomTopCornerModifier;
    [Range(0, 2)]       //�����̴� ���� ����
    public int _roomOffset;

    public GameObject _wallVertical;
    public GameObject _wallHorizontal;

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
        var listoOfRooms = generator.CalculateDungeon(_maxIterations, _roomWidthMin, _roomLengthMin,
                                                      _roomBottomCornerModifier, _roomTopCornerModifier,
                                                      _roomOffset, _corridorWidth);

        GameObject wallParent = new GameObject("wallParent");
        wallParent.transform.parent = transform;
        _possibleWallVerticalPosition = new List<Vector3Int>();
        _possibleDoorVerticalPosition = new List<Vector3Int>();
        _possibleWallHorizontalPosition = new List<Vector3Int>();
        _possibleDoorHorizontalPosition = new List<Vector3Int>();

        for (int i = 0; i < listoOfRooms.Count; i++)
        {
            CreateMesh(listoOfRooms[i].BottomLeftAreaCorner, listoOfRooms[i].TopRightAreaCorner);
        }
        CreateWalls(wallParent);
    }

    void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
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

        GameObject dungeonFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));
        dungeonFloor.transform.position = Vector3.zero;
        dungeonFloor.transform.localScale = Vector3.one;
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _material;
        dungeonFloor.transform.parent = transform;

        for (int row = (int)bottomLeft.x; row < (int)bottomRight.x; row++)
        {
            var wallPosition = new Vector3(row, 0, bottomLeft.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition, _possibleDoorHorizontalPosition);
        }
        for (int row = (int)topLeft.x; row < (int)topRightCorner.x; row++)
        {
            var wallPosition = new Vector3(row, 0, topRight.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition, _possibleDoorHorizontalPosition);
        }

        for (int col = (int)bottomLeft.z; col < (int)topLeft.z; col++)
        {
            var wallPosition = new Vector3(bottomLeft.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition, _possibleDoorVerticalPosition);
        }
        for (int col = (int)bottomRight.z; col < (int)topRight.z; col++)
        {
            var wallPosition = new Vector3(bottomRight.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition, _possibleDoorVerticalPosition);
        }
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
        foreach(var wallPosition in _possibleWallHorizontalPosition)
            CreateWall(wallParent, wallPosition, _wallHorizontal);

        foreach(var wallPosition in _possibleWallVerticalPosition)
            CreateWall(wallParent, wallPosition, _wallVertical);
    }

    void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject wallPrefab)
    {
        Instantiate(wallPrefab, wallPosition, Quaternion.identity, wallParent.transform);
    }

    void DestroyAllChildren()
    {
        while(transform.childCount != 0)
        {
            foreach(Transform item in transform)
                DestroyImmediate(item.gameObject);
        }
    }
}
