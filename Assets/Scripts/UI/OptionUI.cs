using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _keyInfoPanel;
    [SerializeField] GameObject _soundOptionPanel;

    AudioClipManager _audioClipManaer;

    private void Start()
    {
        _audioClipManaer = GenericSingleton<AudioClipManager>.Instance;
    }

    public void CloseButton()
    {
        GenericSingleton<UIManager>.Instance.OnOffOptionUI(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GenericSingleton<UIManager>.Instance.Player.OptionUIState(false);
        _audioClipManaer.PlaySFX(ESFXAudioType.Button);
    }

    public void keyInfoButton()
    {
        this.gameObject.SetActive(false);
        _keyInfoPanel.SetActive(true);
        GenericSingleton<UIManager>.Instance.IsKeyInfoUI = true;
        _audioClipManaer.PlaySFX(ESFXAudioType.Button);
    }

    public void SoundOptionButton()
    {
        this.gameObject.SetActive(false);
        _soundOptionPanel.SetActive(true);
        GenericSingleton<UIManager>.Instance.IsSoundOption = true;
        _audioClipManaer.PlaySFX(ESFXAudioType.Button);
    }

    public void LobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
        GenericSingleton<CsvManager>.Instance.WriteSoundDataFile();
        Application.Quit();
    }
}