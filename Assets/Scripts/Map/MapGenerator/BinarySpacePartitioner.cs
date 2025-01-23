using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNode _rootNode;
    public RoomNode RootNode { get => _rootNode; }

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        _rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);   //최상위 룸노드 생성
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        // 방을 분할하고 추가할 큐, 결과를 담을 리스트 생성
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();

        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);

        int iterations = 0;
        // maxIterations 이하의 반복 횟수 && 큐에 남은 노드가 있는 동안 반복
        while (iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue(); // 큐에서 현재 노드를 꺼냄

            // 현재 방의 크기가 최소 크기의 2배 이상이면 방을 분할
            if (currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)
            {
                SplitTheSpace(currentNode, listToReturn, roomWidthMin, roomLengthMin, graph);
            }
        }
        return listToReturn;
    }

    void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomWidthMin, int roomLengthMin, Queue<RoomNode> graph)
    {
        // 방을 분할할 선을 결정(수평 or 수직)
        Line line = GetLineDividingSpace(currentNode.BottomLeftAreaCorner, currentNode.TopRightAreaCorner, roomWidthMin, roomLengthMin);
        RoomNode node1;
        RoomNode node2;

        // 분할할 선이 수평일 경우)
        if (line.Orientation == EOrientation.Horizontal)
        {
            // 현재 방의 아래 부분을 사용하여 새로운 방 생성
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner,
                                 new Vector2Int(currentNode.TopRightAreaCorner.x, line.Coordinates.y),
                                 currentNode,
                                 currentNode.TreeLayerIndex + 1);

            // 현재 방의 위 부분을 사용하여 새로운 방 생성
            node2 = new RoomNode(new Vector2Int(currentNode.BottomLeftAreaCorner.x, line.Coordinates.y),
                                 currentNode.TopRightAreaCorner,
                                 currentNode,
                                 currentNode.TreeLayerIndex + 1);
        }
        else // 분할할 선이 수직인 경우
        {
            // 현재 방의 왼쪽 부분을 사용하여 새로운 방 생성
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner,
                                 new Vector2Int(line.Coordinates.x, currentNode.TopRightAreaCorner.y),
                                 currentNode,
                                 currentNode.TreeLayerIndex + 1);

            // 현재 방의 오른쪽 부분을 사용하여 새로운 방 생성
            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftAreaCorner.y),
                                 currentNode.TopRightAreaCorner,
                                 currentNode,
                                 currentNode.TreeLayerIndex + 1);
        }

        // 새로운 방을 리스트와 큐에 추가
        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);
    }

    void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        EOrientation orientation;
        bool isWidthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomWidthMin;   // 가로길이 >= 2 * 방 최소 가로 길이
        bool isLengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomLengthMin; // 세로길이 >= 2 * 방 최소 세로 길이

        if (isLengthStatus && isWidthStatus)
        {
            orientation = (EOrientation)(Random.Range(0, 2));
        }
        else if (isWidthStatus)
        {
            orientation = EOrientation.Vertical;
        }
        else
        {
            orientation = EOrientation.Horizontal;
        }
        return new Line(orientation, GetCoordinatesFororientation(orientation, bottomLeftAreaCorner, topRightAreaCorner, roomWidthMin, roomLengthMin));
    }

    // 분할할 좌표를 계산하는 함수
    Vector2Int GetCoordinatesFororientation(EOrientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if (orientation == EOrientation.Vertical)
            coordinates = new Vector2Int(Random.Range((bottomLeftAreaCorner.x + roomWidthMin), (topRightAreaCorner.x - roomWidthMin)), 0);      // 방을 양옆으로 나누기(x : 랜덤 y : 0)
        else
            coordinates = new Vector2Int(0, Random.Range((bottomLeftAreaCorner.y + roomLengthMin), (topRightAreaCorner.y - roomLengthMin)));    // 방을 위아래로 나누기(x : 0 y : 랜덤)

        return coordinates;
    }
}