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
        // �÷��̾���� �Ÿ��� ������� ������ �ڼ�ó�� ���󰡱�
        // ���� �ִϸ��̼� �����
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
