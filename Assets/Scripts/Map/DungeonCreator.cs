using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public int _dungeonWidth;   // �� ���� ����
    public int _dungeonLength;  // �� ���� ����
    public int _roomWidthMin;   // �� �ּ� ���� ����
    public int _roomLengthMin;  // �� �ּ� ���� ����
    public int _maxIterations;  // �ִ� �ݺ�?
    public int _corridorWidth;  // ���� ����?

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
