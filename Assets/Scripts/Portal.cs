using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // 신 로드 or 화면 어둡게 했다 다시 밝게
    [SerializeField] GameManager _gameManager;
    bool _inPlayer;

    private void Update()
    {
        NextStage();
    }

    void NextStage()
    {
        if (Input.GetKeyDown(KeyCode.F) && _inPlayer)
        {
            _gameManager.NextStage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _inPlayer = true;
    }
}
