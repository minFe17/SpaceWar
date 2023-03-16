using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // �� �ε� or ȭ�� ��Ӱ� �ߴ� �ٽ� ���
    [SerializeField] GameManager _gameManager;  //���߿� �ٸ������� �޾ƿ;���
    [SerializeField] GameObject _infoKeyUI;     //���߿� �ٸ������� �޾ƿ;���
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
