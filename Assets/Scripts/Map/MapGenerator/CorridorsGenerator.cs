using System.Collections.Generic;
using System.Linq;

public class CorridorsGenerator
{
    public List<Node> CreateCorridors(List<RoomNode> allNodesCollection, int corridorWidth, int doorWidth)
    {
        List<Node> corridorList = new List<Node>();

        // TreeLayerIndex가 높은 순으로 처리되도록 큐에 넣음
        Queue<RoomNode> structuresToCheck = new Queue<RoomNode>(allNodesCollection.OrderByDescending(node => node.TreeLayerIndex).ToList());
        
        while (structuresToCheck.Count > 0)
        {
            var node = structuresToCheck.Dequeue();

            // 자식 노드가 없다면 복도를 만들지 않음
            if (node.ChildrenNodeList.Count == 0)
                continue;

            // 두 개의 자식 노드를 연결하는 복도 생성
            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth, doorWidth);
            corridorList.Add(corridor);
        }
        return corridorList;
    }
}