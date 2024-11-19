using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class WallCreator : MonoBehaviour
{
    int _wallWidth;
    int _doorWidth;
    int _doorThickness;

    DungeonCreator _dungeonCreator;
    FactoryManager _factoryManager;

    public void Init(int wallWidth, int doorWidth, int doorThickness, DungeonCreator dungeonCreator)
    {
        _wallWidth = wallWidth;
        _doorWidth = doorWidth;
        _doorThickness = doorThickness;
        _dungeonCreator = dungeonCreator;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
    }

    void HorizontalRoomWall(Vector3 leftPos, Vector3 rightPos)
    {
        if (CheckDoor(out int doorPos, _dungeonCreator.HorizontalDoorPos, (int)leftPos.z, (int)leftPos.x, (int)rightPos.x))
        {
            int pos1 = doorPos - (_doorWidth / 2) - (_wallWidth / 2);
            int pos2 = doorPos + (_doorWidth / 2) + (_wallWidth / 2);

            for (int row = pos1; row >= (int)leftPos.x - _wallWidth; row -= _wallWidth)
            {
                Vector3 wallPos = new Vector3(row, 0, leftPos.z);
                CreateWall(wallPos, EMapPoolType.HorizontalWall);
            }
            for (int row = pos2; row <= (int)rightPos.x + _wallWidth; row += _wallWidth)
            {
                Vector3 wallPos = new Vector3(row, 0, rightPos.z);
                CreateWall(wallPos, EMapPoolType.HorizontalWall);
            }
        }
        else
        {
            for (int row = (int)leftPos.x; row <= (int)rightPos.x + _wallWidth; row += _wallWidth)
            {
                var wallPos = new Vector3(row, 0, leftPos.z);
                CreateWall(wallPos, EMapPoolType.HorizontalWall);
            }
        }
    }

    void VerticalRoomWall(Vector3 bottomPos, Vector3 topPos)
    {
        if (CheckDoor(out int doorPos, _dungeonCreator.VerticalDoorPos, (int)bottomPos.x, (int)bottomPos.z, (int)topPos.z))
        {
            int pos1 = doorPos - (_doorWidth / 2) - (_wallWidth / 2);
            int pos2 = doorPos + (_doorWidth / 2) + (_wallWidth / 2);

            for (int col = pos1; col >= (int)bottomPos.z - _wallWidth; col -= _wallWidth)
            {
                Vector3 wallPos = new Vector3(bottomPos.x, 0, col);
                CreateWall(wallPos, EMapPoolType.VerticalWall);
            }
            for (int col = pos2; col <= (int)topPos.z + _wallWidth; col += _wallWidth)
            {
                Vector3 wallPos = new Vector3(topPos.x, 0, col);
                CreateWall(wallPos, EMapPoolType.VerticalWall);
            }
        }
        else
        {
            for (int col = (int)bottomPos.z; col <= (int)topPos.z + _wallWidth; col += _wallWidth)
            {
                var wallPos = new Vector3(bottomPos.x, 0, col);
                CreateWall(wallPos, EMapPoolType.VerticalWall);
            }
        }
    }

    void HorizontalCorridorWall(Vector3 leftPos, Vector3 rightPos)
    {
        int curPos = (int)leftPos.x + (_doorThickness / 2) + (_wallWidth / 2);
        int totalWallLength = (int)leftPos.x + (_doorThickness / 2) + _wallWidth;
        int targetSize = (int)rightPos.x - (_doorThickness / 2);

        while (totalWallLength <= targetSize)
        {
            Vector3 createPos = new Vector3(curPos, 0, leftPos.z);
            CreateWall(createPos, EMapPoolType.HorizontalWall);
            if (totalWallLength + _wallWidth > targetSize)
                break;
            curPos += _wallWidth;
            totalWallLength += _wallWidth;
        }

        if (targetSize - totalWallLength > 0)
        {
            float wallSize = targetSize - totalWallLength;
            float wallPos = totalWallLength + (wallSize / 2);
            float wallScale = 1 / (float)_wallWidth * wallSize;
            Vector3 createPos = new Vector3(wallPos, 0, leftPos.z);
            Vector3 cresteSize = new Vector3(wallScale, 1, 1);
            CreateWall(createPos, EMapPoolType.HorizontalWall, cresteSize);
        }
    }

    void VerticalCorridorWall(Vector3 bottomPos, Vector3 topPos)
    {
        int curPos = (int)bottomPos.z + (_doorThickness / 2) + (_wallWidth / 2);
        int totalWallLength = (int)bottomPos.z + (_doorThickness / 2) + _wallWidth;
        int targetSize = (int)topPos.z - (_doorThickness / 2);

        while (totalWallLength <= targetSize)
        {
            Vector3 createPos = new Vector3(bottomPos.x, 0, curPos);
            CreateWall(createPos, EMapPoolType.VerticalWall);
            if (totalWallLength + _wallWidth > targetSize)
                break;
            curPos += _wallWidth;
            totalWallLength += _wallWidth;
        }

        if (targetSize - totalWallLength > 0)
        {
            float wallSize = targetSize - totalWallLength;
            float wallPos = totalWallLength + (wallSize / 2);
            float wallScale = 1 / (float)_wallWidth * wallSize;
            Vector3 createPos = new Vector3(bottomPos.x, 0, wallPos);
            Vector3 cresteSize = new Vector3(1, 1, wallScale);
            CreateWall(createPos, EMapPoolType.VerticalWall, cresteSize);
        }
    }

    bool CheckDoor(out int doorPos, Dictionary<int, HashSet<int>> dict, int key, int pos1, int pos2)
    {
        HashSet<int> hash;
        doorPos = 0;
        if (dict.TryGetValue(key, out hash))
        {
            doorPos = hash.FirstOrDefault(value => pos1 < value && value < pos2);
            if (doorPos != 0)
                return true;
        }
        return false;
    }

    void CreateWall(Vector3 pos, EMapPoolType type)
    {
        GameObject wall = _factoryManager.MapFactory.MakeObject(type);
        wall.transform.position = pos;
        _dungeonCreator.Maps.Add(wall.GetComponent<IMap>());
    }

    void CreateWall(Vector3 pos, EMapPoolType type, Vector3 size)
    {
        GameObject wall = _factoryManager.MapFactory.MakeObject(type);
        wall.transform.position = pos;
        wall.GetComponent<Wall>().ChangeWallSize(size);
        _dungeonCreator.Maps.Add(wall.GetComponent<IMap>());
    }

    public void CalculateRoomWallPosition(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
    {
        HorizontalRoomWall(bottomLeft, bottomRight);   // Bottom
        HorizontalRoomWall(topLeft, topRight);         // Top
        VerticalRoomWall(bottomLeft, topLeft);         // Left
        VerticalRoomWall(bottomRight, topRight);       // Right
    }

    public void CalculateCorridorWallPosition(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
    {
        if ((bottomRight.x - bottomLeft.x) < (topLeft.z - bottomLeft.z))
        {
            VerticalCorridorWall(bottomLeft, topLeft);          // Left
            VerticalCorridorWall(bottomRight, topRight);        // Right
        }
        else
        {
            HorizontalCorridorWall(bottomLeft, bottomRight);    // Bottom
            HorizontalCorridorWall(topLeft, topRight);          // Top
        }
    }
}