using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorNode : Node
{
    Node _structure1;
    Node _structure2;
    int _corridorWidth;
    int _modifierDistanceFromWall = 1;

    public CorridorNode(Node structure1, Node structure2, int corridorWidth) : base(null)
    {
        _structure1 = structure1;
        _structure2 = structure2;
        _corridorWidth = corridorWidth;
        GenerateCorridor();
    }

    void GenerateCorridor()
    {
        var relativePositionOfStructure2 = CheckPositionStructure2AgainstStructure1();
        switch (relativePositionOfStructure2)
        {
            case ERelativePositionType.Up:
                ProcessRoomInRelationUpOrDown(_structure1, _structure2);
                break;
            case ERelativePositionType.Down:
                ProcessRoomInRelationUpOrDown(_structure2, _structure1);
                break;
            case ERelativePositionType.Left:
                ProcessRoomInRelationLeftOrRight(_structure1, _structure2);
                break;
            case ERelativePositionType.Right:
                ProcessRoomInRelationLeftOrRight(_structure2, _structure1);
                break;
            default:
                break;
        }

    }


    void ProcessRoomInRelationUpOrDown(Node structure1, Node structure2)
    {
        throw new NotImplementedException();
    }

    void ProcessRoomInRelationLeftOrRight(Node structure1, Node structure2)
    {
        Node leftStructure = null;
        List<Node> leftStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node rightStructure = null;
        List<Node> rightStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        var sortedLeftStructure = leftStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();
        if (sortedLeftStructure.Count == 1)
        {
            leftStructure = sortedLeftStructure[0];
        }
        else
        {
            int maxX = sortedLeftStructure[0].TopRightAreaCorner.x;
            sortedLeftStructure = sortedLeftStructure.Where(children => Mathf.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();
            int index = Random.Range(0, sortedLeftStructure.Count);
            leftStructure = sortedLeftStructure[index];
        }

        var possibleNeighboursInRightStructureList = rightStructureChildren.Where(
            child => GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                                   child.TopLeftAreaCorner, child.BottomLeftAreaCorner) != -1).ToList();

        if (possibleNeighboursInRightStructureList.Count <= 0)
            rightStructure = structure2;
        else
            rightStructure = possibleNeighboursInRightStructureList[0];

        int y = GetValidYForNeighourLeftRight(leftStructure.TopLeftAreaCorner, leftStructure.BottomRightAreaCorner,
                                              rightStructure.TopLeftAreaCorner, rightStructure.BottomLeftAreaCorner);

        while (y == -1 && sortedLeftStructure.Count > 0)
        {
            sortedLeftStructure = sortedLeftStructure.Where(child => child.TopLeftAreaCorner.y != leftStructure.TopLeftAreaCorner.y).ToList();
            leftStructure = sortedLeftStructure[0];

            y = GetValidYForNeighourLeftRight(leftStructure.TopLeftAreaCorner, leftStructure.BottomRightAreaCorner,
                                              rightStructure.TopLeftAreaCorner, rightStructure.BottomLeftAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(leftStructure.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStructure.TopLeftAreaCorner.x, y + _corridorWidth);
    }

    private int GetValidYForNeighourLeftRight(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int rightNodeUp, Vector2Int rightNodeDown)
    {
        if (leftNodeUp.y <= rightNodeUp.y && leftNodeDown.y >= rightNodeDown.y)
        {
            return StructureHelper.CalculateMiddlePoint(leftNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        leftNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
        }
        if (leftNodeUp.y >= rightNodeUp.y && leftNodeDown.y <= rightNodeDown.y)
        {
            return StructureHelper.CalculateMiddlePoint(rightNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        rightNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;

        }
        if (leftNodeUp.y >= rightNodeDown.y && leftNodeUp.y <= rightNodeUp.y)
        {
            return StructureHelper.CalculateMiddlePoint(rightNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        leftNodeUp - new Vector2Int(0, _modifierDistanceFromWall)).y;
        }
        if (leftNodeDown.y >= rightNodeDown.y && leftNodeDown.y <= rightNodeUp.y)
        {
            return StructureHelper.CalculateMiddlePoint(leftNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        rightNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
        }
        return -1;

    }

    ERelativePositionType CheckPositionStructure2AgainstStructure1()
    {
        Vector2 middlePointStructure1Temp = ((Vector2)_structure1.TopRightAreaCorner + _structure1.BottomLeftAreaCorner) / 2;
        Vector2 middlePointStructure2Temp = ((Vector2)_structure2.TopRightAreaCorner + _structure2.BottomLeftAreaCorner) / 2;
        float angle = CalculateAngle(middlePointStructure1Temp, middlePointStructure2Temp);

        if ((angle < 45 && angle >= 0) || (angle > -45 && angle < 0))
            return ERelativePositionType.Right;
        else if (angle > 45 && angle < 135)
            return ERelativePositionType.Up;
        else if (angle > -135 && angle < -45)
            return ERelativePositionType.Down;
        else
            return ERelativePositionType.Left;
    }

    float CalculateAngle(Vector2 middlePointStructure1Temp, Vector2 middlePointStructure2Temp)
    {
        return Mathf.Atan2(middlePointStructure2Temp.y - middlePointStructure1Temp.y, middlePointStructure2Temp.x - middlePointStructure1Temp.x) * Mathf.Rad2Deg;
    }
}

public enum ERelativePositionType
{
    Up,
    Down,
    Left,
    Right
}