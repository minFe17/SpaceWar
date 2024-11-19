using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DoorCreator : MonoBehaviour
{
    DungeonCreator _dungeonCreator;
    FactoryManager _factoryManager;

    public void Init(DungeonCreator dungeonCreator)
    {
        _dungeonCreator = dungeonCreator;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _dungeonCreator.HorizontalDoorPos = new Dictionary<int, HashSet<int>>();
        _dungeonCreator.VerticalDoorPos = new Dictionary<int, HashSet<int>>();
    }

    void CreateDoor(Enum doorType, Vector3 pos)
    {
        GameObject door = _factoryManager.MapFactory.MakeObject((EMapPoolType)doorType);
        door.transform.position = pos;
        _dungeonCreator.Maps.Add(door.GetComponent<IMap>());
    }

    void AddDoorPosition(Dictionary<int, HashSet<int>> target, int key, int value)
    {
        if (!target.ContainsKey(key))
            target[key] = new HashSet<int>();
        target[key].Add(value);
    }

    public void CreateDoors(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        if ((topRightCorner.x - bottomLeftCorner.x) < (topRightCorner.y - bottomLeftCorner.y))
        {
            Vector3 createBottomPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, -0.2f, bottomLeftCorner.y);
            Vector3 createTopPos = new Vector3((bottomLeftCorner.x + topRightCorner.x) / 2, -0.2f, topRightCorner.y);

            CreateDoor(EMapPoolType.HorizontalDoor, createBottomPos);
            CreateDoor(EMapPoolType.HorizontalDoor, createTopPos);

            AddDoorPosition(_dungeonCreator.HorizontalDoorPos, (int)createBottomPos.z, (int)createBottomPos.x);
            AddDoorPosition(_dungeonCreator.HorizontalDoorPos, (int)createTopPos.z, (int)createTopPos.x);
        }
        else
        {
            Vector3 createLeftPos = new Vector3(bottomLeftCorner.x, -0.2f, (bottomLeftCorner.y + topRightCorner.y) / 2);
            Vector3 createRightPos = new Vector3(topRightCorner.x, -0.2f, (bottomLeftCorner.y + topRightCorner.y) / 2);

            CreateDoor(EMapPoolType.VerticalDoor, createLeftPos);
            CreateDoor(EMapPoolType.VerticalDoor, createRightPos);

            AddDoorPosition(_dungeonCreator.VerticalDoorPos, (int)createLeftPos.x, (int)createLeftPos.z);
            AddDoorPosition(_dungeonCreator.VerticalDoorPos, (int)createRightPos.x, (int)createRightPos.z);
        }
    }
}