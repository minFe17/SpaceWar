 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _keyInfoPanel;

    Player _player; //�÷��̾� �����ͷ� �̱��� ���� �����

    public void keyInfoButton()
    {
        _keyInfoPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CloseButton()
    {
        GenericSingleton<UIManager>.GetInstance().OnOffOptionUI();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        _player.OptionUIState(false);
    }
}
