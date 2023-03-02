using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNode _rootNode;

    public RoomNode RootNode { get => _rootNode;}

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
       _rootNode = new RoomNode(new Vector2Int(0,0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);
        int iterations = 0;
        while(iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            if(currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)
            {
                SplitTheSpace(currentNode, listToReturn, roomWidthMin, roomLengthMin, graph);
            }
        }
    }

    private void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomWidthMin, int roomLengthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(currentNode.BottomLeftAreaCorner, currentNode.TopRightAreaCorner, roomWidthMin, roomLengthMin);
    }

    private Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        EOrientation orientation;
        bool isLengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomWidthMin;  // 세로 길이가 더 크면 True
        bool isWidthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomLengthMin;  // 가로 길이가 더 크면 True
        if(isLengthStatus && isWidthStatus)
        {
            orientation =  (EOrientation)(Random.Range(0, 2))
        }
        else if(isLengthStatus)
        {
            orientation = EOrientation.Horizontal;
        }
        else
        {
            orientation = EOrientation.Vertical;
        }
        return new Line(orientation, GetCoordinatesFororientation(orientation, bottomLeftAreaCorner, topRightAreaCorner, roomWidthMin, roomLengthMin));
    }

    private Vector2Int GetCoordinatesFororientation(EOrientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        throw new NotImplementedException();
    }
}