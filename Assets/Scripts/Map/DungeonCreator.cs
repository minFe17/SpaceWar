using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    // 수정하고 생각할 것
    // 바닥, 복도, 벽 생성 = 게임오브젝트로 타일처럼
    // 바닥 최소 넓이 = 바닥크기 * 가로 갯수 or 세로 갯수
    // 복도 길이, 넓이 = 복도크기 * 복도 갯수
    // 벽 길이 = 벽 크기 * 벽갯수

    // 바닥, 복도, 벽 만들 때 지금 설치한 오브젝트 위치 + 크기 -> 다음 위치에 타일처럼 생성됨
    
    // 방을 다 Room1처럼 생성 -> 방 부모 안에 문, 배틀필드, EnemyController 넣기
    // 방 문 잠글 거는 방 부모 안에 있는 문들

    // 바닥, 복도 = (6, 0, 6)
    // 기둥 = (2,0,2)
    // 벽 = (넓은 쪽 : 6, 좁은 쪽 : 2)
    // 문 = (넓은 쪽 : 10, 좁은 쪽 : 2)
    
    // 몬스터 소환 범위는 중심 어떻게???
    //      1. 바닥 갯수가 홀수면 중간에서 생성, 바닥 갯수가 짝수면 바닥크기 / 2로 사각형들을 생성?
    //          ex) 바닥 크기가 2,2면 (1,1), (1, -1), (-1, 1), (-1, -1)
    //      2. 바닥 넓이 구해서 /2 -> (바닥 시작 - 바닥 끝) / 2     (바닥 넓이 구하지 않고 여기서 전달해주는건?)
    // 몬스터 소한 범위는 바닥 넓이 or offset넣어서
    // Nav mesh Agent는 어떻게???


    public int _dungeonWidth;   // 맵 가로 길이
    public int _dungeonLength;  // 맵 세로 길이
    public int _roomWidthMin;   // 방 최소 가로 길이
    public int _roomLengthMin;  // 방 최소 세로 길이
    public int _maxIterations;  // 최대 반복?
    public int _corridorWidth;  // 복도 넓이?
    public Material _material;

    [Range(0.0f, 0.3f)] //슬라이더 범위 지정
    public float _roomBottomCornerModifier;
    [Range(0.7f, 1.0f)] //슬라이더 범위 지정
    public float _roomTopCornerModifier;
    [Range(0, 2)]       //슬라이더 범위 지정
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
