using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // �� �ε� or ȭ�� ��Ӱ� �ߴ� �ٽ� ���
    Player _player;

    bool _inPlayer;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        NextStage();
    }

    void NextStage()
    {
        if (Input.GetKeyDown(KeyCode.F) && _inPlayer)
        {
            GenericSingleton<GameManager>.GetInstance().NextStage();
            _inPlayer = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player.ShowPortalKeyUI();
            _inPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player.HidePortalKeyUI();
            _inPlayer = false;
        }
    }
}
