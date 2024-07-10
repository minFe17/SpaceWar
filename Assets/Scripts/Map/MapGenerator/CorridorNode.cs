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
                ProcessRoomInRelationLeftOrRight(_structure2, _structure1);
                break;
            case ERelativePositionType.Right:
                ProcessRoomInRelationLeftOrRight(_structure1, _structure2);
                break;
            default:
                break;
        }
    }

    void ProcessRoomInRelationUpOrDown(Node structure1, Node structure2)
    {
        Node bottomStructure = null;
        List<Node> bottomStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node topStructure = null;
        List<Node> topStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        var sortedBottomStructure = bottomStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.y).ToList();

        if (sortedBottomStructure.Count == 1)
            bottomStructure = bottomStructureChildren[0];
        else
        {
            int maxY = sortedBottomStructure[0].TopRightAreaCorner.y;
            sortedBottomStructure = sortedBottomStructure.Where(child => Mathf.Abs(maxY - child.TopRightAreaCorner.y) < 10).ToList();
            int index = Random.Range(0, sortedBottomStructure.Count);
            bottomStructure = sortedBottomStructure[index];
        }

        var possibleNeighboursInTopStructure = topStructureChildren.Where(
            child => GetValidXForNeighourUpDown(bottomStructure.TopLeftAreaCorner, bottomStructure.TopRightAreaCorner,
                                                child.BottomLeftAreaCorner, child.BottomRightAreaCorner) != -1).
                                                OrderBy(child => child.BottomRightAreaCorner.y).ToList();       

        if (possibleNeighboursInTopStructure.Count == 0)
            topStructure = structure2;
        else
            topStructure = possibleNeighboursInTopStructure[0];

        int x = GetValidXForNeighourUpDown(bottomStructure.TopLeftAreaCorner, bottomStructure.TopRightAreaCorner,
                                           topStructure.BottomLeftAreaCorner, topStructure.BottomRightAreaCorner);

        while (x == -1 && sortedBottomStructure.Count > 0)
        {
            sortedBottomStructure = sortedBottomStructure.Where(child => child.TopLeftAreaCorner.x != bottomStructure.TopLeftAreaCorner.x).ToList();
            bottomStructure = sortedBottomStructure[0];

            x = GetValidXForNeighourUpDown(bottomStructure.TopLeftAreaCorner, bottomStructure.TopRightAreaCorner,
                                           topStructure.BottomLeftAreaCorner, topStructure.BottomRightAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(x, bottomStructure.TopLeftAreaCorner.y);
        TopRightAreaCorner = new Vector2Int(x + _corridorWidth, topStructure.BottomLeftAreaCorner.y);
    }

    int GetValidXForNeighourUpDown(Vector2Int bottomNodeLeft, Vector2Int bottomNodeRight, Vector2Int topNodeLeft, Vector2Int topNodeRight)
    {
        if (bottomNodeLeft.x > topNodeLeft.x && bottomNodeRight.x < topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(bottomNodeLeft + new Vector2Int(_modifierDistanceFromWall, 0),
                                                        bottomNodeRight - new Vector2Int(_corridorWidth + _modifierDistanceFromWall, 0)).x;
        }
        if (bottomNodeLeft.x <= topNodeLeft.x && bottomNodeRight.x >= topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(topNodeLeft + new Vector2Int(_modifierDistanceFromWall, 0),
                                                        topNodeRight - new Vector2Int(_corridorWidth + _modifierDistanceFromWall, 0)).x;
        }
        if (bottomNodeLeft.x >= topNodeLeft.x && bottomNodeLeft.x <= topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(bottomNodeLeft + new Vector2Int(_modifierDistanceFromWall, 0),
                                                        topNodeRight - new Vector2Int(_corridorWidth + _modifierDistanceFromWall, 0)).x;
        }
        if (bottomNodeRight.x <= topNodeRight.x && bottomNodeRight.x >= topNodeLeft.x)
        {
            return StructureHelper.CalculateMiddlePoint(topNodeLeft + new Vector2Int(_modifierDistanceFromWall, 0),
                                                        bottomNodeRight - new Vector2Int(_corridorWidth + _modifierDistanceFromWall, 0)).x;
        }
        return -1;
    }

    void ProcessRoomInRelationLeftOrRight(Node structure1, Node structure2)
    {
        Node leftStructure = null;
        List<Node> leftStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node rightStructure = null;
        List<Node> rightStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        var sortedLeftStructure = leftStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();

        if (sortedLeftStructure.Count == 1)
            leftStructure = leftStructureChildren[0];
        else
        {
            int maxX = sortedLeftStructure[0].TopRightAreaCorner.x;
            sortedLeftStructure = sortedLeftStructure.Where(children => Mathf.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();
            int index = Random.Range(0, sortedLeftStructure.Count);
            leftStructure = sortedLeftStructure[index];
        }

        var possibleNeighboursInRightStructure = rightStructureChildren.Where(
            child => GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                                   child.TopLeftAreaCorner, child.BottomLeftAreaCorner) != -1).
                                                   OrderBy(child => child.BottomRightAreaCorner.x).ToList();

        if (possibleNeighboursInRightStructure.Count == 0)
            rightStructure = structure2;
        else
            rightStructure = possibleNeighboursInRightStructure[0];

        int y = GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                              rightStructure.TopLeftAreaCorner, rightStructure.BottomLeftAreaCorner);

        while (y == -1 && sortedLeftStructure.Count > 0)
        {
            sortedLeftStructure = sortedLeftStructure.Where(child => child.TopLeftAreaCorner.y != leftStructure.TopLeftAreaCorner.y).ToList();
            leftStructure = sortedLeftStructure[0];

            y = GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                              rightStructure.TopLeftAreaCorner, rightStructure.BottomLeftAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(leftStructure.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStructure.TopLeftAreaCorner.x, y + _corridorWidth);
    }

    int GetValidYForNeighourLeftRight(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int rightNodeUp, Vector2Int rightNodeDown)
    {
        if (leftNodeUp.y >= rightNodeUp.y && leftNodeDown.y <= rightNodeDown.y)
        {
            return StructureHelper.CalculateMiddlePoint(rightNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        rightNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
        }
        if (leftNodeUp.y <= rightNodeUp.y && leftNodeDown.y >= rightNodeDown.y)
        {
            return StructureHelper.CalculateMiddlePoint(leftNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        leftNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
        }
        if (leftNodeUp.y >= rightNodeDown.y && leftNodeUp.y <= rightNodeUp.y)
        {
            return StructureHelper.CalculateMiddlePoint(rightNodeDown + new Vector2Int(0, _modifierDistanceFromWall),
                                                        leftNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
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
        Vector2 middlePointStructure1Temp = ((Vector2)_structure1.TopRightAreaCorner + _structure1.BottomLeftAreaCorner) / 2;   // 첫번째 방 중심
        Vector2 middlePointStructure2Temp = ((Vector2)_structure2.TopRightAreaCorner + _structure2.BottomLeftAreaCorner) / 2;   // 두번째 방 중심
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
        return Mathf.Atan2(middlePointStructure2Temp.y - middlePointStructure1Temp.y,
                           middlePointStructure2Temp.x - middlePointStructure1Temp.x) * Mathf.Rad2Deg;
    }
}