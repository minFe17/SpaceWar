using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNode _rootNode;

    int _roomWidthMin;
    int _roomLengthMin;

    public RoomNode RootNode { get => _rootNode; }

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        _rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);   //�ֻ��� ���� ����
    }

    /// <summary>
    /// BSP Ʈ�� ������� ��ü ������ �ִ� �ݺ� Ƚ����ŭ ����
    /// �� ������ �ּ� ũ���� 2�� �̻��� ���� ���� �Ǵ� �������� ����
    /// </summary>
    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        _roomWidthMin = roomWidthMin;
        _roomLengthMin = roomLengthMin;

        // ������ ������ ������ ť�� ����� ������ ����Ʈ ����
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();

        // �ʱ� ��ü ����(��Ʈ ���)�� ť�� ����Ʈ�� �߰�
        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);

        int iterations = 0;
        // �ִ� �ݺ� Ƚ����ŭ �Ǵ� ť�� �� ������ ���� �ݺ�
        while (iterations < maxIterations && graph.Count > 0)
        {
            iterations++;

            // ť���� ���� ���� ��� ����
            RoomNode currentNode = graph.Dequeue();

            // ���� ���� ũ�Ⱑ �ּ� ũ���� 2�� �̻��� ��� ���� ����
            if (currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)
                SplitTheSpace(currentNode, listToReturn, graph);
        }

        // ��� ���ҵ� ���� ����Ʈ ��ȯ
        return listToReturn;
    }


    /// <summary>
    /// ���� ������ ���� �Ǵ� �������� �����Ͽ� �� ���� �ڽ� ������ ����
    /// ���� ���� ���� ũ��� �ּ� �� ũ�⸦ ����� ����
    /// </summary>
    void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, Queue<RoomNode> graph)
    {
        // ������ �� ���� (���� or ����)
        Line dividingLine = GetLineDividingSpace(currentNode.BottomLeftAreaCorner, currentNode.TopRightAreaCorner);

        RoomNode firstChild;
        RoomNode secondChild;

        if (dividingLine.Orientation == EOrientation.Horizontal)
        {
            // ���� ����: �Ʒ� �κ� �� ����
            firstChild = new RoomNode(currentNode.BottomLeftAreaCorner, new Vector2Int(currentNode.TopRightAreaCorner.x, dividingLine.Coordinates.y),
                                      currentNode, currentNode.TreeLayerIndex + 1);

            // ���� ����: �� �κ� �� ����
            secondChild = new RoomNode(new Vector2Int(currentNode.BottomLeftAreaCorner.x, dividingLine.Coordinates.y), currentNode.TopRightAreaCorner,
                                       currentNode, currentNode.TreeLayerIndex + 1);
        }
        else
        {
            // ���� ����: ���� �κ� �� ����
            firstChild = new RoomNode(currentNode.BottomLeftAreaCorner, new Vector2Int(dividingLine.Coordinates.x, currentNode.TopRightAreaCorner.y),
                                      currentNode, currentNode.TreeLayerIndex + 1);

            // ���� ����: ������ �κ� �� ����
            secondChild = new RoomNode(new Vector2Int(dividingLine.Coordinates.x, currentNode.BottomLeftAreaCorner.y), currentNode.TopRightAreaCorner,
                                       currentNode, currentNode.TreeLayerIndex + 1);
        }

        // �� ����� ����Ʈ�� ť�� �߰�
        AddNewNodeToCollections(listToReturn, graph, firstChild);
        AddNewNodeToCollections(listToReturn, graph, secondChild);
    }


    void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    /// <summary>
    /// ���� ������ ���� �Ǵ� �������� ������ ����� ���� ��ǥ�� ����
    /// ���� ũ��� �ּ� �� ũ�⸦ �������� ���� ������ ����
    /// </summary>
    Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner)
    {
        // ���� ���� ���̰� �ּ� 2�� �̻����� Ȯ��
        bool canSplitWidth = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * _roomWidthMin;

        // ���� ���� ���̰� �ּ� 2�� �̻����� Ȯ��
        bool canSplitLength = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * _roomLengthMin;

        EOrientation orientation;

        if (canSplitWidth && canSplitLength)
        {
            // ����, ���� ��� ���� �����ϸ� ���� ����
            orientation = (EOrientation)(Random.Range(0, 2));
        }
        else if (canSplitWidth)
        {
            // ���θ� ���� �����ϸ� ���� ���ؼ� ����
            orientation = EOrientation.Vertical;
        }
        else
        {
            // ���� ���� �Ұ� �� ���� ���ؼ� ����
            orientation = EOrientation.Horizontal;
        }

        // ���� ���⿡ ���� ���� ��ǥ ��� �� ��ȯ
        return new Line(orientation, GetCoordinatesFororientation(orientation, bottomLeftAreaCorner, topRightAreaCorner));
    }

    /// <summary>
    /// ���� ���⿡ ���� ���� ��ǥ�� �������� ���
    /// </summary>
    Vector2Int GetCoordinatesFororientation(EOrientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner)
    {
        Vector2Int coordinates = Vector2Int.zero;

        if (orientation == EOrientation.Vertical)
        {
            // ���� ���� ��, x ��ǥ�� ����, y ��ǥ�� 0���� ����
            coordinates = new Vector2Int(Random.Range(bottomLeftAreaCorner.x + _roomWidthMin, topRightAreaCorner.x - _roomWidthMin), 0);
        }
        else
        {
            // ���� ���� ��, x ��ǥ�� 0���� ����, y ��ǥ�� ����
            coordinates = new Vector2Int(0, Random.Range(bottomLeftAreaCorner.y + _roomLengthMin, topRightAreaCorner.y - _roomLengthMin));
        }
        return coordinates;
    }
}