using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StructureHelper
{
    static int _minX;
    static int _maxX;
    static int _minY;
    static int _maxY;

    /// <summary>
    /// 주어진 노드 트리에서 가장 하위 리프 노드들을 모두 찾아 반환
    /// 너비 우선 탐색(BFS) 방식을 사용하여 트리를 순회
    /// </summary>
    public static List<Node> TraverseGraphToExtractLowestLeafes(Node parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();

        // 만약 부모 노드가 리프 노드라면, 그 노드만 반환
        if (parentNode.ChildrenNodeList.Count == 0)
            return new List<Node>() { parentNode };

        // 부모 노드의 모든 자식 노드를 탐색 대기 큐에 추가
        foreach (Node child in parentNode.ChildrenNodeList)
            nodesToCheck.Enqueue(child);

        // 큐가 빌 때까지 반복 탐색
        while (nodesToCheck.Count > 0)
        {
            // 큐에서 노드 하나 꺼내기
            Node currentNode = nodesToCheck.Dequeue();

            if (currentNode.ChildrenNodeList.Count == 0)
            {
                // 자식이 없으면 리프 노드 리스트에 추가
                listToReturn.Add(currentNode);
            }
            else
            {
                // 자식이 있으면 그 자식들을 큐에 추가하여 탐색 계속
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