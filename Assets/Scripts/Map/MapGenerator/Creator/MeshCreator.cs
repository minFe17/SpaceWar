using UnityEngine;
using Utils;

public class MeshCreator : MonoBehaviour
{
    WallCreator _wallCreator;
    MapAssetManager _mapAssetManager;

    public void Init(WallCreator wallCreator)
    {
        _wallCreator = wallCreator;
        _mapAssetManager = GenericSingleton<MapAssetManager>.Instance;
    }
    
    public GameObject CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner, bool isCorridor)
    {
        // 2D 좌표를 3D 좌표로 변환
        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRight = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeft = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        // 4개의 꼭지점을 배열로 저장
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

    /// <summary>
    /// 주어진 정점 배열을 기반으로 사각형 메쉬를 초기화
    /// UV 맵핑 및 삼각형 인덱스를 설정하고, 법선 벡터를 자동으로 계산
    /// </summary>
    void InitMesh(out Mesh mesh, Vector3[] vertices)
    {
        // UV 좌표 설정: XZ 평면 기준으로 각 정점의 텍스처 맵핑 좌표 생성
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);

        // 삼각형 인덱스 설정: 정점 0,1,2 와 2,1,3 으로 두 개의 삼각형을 구성하여 사각형을 생성
        int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };

        mesh = new Mesh();
        mesh.vertices = vertices;      // 정점 위치 지정
        mesh.uv = uvs;                 // UV 맵핑 정보 지정
        mesh.triangles = triangles;   // 삼각형 인덱스 지정
        mesh.RecalculateNormals();    // 조명 처리를 위한 법선 벡터 자동 계산
    }

    void InitDungeonFloor(GameObject dungeonFloor, Mesh mesh)
    {
        dungeonFloor.transform.position = Vector3.zero;
        dungeonFloor.transform.localScale = Vector3.one;
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _mapAssetManager.FloorMaterial;
        dungeonFloor.AddComponent<MeshCollider>();
        dungeonFloor.AddComponent<BoxCollider>().isTrigger = true;
        dungeonFloor.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dungeonFloor.GetComponent<Rigidbody>().isKinematic = true;
        dungeonFloor.tag = "Ground";
    }
}