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
        _rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);   //최상위 룸노드 생성
    }

    /// <summary>
    /// BSP 트리 기반으로 전체 공간을 최대 반복 횟수만큼 분할
    /// 각 공간이 최소 크기의 2배 이상일 때만 수평 또는 수직으로 분할
    /// </summary>
    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        _roomWidthMin = roomWidthMin;
        _roomLengthMin = roomLengthMin;

        // 분할할 공간을 저장할 큐와 결과를 저장할 리스트 생성
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();

        // 초기 전체 공간(루트 노드)을 큐와 리스트에 추가
        graph.Enqueue(_rootNode);
        listToReturn.Add(_rootNode);

        int iterations = 0;
        // 최대 반복 횟수만큼 또는 큐가 빌 때까지 분할 반복
        while (iterations < maxIterations && graph.Count > 0)
        {
            iterations++;

            // 큐에서 현재 공간 노드 추출
            RoomNode currentNode = graph.Dequeue();

            // 현재 공간 크기가 최소 크기의 2배 이상일 경우 분할 수행
            if (currentNode.Width >= roomWidthMin * 2 || currentNode.Length >= roomLengthMin * 2)
                SplitTheSpace(currentNode, listToReturn, graph);
        }

        // 모든 분할된 공간 리스트 반환
        return listToReturn;
    }


    /// <summary>
    /// 현재 공간을 수평 또는 수직으로 분할하여 두 개의 자식 공간을 생성
    /// 분할 선은 공간 크기와 최소 방 크기를 고려해 결정
    /// </summary>
    void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, Queue<RoomNode> graph)
    {
        // 분할할 선 결정 (수평 or 수직)
        Line dividingLine = GetLineDividingSpace(currentNode.BottomLeftAreaCorner, currentNode.TopRightAreaCorner);

        RoomNode firstChild;
        RoomNode secondChild;

        if (dividingLine.Orientation == EOrientation.Horizontal)
        {
            // 수평 분할: 아래 부분 방 생성
            firstChild = new RoomNode(currentNode.BottomLeftAreaCorner, new Vector2Int(currentNode.TopRightAreaCorner.x, dividingLine.Coordinates.y),
                                      currentNode, currentNode.TreeLayerIndex + 1);

            // 수평 분할: 위 부분 방 생성
            secondChild = new RoomNode(new Vector2Int(currentNode.BottomLeftAreaCorner.x, dividingLine.Coordinates.y), currentNode.TopRightAreaCorner,
                                       currentNode, currentNode.TreeLayerIndex + 1);
        }
        else
        {
            // 수직 분할: 왼쪽 부분 방 생성
            firstChild = new RoomNode(currentNode.BottomLeftAreaCorner, new Vector2Int(dividingLine.Coordinates.x, currentNode.TopRightAreaCorner.y),
                                      currentNode, currentNode.TreeLayerIndex + 1);

            // 수직 분할: 오른쪽 부분 방 생성
            secondChild = new RoomNode(new Vector2Int(dividingLine.Coordinates.x, currentNode.BottomLeftAreaCorner.y), currentNode.TopRightAreaCorner,
                                       currentNode, currentNode.TreeLayerIndex + 1);
        }

        // 새 방들을 리스트와 큐에 추가
        AddNewNodeToCollections(listToReturn, graph, firstChild);
        AddNewNodeToCollections(listToReturn, graph, secondChild);
    }


    void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    /// <summary>
    /// 현재 공간을 수평 또는 수직으로 분할할 방향과 분할 좌표를 결정
    /// 공간 크기와 최소 방 크기를 기준으로 분할 방향을 선택
    /// </summary>
    Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner)
    {
        // 공간 가로 길이가 최소 2배 이상인지 확인
        bool canSplitWidth = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * _roomWidthMin;

        // 공간 세로 길이가 최소 2배 이상인지 확인
        bool canSplitLength = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * _roomLengthMin;

        EOrientation orientation;

        if (canSplitWidth && canSplitLength)
        {
            // 가로, 세로 모두 분할 가능하면 랜덤 선택
            orientation = (EOrientation)(Random.Range(0, 2));
        }
        else if (canSplitWidth)
        {
            // 가로만 분할 가능하면 수직 기준선 설정
            orientation = EOrientation.Vertical;
        }
        else
        {
            // 가로 분할 불가 시 수평 기준선 설정
            orientation = EOrientation.Horizontal;
        }

        // 분할 방향에 따른 분할 좌표 계산 후 반환
        return new Line(orientation, GetCoordinatesFororientation(orientation, bottomLeftAreaCorner, topRightAreaCorner));
    }

    /// <summary>
    /// 분할 방향에 따라 분할 좌표를 랜덤으로 계산
    /// </summary>
    Vector2Int GetCoordinatesFororientation(EOrientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner)
    {
        Vector2Int coordinates = Vector2Int.zero;

        if (orientation == EOrientation.Vertical)
        {
            // 수직 분할 시, x 좌표는 랜덤, y 좌표는 0으로 고정
            coordinates = new Vector2Int(Random.Range(bottomLeftAreaCorner.x + _roomWidthMin, topRightAreaCorner.x - _roomWidthMin), 0);
        }
        else
        {
            // 수평 분할 시, x 좌표는 0으로 고정, y 좌표는 랜덤
            coordinates = new Vector2Int(0, Random.Range(bottomLeftAreaCorner.y + _roomLengthMin, topRightAreaCorner.y - _roomLengthMin));
        }
        return coordinates;
    }
}