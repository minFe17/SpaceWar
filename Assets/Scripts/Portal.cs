using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // 신 로드 or 화면 어둡게 했다 다시 밝게
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
