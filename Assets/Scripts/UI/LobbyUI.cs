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

    AudioClip _lobbySound;
    float _disabledColorAlpha;

    void Start()
    {
        _clickText.SetActive(true);
        _buttonPanel.SetActive(false);
        _continueGameButton.interactable = false;
        _disabledColorAlpha = _continueGameButton.colors.disabledColor.a;
        _continueGameText.color = new Color(0, 0, 0, _disabledColorAlpha);
        _lobbySound = Resources.Load("Prefabs/SoundClip/Lobby") as AudioClip;
        GenericSingleton<SoundManager>.Instance.Init();
        GenericSingleton<SoundManager>.Instance.SoundController.StartBGM(_lobbySound);
    }

    public void ClickButton()
    {
        AudioClip uiButtonSound = Resources.Load("Prefabs/SoundClip/UIButton") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(uiButtonSound);
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
        GenericSingleton<CsvController>.Instance.DestroyDataFile();
        SceneManager.LoadScene("FirstWorld");
    }

    public void ContinueGameButton()
    {
        SceneManager.LoadScene($"{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}");
    }

    public void OpenSoundOption()
    {
        _buttonPanel.SetActive(false);
        _soundOption.SetActive(true);
        AudioClip uiButtonSound = Resources.Load("Prefabs/SoundClip/UIButton") as AudioClip;
        GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(uiButtonSound);
    }

    public void ExitGameButton()
    {
        GenericSingleton<SoundCsv>.Instance.WriteSound();
        Application.Quit();
    }
}
