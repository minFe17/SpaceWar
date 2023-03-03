using UnityEngine;

public class RoomNode : Node
{
    // RoomNode 생성자
    public RoomNode(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAReaCorner, Node parentNode, int index) : base(parentNode)
    {
        // 던전 사각형 모서리
        this.BottomLeftAreaCorner = bottomLeftAreaCorner;   // 왼쪽 아래
        this.TopRightAreaCorner = topRightAReaCorner;       // 오른쪽 위
        this.BottomRightAreaCorner = new Vector2Int(topRightAReaCorner.x, bottomLeftAreaCorner.y);  // 오른쪽 아래
        this.TopLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x, topRightAReaCorner.y);      // 왼쪽 위
        this.TreeLayerIndex = index;
    }

    public int Width { get => (int)(TopRightAreaCorner.x - BottomLeftAreaCorner.x); }   // 가로 길이 구하기
    public int Length { get => (int)(TopRightAreaCorner.y - BottomLeftAreaCorner.y); }  //세로 길이 구하기
}