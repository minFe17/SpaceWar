using System.Collections.Generic;

public class DungeonGenerator
{
    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    int _dungeonWidth;  
    int _dungeonLength;

    public DungeonGenerator()
    {

    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        // BinarySpacePartitioner 생성자 호출
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(_dungeonWidth, _dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        return new List<Node>(allSpaceNodes);
    }
}