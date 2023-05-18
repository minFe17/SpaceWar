using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNode _rootNode;
    public RoomNode RootNode { get => _rootNode; }

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        _rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);   //�ֻ��� ���� ����
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);
        int iterations = 0; // ���� �� ����
        while (iterations < maxIterations && graph.Count > 0)   // �� ���� < �ִ� �氳�� && ť�� ������ 0�� �ƴϸ� ����
        {
            iterations++;   // ���� �� 1�� �߰�
            RoomNode currentNode = graph.Dequeue(); // ť �ȿ� �ִ� ���� 1�� ������ currentNode�� ����
            if (currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)   // currentNode�� ���� ���� �� ������ ���� 
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
        bool isWidthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomWidthMin;   // ���α��� >= 2 * �� �ּ� ���� ����
        bool isLengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomLengthMin; // ���α��� >= 2 * �� �ּ� ���� ����

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
            coordinates = new Vector2Int(Random.Range((bottomLeftAreaCorner.x + roomWidthMin), (topRightAreaCorner.x - roomWidthMin)), 0);  // ���� �翷���� ������(x : ���� y : 0)
        else
            coordinates = new Vector2Int(0, Random.Range((bottomLeftAreaCorner.y + roomLengthMin), (topRightAreaCorner.y - roomLengthMin)));// ���� ���Ʒ��� ������(x : 0 y : ����)

        return coordinates;
    }
}