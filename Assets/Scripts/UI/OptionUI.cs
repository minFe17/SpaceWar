 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _keyInfoPanel;

    public void keyInfoButton()
    {
        _keyInfoPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CloseButton()
    {
        GenericSingleton<UIManager>.Instance.OnOffOptionUI();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GenericSingleton<UIManager>.Instance.Player.OptionUIState(false);
    }
}
