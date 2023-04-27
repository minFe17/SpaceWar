using UnityEngine;
using Utils;

public class KeyInfoUI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseButton();
    }

    public void CloseButton()
    {
        this.gameObject.SetActive(false);
        GenericSingleton<UIManager>.Instance.OptionUI.SetActive(true);
        GenericSingleton<UIManager>.Instance.IsKeyInfoUI = false;
    }
}
