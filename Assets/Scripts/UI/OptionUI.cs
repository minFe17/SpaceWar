using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _keyInfoPanel;
    [SerializeField] GameObject _soundOptionPanel;

    public void CloseButton()
    {
        GenericSingleton<UIManager>.Instance.OnOffOptionUI(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GenericSingleton<UIManager>.Instance.Player.OptionUIState(false);
        AudioClip uiButtonSound = Resources.Load("Prefabs/SoundClip/UIButton") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(uiButtonSound);
    }

    public void keyInfoButton()
    {
        this.gameObject.SetActive(false);
        _keyInfoPanel.SetActive(true);
        GenericSingleton<UIManager>.Instance.IsKeyInfoUI = true;
        AudioClip uiButtonSound = Resources.Load("Prefabs/SoundClip/UIButton") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(uiButtonSound);
    }

    public void SoundOptionButton()
    {
        this.gameObject.SetActive(false);
        _soundOptionPanel.SetActive(true);
        GenericSingleton<UIManager>.Instance.IsSoundOption = true;
        AudioClip uiButtonSound = Resources.Load("Prefabs/SoundClip/UIButton") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(uiButtonSound);
    }

    public void LobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
        GenericSingleton<SoundCsv>.Instance.WriteSound();
        Application.Quit();
    }
}