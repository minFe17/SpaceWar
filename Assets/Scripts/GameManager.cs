using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void Battle(DoorList doorList)
    {
        doorList.LockDoor();
        //몬스터 소환
        //전투
        //전투 끝나면 문 콜라이더 켜기
    }
}
