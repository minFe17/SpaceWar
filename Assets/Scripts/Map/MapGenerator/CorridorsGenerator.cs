using System.Collections.Generic;
using System.Linq;

public class CorridorsGenerator
{
    public List<Node> CreateCorridors(List<RoomNode> allNodesCollection, int corridorWidth, int doorWidth)
    {
        List<Node> corridorList = new List<Node>();
        Queue<RoomNode> structuresToCheck = new Queue<RoomNode>(allNodesCollection.OrderByDescending(node => node.TreeLayerIndex).ToList());
        while (structuresToCheck.Count > 0)
        {
            var node = structuresToCheck.Dequeue();
            if (node.ChildrenNodeList.Count == 0)
                continue;

            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth, doorWidth);
            corridorList.Add(corridor);
        }
        return corridorList;
    }
}