using System.Collections.Generic;

public class DungeonGenerator
{
    List<RoomNode> _allNodesCollection = new List<RoomNode>();

    int _dungeonWidth;
    int _dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        _dungeonWidth = dungeonWidth;
        _dungeonLength = dungeonLength;
    }

    public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(_dungeonWidth, _dungeonLength);
        _allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator();
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpace(roomSpaces, roomBottomCornerModifier, roomTopCornerModifier, roomOffset);

        return new List<Node>(roomList);
    }

    public List<Node> CalculateCorridors(int corridorWidth)
    {
        CorridorsGenerator corridorsGenerator = new CorridorsGenerator();
        var corridorList = corridorsGenerator.CreateCorridors(_allNodesCollection, corridorWidth);
        return corridorList;
    }
}