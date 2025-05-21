using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StructureHelper
{
    static int _minX;
    static int _maxX;
    static int _minY;
    static int _maxY;

    public static List<Node> TraverseGraphToExtractLowestLeafes(Node parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();
        if (parentNode.ChildrenNodeList.Count == 0)
            return new List<Node>() { parentNode };

        // 자식 노드가 있으면 큐에 추가
        foreach (Node child in parentNode.ChildrenNodeList)
            nodesToCheck.Enqueue(child);

        // 큐가 비어있지 않으면 노드 탐색
        while (nodesToCheck.Count > 0)
        {
            // 큐에서 노드 꺼내기
            Node currentNode = nodesToCheck.Dequeue();

            // 자식 노드가 없다면 리스트에 추가
            if (currentNode.ChildrenNodeList.Count == 0)
                listToReturn.Add(currentNode);
            else
            {
                // 자식 노드가 있다면 자식들을 큐에 추가
                foreach (Node child in currentNode.ChildrenNodeList)
                    nodesToCheck.Enqueue(child);
            }
        }
        return listToReturn;
    }

    public static Vector2Int GenerateBottomLeftCornerBetween(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        CalculateOffsetPoint(boundaryLeftPoint, boundaryRightPoint, offset);
        return new Vector2Int(Random.Range(_minX, (int)(_minX + (_maxX - _minX) * pointModifier)),
                              Random.Range(_minY, (int)(_minY + (_maxY - _minY) * pointModifier)));
    }

    public static Vector2Int GenerateTopRightCornerBetween(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        CalculateOffsetPoint(boundaryLeftPoint, boundaryRightPoint, offset);
        return new Vector2Int(Random.Range((int)(_minX + (_maxX - _minX) * pointModifier), _maxX),
                              Random.Range((int)(_minY + (_maxY - _minY) * pointModifier), _maxY));
    }

    static void CalculateOffsetPoint(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, int offset)
    {
        _minX = boundaryLeftPoint.x + offset;
        _maxX = boundaryRightPoint.x - offset;
        _minY = boundaryLeftPoint.y + offset;
        _maxY = boundaryRightPoint.y - offset;
    }

    public static Vector2Int CalculateMiddlePoint(Vector2Int v1, Vector2Int v2)
    {
        Vector2 sum = v1 + v2;
        Vector2 tempVector = sum / 2;
        return new Vector2Int((int)tempVector.x, (int)tempVector.y);
    }
}