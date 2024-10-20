using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    List<Node> _childrenNodeList;
    public List<Node> ChildrenNodeList { get => _childrenNodeList; }

    public Vector2Int BottomLeftAreaCorner { get; set; }
    public Vector2Int BottomRightAreaCorner { get; set; }
    public Vector2Int TopLeftAreaCorner { get; set; }
    public Vector2Int TopRightAreaCorner { get; set; }

    public Node Parent { get; set; }

    public int TreeLayerIndex { get; set; }

    public Node(Node parentNode)
    {
        _childrenNodeList = new List<Node>();
        Parent = parentNode;
        if (parentNode != null)
            parentNode.AddChild(this);
    }

    public void AddChild(Node node)
    {
        _childrenNodeList.Add(node);
    }

    public void RemoveChild(Node node)
    {
        _childrenNodeList.Remove(node);
    }
}