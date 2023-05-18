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
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);
        int iterations = 0; // 현재 방 개수
        while (iterations < maxIterations && graph.Count > 0)   // 방 개수 < 최대 방개수 && 큐에 개수가 0이 아니면 실행
        {
            iterations++;   // 현재 방 1개 추가
            RoomNode currentNode = graph.Dequeue(); // 큐 안에 있는 룸노드 1개 꺼내서 currentNode에 저장
            if (currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)   // currentNode가 방을 나눌 수 있으면 실행 
            {
                SplitTheSpace(currentNode, listToReturn, roomWidthMin, roomLengthMin, graph); 
            }
        }
        return listToReturn;
    }

    void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomWidthMin, int roomLengthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(currentNode.BottomLeftAreaCorner, currentNode.TopRightAreaCorner, roomWidthMin, roomLengthMin);
        RoomNode node1;
        RoomNode node2;
        if (line.Orientation == EOrientation.Horizontal)
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner, 
                                 new Vector2Int(currentNode.TopRightAreaCorner.x, line.Coordinates.y), 
                                 currentNode, currentNode.TreeLayerIndex + 1);

            node2 = new RoomNode(new Vector2Int(currentNode.BottomLeftAreaCorner.x, line.Coordinates.y), 
                                 currentNode.TopRightAreaCorner, 
                                 currentNode, currentNode.TreeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner, 
                                 new Vector2Int(line.Coordinates.x, currentNode.TopRightAreaCorner.y), 
                                 currentNode, 
                                 currentNode.TreeLayerIndex + 1);

            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftAreaCorner.y), 
                                 currentNode.TopRightAreaCorner, 
                                 currentNode, 
                                 currentNode.TreeLayerIndex + 1);
        }
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

    Vector2Int GetCoordinatesFororientation(EOrientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if (orientation == EOrientation.Vertical)
            coordinates = new Vector2Int(Random.Range((bottomLeftAreaCorner.x + roomWidthMin), (topRightAreaCorner.x - roomWidthMin)), 0);  // 방을 양옆으로 나누기(x : 랜덤 y : 0)
        else
            coordinates = new Vector2Int(0, Random.Range((bottomLeftAreaCorner.y + roomLengthMin), (topRightAreaCorner.y - roomLengthMin)));// 방을 위아래로 나누기(x : 0 y : 랜덤)

        return coordinates;
    }
}