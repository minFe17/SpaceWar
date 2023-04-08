 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _keyInfoPanel;

    Player _player; //플레이어 데이터로 싱글톤 따로 만들기

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
