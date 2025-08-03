using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    /// <summary>
    /// 주어진 공간 리스트 내 각 영역에 대해 방 크기를 조정하여 새로운 방 노드를 생성
    /// 각 영역의 좌하단과 우상단 좌표를 수정하여 방 크기와 위치를 조절
    /// </summary>
    /// <param name="roomSpaces">조정할 공간 리스트</param>
    /// <param name="roomBottomCornerModifier">좌하단 좌표 보정 비율</param>
    /// <param name="roomTopCornerModifier">우상단 좌표 보정 비율</param>
    /// <param name="roomOffset">좌표 보정 오프셋</param>
    /// <returns>크기 조정된 방 노드 리스트</returns>
    public List<RoomNode> GenerateRoomsInGivenSpace(List<Node> roomSpaces, float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();

        foreach (Node space in roomSpaces)
        {
            // BottomLeft 좌표를 새 값으로 계산
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(space.BottomLeftAreaCorner,space.TopRightAreaCorner,roomBottomCornerModifier, roomOffset);

            // TopRight 좌표를 새 값으로 계산
            Vector2Int newTopRightPoint = StructureHelper.GenerateTopRightCornerBetween(space.BottomLeftAreaCorner,space.TopRightAreaCorner, roomTopCornerModifier,roomOffset);

            // 좌표 값 갱신
            space.BottomLeftAreaCorner = newBottomLeftPoint;
            space.TopRightAreaCorner = newTopRightPoint;
            space.BottomRightAreaCorner = new Vector2Int(newTopRightPoint.x, newBottomLeftPoint.y);
            space.TopLeftAreaCorner = new Vector2Int(newBottomLeftPoint.x, newTopRightPoint.y);

            listToReturn.Add((RoomNode)space);
        }
        return listToReturn;
    }
}