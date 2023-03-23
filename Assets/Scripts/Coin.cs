using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Player _player;    //Init
    [SerializeField] Transform _target;
    [SerializeField] int _money;

    void Update()
    {
        Move();
    }

    void Move()
    {
        // 플레이어와의 거리가 어느정도 가까우면 자석처럼 따라가기
        // 코인 애니메이션 만들기
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            PlayerGetCoin();
    }

    void PlayerGetCoin()
    {
        _player.GetMoney(_money);
        Destroy(this.gameObject);
    }
}
