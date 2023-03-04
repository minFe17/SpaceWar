using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public int _dungeonWidth;   // 맵 가로 길이
    public int _dungeonLength;  // 맵 세로 길이
    public int _roomWidthMin;   // 방 최소 가로 길이
    public int _roomLengthMin;  // 방 최소 세로 길이
    public int _maxIterations;  // 최대 반복?
    public int _corridorWidth;  // 복도 길이?
    public Material _material;

    [Range(0.0f, 0.3f)] //슬라이더 범위 지정
    public float _roomBottomCornerModifier;
    [Range(0.7f, 1.0f)] //슬라이더 범위 지정
    public float _roomTopCornerModifier;
    [Range(0, 2)]       //슬라이더 범위 지정
    public int _roomOffset;

    void Start()
    {
        CreateDungeon();
    }

    void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(_dungeonWidth, _dungeonLength);
        var listoOfRooms = generator.CalculateDungeon(_maxIterations, _roomWidthMin, _roomLengthMin,
                                                      _roomBottomCornerModifier, _roomTopCornerModifier,
                                                      _roomOffset, _corridorWidth);
        for (int i = 0; i < listoOfRooms.Count; i++)
        {
            CreateMesh(listoOfRooms[i].BottomLeftAreaCorner, listoOfRooms[i].TopRightAreaCorner);
        }
    }

    void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRight = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeft = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeft,
            topRight,
            bottomLeft,
            bottomRight
        };

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
    }
}
