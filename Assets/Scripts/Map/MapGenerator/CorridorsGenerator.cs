using System.Collections.Generic;
using System.Linq;

public class CorridorsGenerator
{
    /// <summary>
    /// BSP 트리 구조에서 각 부모 노드의 자식 방들을 연결하는 복도를 생성
    /// 각 복도는 두 자식 노드를 연결하며, 지정된 복도 너비와 문 너비를 기반으로 생성
    /// </summary>
    public List<Node> CreateCorridors(List<RoomNode> allNodesCollection, int corridorWidth, int doorWidth)
    {
        List<Node> corridorList = new List<Node>();

        // 트리 계층이 높은 노드부터 처리하도록 내림차순 정렬 후 큐에 삽입
        Queue<RoomNode> structuresToCheck = new Queue<RoomNode>(allNodesCollection.OrderByDescending(node => node.TreeLayerIndex).ToList());

        while (structuresToCheck.Count > 0)
        {
            RoomNode node = structuresToCheck.Dequeue();

            // 자식 노드가 없는 경우 복도 생성 불필요
            if (node.ChildrenNodeList.Count == 0)
                continue;

            // 두 자식 노드를 연결하는 복도 노드 생성 및 추가
            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth, doorWidth);

            corridorList.Add(corridor);
        }

        return corridorList;
    }
}