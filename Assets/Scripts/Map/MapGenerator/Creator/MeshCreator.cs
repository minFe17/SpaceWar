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
        dungeonFloor.GetComponent<MeshFilter>().mesh = mesh;
        dungeonFloor.GetComponent<MeshRenderer>().material = _mapAssetManager.FloorMaterial;
        dungeonFloor.AddComponent<MeshCollider>();
        dungeonFloor.AddComponent<BoxCollider>().isTrigger = true;
        dungeonFloor.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dungeonFloor.GetComponent<Rigidbody>().isKinematic = true;
        dungeonFloor.tag = "Ground";
    }
}