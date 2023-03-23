using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    //문, 몬스터 컨트롤러 설치
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

    [Range(5, 10)]
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

        Debug.Log("bottomLeft : " + bottomLeft);
        Debug.Log("bottomRight : " + bottomRight);
        Debug.Log("topLeft : " + topLeft);
        Debug.Log("topRight : " + topRight);

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
        foreach (var data in mesh.normals)
        {
            Debug.Log(data);
        }

        Debug.Log("Length" + mesh.normals.Length);

        GameObject dungeonFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));
        dungeonFloor.transform.position = Vector3.zero;
        dungeonFloor.transform.localScale = Vector3.one;
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _material;
        dungeonFloor.AddComponent<MeshCollider>();
        dungeonFloor.tag = "Ground";
        dungeonFloor.transform.parent = transform;

        for (int row = (int)bottomLeft.x; row < (int)bottomRight.x; row += 1* _wallWidth)
        {
            var wallPosition = new Vector3(row, 0, bottomLeft.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition, _possibleDoorHorizontalPosition);
        }
        for (int row = (int)topLeft.x; row < (int)topRightCorner.x; row += 1 * _wallWidth)
        {
            var wallPosition = new Vector3(row, 0, topRight.z);
            AddWallPositionToList(wallPosition, _possibleWallHorizontalPosition, _possibleDoorHorizontalPosition);
        }

        for (int col = (int)bottomLeft.z; col < (int)topLeft.z; col += 1 * _wallWidth)
        {
            var wallPosition = new Vector3(bottomLeft.x, 0, col);
            AddWallPositionToList(wallPosition, _possibleWallVerticalPosition, _possibleDoorVerticalPosition);
        }
        for (int col = (int)bottomRight.z; col < (int)topRight.z; col += 1 * _wallWidth)
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
