using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    RoomNode _rootNode;

    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    int _dungeonWidth;
    int _dungeonLength;

    public DungeonGenerator()
    {
    }

    internal object CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        throw new NotImplementedException();
    }
}