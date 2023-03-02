using System;
using System.Collections.Generic;
using UnityEngine;

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
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(_dungeonWidth, _dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
    }
}