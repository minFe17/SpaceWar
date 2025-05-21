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
    int _doorHalfWidth;

    public CorridorNode(Node structure1, Node structure2, int corridorWidth, int doorWidth) : base(null)
    {
        _structure1 = structure1;
        _structure2 = structure2;
        _corridorWidth = corridorWidth;
        _doorHalfWidth = doorWidth / 2;
        GenerateCorridor();
    }

    void GenerateCorridor()
    {
        ERelativePositionType relativePositionOfStructure2 = CheckPositionStructure2AgainstStructure1();
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
        // 리프노드 구하기
        Node bottomStructure = null;
        List<Node> bottomStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node topStructure = null;
        List<Node> topStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        // 노드를 y 좌표 기준으로 내림차순으로 정령
        List<Node> sortedBottomStructure = bottomStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.y).ToList();

        // 노드가 1개일 경우(structure1 리프노드인 경우)
        if (sortedBottomStructure.Count == 1)
            bottomStructure = bottomStructureChildren[0];
        else
        {
            // y 좌표 차이가 10 미만인 노드만 가져옴
            int maxY = sortedBottomStructure[0].TopRightAreaCorner.y;
            sortedBottomStructure = sortedBottomStructure.Where(child => Mathf.Abs(maxY - child.TopRightAreaCorner.y) < 10).ToList();
            
            // 가져온 노드들 중에 무작위로 1개 선택
            int index = Random.Range(0, sortedBottomStructure.Count);
            bottomStructure = sortedBottomStructure[index];
        }

        // bottomStructure와 연결 가능한 노드 찾기
        // 연결 가능한 x 좌표를 가져야 함
        List<Node> possibleNeighboursInTopStructure = topStructureChildren.Where(
            child => GetValidXForNeighourUpDown(bottomStructure.TopLeftAreaCorner, bottomStructure.TopRightAreaCorner,
                                                child.BottomLeftAreaCorner, child.BottomRightAreaCorner) != -1).
                                                OrderBy(child => child.BottomRightAreaCorner.y).ToList();

        // 복도를 연결할 노드가 없다면 structure2로 설정
        if (possibleNeighboursInTopStructure.Count == 0)
            topStructure = structure2;
        else
            topStructure = possibleNeighboursInTopStructure[0];

        // 연결 가능한 x 좌표 계산
        int x = GetValidXForNeighourUpDown(bottomStructure.TopLeftAreaCorner, bottomStructure.TopRightAreaCorner,
                                           topStructure.BottomLeftAreaCorner, topStructure.BottomRightAreaCorner);

        // x가 -1이면 연결할 수 없다는 뜻으로, 다른 노드를 선택해 다시 계산
        while (x == -1 && sortedBottomStructure.Count > 0)
        {
            // 같은 x 좌표를 가진 노드를 제외하고 새로운 노드를 선택 
            sortedBottomStructure = sortedBottomStructure.Where(child => child.TopLeftAreaCorner.x != bottomStructure.TopLeftAreaCorner.x).ToList();
            bottomStructure = sortedBottomStructure[0];

            // 연결 가능한지 다시 계산
            x = GetValidXForNeighourUpDown(bottomStructure.TopLeftAreaCorner, bottomStructure.TopRightAreaCorner,
                                           topStructure.BottomLeftAreaCorner, topStructure.BottomRightAreaCorner);
        }

        // 복도의 시작점과 끝점 계산
        BottomLeftAreaCorner = new Vector2Int(x, bottomStructure.TopLeftAreaCorner.y);
        TopRightAreaCorner = new Vector2Int(x + _corridorWidth, topStructure.BottomLeftAreaCorner.y);
    }

    int GetValidXForNeighourUpDown(Vector2Int bottomNodeLeft, Vector2Int bottomNodeRight, Vector2Int topNodeLeft, Vector2Int topNodeRight)
    {
        // 복도가 설치 가능한지 조건 탐색 후 설치할 위치 리턴
        // 리턴 값 : 복도 왼쪽 끝점
        if (bottomNodeLeft.x > topNodeLeft.x && bottomNodeRight.x < topNodeRight.x)
        {
            
            return StructureHelper.CalculateMiddlePoint(bottomNodeLeft + new Vector2Int(_modifierDistanceFromWall, 0),
                                                        bottomNodeRight - new Vector2Int(_modifierDistanceFromWall + _corridorWidth, 0)).x;
        }
        if (bottomNodeLeft.x <= topNodeLeft.x && bottomNodeRight.x >= topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(topNodeLeft + new Vector2Int(_modifierDistanceFromWall, 0),
                                                        topNodeRight - new Vector2Int(_modifierDistanceFromWall + _corridorWidth, 0)).x;
        }
        if (bottomNodeLeft.x >= topNodeLeft.x && bottomNodeLeft.x +_doorHalfWidth <= topNodeRight.x - _doorHalfWidth)
        {
            return StructureHelper.CalculateMiddlePoint(bottomNodeLeft + new Vector2Int(_modifierDistanceFromWall + _doorHalfWidth, 0),
                                                        topNodeRight - new Vector2Int(_modifierDistanceFromWall + _corridorWidth, 0)).x;
        }
        if (bottomNodeRight.x <= topNodeRight.x && bottomNodeRight.x - _doorHalfWidth >= topNodeLeft.x + _doorHalfWidth)
        {
            return StructureHelper.CalculateMiddlePoint(topNodeLeft + new Vector2Int(_modifierDistanceFromWall + _doorHalfWidth, 0),
                                                        bottomNodeRight - new Vector2Int(_modifierDistanceFromWall + _corridorWidth, 0)).x;
        }

        // 연결 불가능 할 때
        return -1;  
    }

    void ProcessRoomInRelationLeftOrRight(Node structure1, Node structure2)
    {
        // 리프노드 구하기
        Node leftStructure = null;
        List<Node> leftStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node rightStructure = null;
        List<Node> rightStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        // 자식 노드를 x 좌표 기준으로 내림차순으로 정령
        List<Node> sortedLeftStructure = leftStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();

        // 노드가 1개일 경우(structure1 리프노드인 경우)
        if (sortedLeftStructure.Count == 1)
            leftStructure = leftStructureChildren[0];
        else
        {
            // x 좌표 차이가 10 미만인 노드만 가져옴
            int maxX = sortedLeftStructure[0].TopRightAreaCorner.x;
            sortedLeftStructure = sortedLeftStructure.Where(children => Mathf.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();

            // 가져온 노드들 중에 무작위로 1개 선택
            int index = Random.Range(0, sortedLeftStructure.Count);
            leftStructure = sortedLeftStructure[index];
        }

        // leftStructure와 연결 가능한 노드 찾기
        // 연결 가능한 y 좌표를 가져야 함
        List<Node> possibleNeighboursInRightStructure = rightStructureChildren.Where(
            child => GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                                   child.TopLeftAreaCorner, child.BottomLeftAreaCorner) != -1).
                                                   OrderBy(child => child.BottomRightAreaCorner.x).ToList();

        // 복도를 연결할 노드가 없다면 structure2로 설정
        if (possibleNeighboursInRightStructure.Count == 0)
            rightStructure = structure2;
        else
            rightStructure = possibleNeighboursInRightStructure[0];

        // 연결 가능한 y 좌표 계산
        int y = GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                              rightStructure.TopLeftAreaCorner, rightStructure.BottomLeftAreaCorner);

        // y가 -1이면 연결할 수 없다는 뜻으로, 다른 노드를 선택해 다시 계산
        while (y == -1 && sortedLeftStructure.Count > 0)
        {
            // 같은 y 좌표를 가진 노드를 제외하고 새로운 노드를 선택 
            sortedLeftStructure = sortedLeftStructure.Where(child => child.TopLeftAreaCorner.y != leftStructure.TopLeftAreaCorner.y).ToList();
            leftStructure = sortedLeftStructure[0];

            // 연결 가능한지 다시 계산
            y = GetValidYForNeighourLeftRight(leftStructure.TopRightAreaCorner, leftStructure.BottomRightAreaCorner,
                                              rightStructure.TopLeftAreaCorner, rightStructure.BottomLeftAreaCorner);
        }

        // 복도의 시작점과 끝점 계산
        BottomLeftAreaCorner = new Vector2Int(leftStructure.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStructure.TopLeftAreaCorner.x, y + _corridorWidth);
    }

    int GetValidYForNeighourLeftRight(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int rightNodeUp, Vector2Int rightNodeDown)
    {
        // 복도가 설치 가능한지 조건 탐색 후 설치할 위치 리턴
        // 리턴 값 : 복도 아래쪽 끝점
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
        if (leftNodeUp.y <= rightNodeUp.y && leftNodeUp.y - _doorHalfWidth >= rightNodeDown.y +_doorHalfWidth )
        {
            return StructureHelper.CalculateMiddlePoint(rightNodeDown + new Vector2Int(0, _modifierDistanceFromWall + _doorHalfWidth),
                                                        leftNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
        }
        if (leftNodeDown.y >= rightNodeDown.y && leftNodeDown.y + _doorHalfWidth <= rightNodeUp.y - _doorHalfWidth)
        {
            return StructureHelper.CalculateMiddlePoint(leftNodeDown + new Vector2Int(0, _modifierDistanceFromWall + _doorHalfWidth),
                                                        rightNodeUp - new Vector2Int(0, _modifierDistanceFromWall + _corridorWidth)).y;
        }

        // 연결 불가능 할 때
        return -1;
    }

    ERelativePositionType CheckPositionStructure2AgainstStructure1()
    {
        // 방 중심 구하기
        Vector2 middlePointStructure1Temp = ((Vector2)_structure1.TopRightAreaCorner + _structure1.BottomLeftAreaCorner) / 2;   // 첫번째 방 중심
        Vector2 middlePointStructure2Temp = ((Vector2)_structure2.TopRightAreaCorner + _structure2.BottomLeftAreaCorner) / 2;   // 두번째 방 중심
        
        // 중심을 사이의 각도 계산
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
        // 두 개의 y 좌표와 x 좌표 차이를 구한 후, 각도 계산
        return Mathf.Atan2(middlePointStructure2Temp.y - middlePointStructure1Temp.y,
                           middlePointStructure2Temp.x - middlePointStructure1Temp.x) * Mathf.Rad2Deg;
    }
}