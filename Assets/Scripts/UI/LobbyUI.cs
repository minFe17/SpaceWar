using System.Threading.Tasks;
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

    Animator _animator;
    float _disabledColorAlpha;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isLoad", false);
    }

    async void Start()
    {
        await LoadAsset();
        _animator.SetBool("isLoad", true);
        _clickText.SetActive(true);
        _buttonPanel.SetActive(false);
        GenericSingleton<SoundManager>.Instance.Init();
        _continueGameButton.interactable = false;
        _disabledColorAlpha = _continueGameButton.colors.disabledColor.a;
        _continueGameText.color = new Color(0, 0, 0, _disabledColorAlpha);
        _auidoClipManager = GenericSingleton<AudioClipManager>.Instance;
    }

    async Task LoadAsset()
    {
        await GenericSingleton<AssetManager>.Instance.LoadAsset();
    }

    public void ClickLobby()
    {
        _auidoClipManager.PlaySFX(ESFXAudioType.Button);
        CsvManager csvManager = GenericSingleton<CsvManager>.Instance;
        _clickText.SetActive(false);
        _buttonPanel.SetActive(true);
        if (csvManager.CheckDataFiles())
        {
            _continueGameButton.interactable = true;
            _continueGameText.color = new Color(0, 0, 0, 1);
        }
    }

    public void NewGameButton()
    {
        Time.timeScale = 1f;
        GenericSingleton<CsvManager>.Instance.DestroyDataFiles();
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
        GenericSingleton<CsvManager>.Instance.WriteSoundDataFile();
        Application.Quit();
    }
}