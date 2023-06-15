using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _keyInfoPanel;

    public void keyInfoButton()
    {
        this.gameObject.SetActive(false);
        _keyInfoPanel.SetActive(true);
        GenericSingleton<UIManager>.Instance.IsKeyInfoUI = true;
    }

    public void CloseButton()
    {
        GenericSingleton<UIManager>.Instance.OnOffOptionUI();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GenericSingleton<UIManager>.Instance.Player.OptionUIState(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}
