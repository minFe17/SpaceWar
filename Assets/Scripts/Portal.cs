using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // 신 로드 or 화면 어둡게 했다 다시 밝게
    [SerializeField] GameManager _gameManager;  //나중에 다른데에서 받아와야함
    [SerializeField] GameObject _infoKeyUI;     //나중에 다른데에서 받아와야함
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
            _inPlayer = false;
        }
    }

    void ShowKeyUI()
    {
        _infoKeyUI.SetActive(true);
    }

    void HideKeyUI()
    {
        _infoKeyUI.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowKeyUI();
            _inPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HideKeyUI();
        _inPlayer = false;
    }
}
