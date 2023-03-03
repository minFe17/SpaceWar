using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public int _dungeonWidth;   // 맵 가로 길이
    public int _dungeonLength;  // 맵 세로 길이
    public int _roomWidthMin;   // 방 최소 가로 길이
    public int _roomLengthMin;  // 방 최소 세로 길이
    public int _maxIterations;  // 최대 반복?
    public int _corridorWidth;  // 복도 길이?

    void Start()
    {
        CreateDungeon();
    }


    void Update()
    {

    }

    void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator();
        var listoOfRooms = generator.CalculateRooms(_maxIterations, _roomWidthMin, _roomLengthMin);
    }
}
