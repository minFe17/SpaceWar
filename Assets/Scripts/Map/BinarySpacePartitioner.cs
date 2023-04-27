using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNode _rootNode;
    public RoomNode RootNode { get => _rootNode; }

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        _rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);
        int iterations = 0;
        while (iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            if (currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)
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
        bool isLengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomWidthMin;  
        bool isWidthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomLengthMin;
        if (isLengthStatus && isWidthStatus)
        {
            orientation = (EOrientation)(Random.Range(0, 2));
        }
        else if (isLengthStatus)
        {
            orientation = EOrientation.Horizontal;
        }
        else
        {
            orientation = EOrientation.Vertical;
        }
        return new Line(orientation, GetCoordinatesFororientation(orientation, bottomLeftAreaCorner, topRightAreaCorner, roomWidthMin, roomLengthMin));
    }

    Vector2Int GetCoordinatesFororientation(EOrientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if (orientation == EOrientation.Horizontal)
        {
            coordinates = new Vector2Int(0, Random.Range((bottomLeftAreaCorner.y + roomLengthMin), (topRightAreaCorner.y - roomLengthMin)));
        }
        else
        {
            coordinates = new Vector2Int(Random.Range((bottomLeftAreaCorner.x + roomWidthMin), (topRightAreaCorner.x - roomWidthMin)), 0);
        }
        return coordinates;
    }
}