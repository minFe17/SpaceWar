using System.Collections.Generic;

public class DungeonGenerator
{
    List<RoomNode> allNodesCollection = new List<RoomNode>();

    int _dungeonWidth;
    int _dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        _dungeonWidth = dungeonWidth;
        _dungeonLength = dungeonLength;
    }

    public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset, int corridorWidth)
    {
        // BinarySpacePartitioner 생성자 호출
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(_dungeonWidth, _dungeonLength);
        allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomWidthMin, roomLengthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpace(roomSpaces, roomBottomCornerModifier, roomTopCornerModifier, roomOffset);

        CorridorsGenerator corridorsGenerator = new CorridorsGenerator();
        var corridorList = corridorsGenerator.CreateCorridors(allNodesCollection, corridorWidth);

        return new List<Node>(roomList);
    }
}