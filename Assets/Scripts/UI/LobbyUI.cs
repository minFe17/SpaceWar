using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] GameObject _clickText;
    [SerializeField] GameObject _buttonPanel;
    [SerializeField] Button _continueGameButton;
    [SerializeField] Text _continueGameText;
    [SerializeField] GameObject _soundOption;

    AudioClipManager _auidoClipManager;
    float _disabledColorAlpha;

    void Start()
    {
        _clickText.SetActive(true);
        _buttonPanel.SetActive(false);
        _continueGameButton.interactable = false;
        _disabledColorAlpha = _continueGameButton.colors.disabledColor.a;
        _continueGameText.color = new Color(0, 0, 0, _disabledColorAlpha);
        GenericSingleton<SoundManager>.Instance.Init();
        _auidoClipManager = GenericSingleton<AudioClipManager>.Instance;
        _auidoClipManager.Init();
        _auidoClipManager.PlayBGM(EBGMAudioType.Lobby);
    }

    public void ClickLobby()
    {
        _auidoClipManager.PlaySFX(ESFXAudioType.Button);
        CsvController csvController = GenericSingleton<CsvController>.Instance;
        _clickText.SetActive(false);
        _buttonPanel.SetActive(true);
        if (csvController.CheckDataFile())
        {
            _continueGameButton.interactable = true;
            _continueGameText.color = new Color(0, 0, 0, 1);
        }
    }

    public void NewGameButton()
    {
        Time.timeScale = 1f;
        GenericSingleton<CsvController>.Instance.DestroyDataFile();
        SceneManager.LoadScene("FirstWorld");
    }

    public void ContinueGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene($"{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}");
    }

    public void OpenSoundOption()
    {
        _buttonPanel.SetActive(false);
        _soundOption.SetActive(true);
        _auidoClipManager.PlaySFX(ESFXAudioType.Button);
    }

    public void ExitGameButton()
    {
        GenericSingleton<SoundCsv>.Instance.WriteSound();
        Application.Quit();
    }
}