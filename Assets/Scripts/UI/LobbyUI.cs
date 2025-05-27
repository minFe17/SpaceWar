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
        Time.timeScale = 1.0f;
        if (!GenericSingleton<AssetManager>.Instance.IsLoad)
            await LoadAsset();
        _animator.SetBool("isLoad", true);
        _clickText.SetActive(true);
        _buttonPanel.SetActive(false);

        GenericSingleton<SoundManager>.Instance.Init();

        await GenericSingleton<PlayerStatManager>.Instance.Init();
        GenericSingleton<JsonManager>.Instance.ReadDataFile();
        GenericSingleton<JsonManager>.Instance.ReadGemDataFile();
        _continueGameButton.interactable = false;
        _auidoClipManager = GenericSingleton<AudioClipManager>.Instance;
        _disabledColorAlpha = _continueGameButton.colors.disabledColor.a;
        _continueGameText.color = new Color(0, 0, 0, _disabledColorAlpha);
    }

    async Task LoadAsset()
    {
        await GenericSingleton<AssetManager>.Instance.LoadAsset();
    }

    public void ClickLobby()
    {
        _auidoClipManager.PlaySFX(ESFXAudioType.Button);
        JsonManager jsonManager = GenericSingleton<JsonManager>.Instance;
        _clickText.SetActive(false);
        _buttonPanel.SetActive(true);
        if (jsonManager.CheckDataFiles())
        {
            _continueGameButton.interactable = true;
            _continueGameText.color = new Color(0, 0, 0, 1);
        }
    }

    public void NewGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChoiceCharacterScene");
    }

    public async void ContinueGameButton()
    {
        Time.timeScale = 1f;
        await GenericSingleton<WorldManager>.Instance.ContinueGame();
        SceneManager.LoadScene($"{(EWorldType)DataSingleton<GameData>.Instance.MapStage}");
    }

    public void OpenSoundOption()
    {
        _buttonPanel.SetActive(false);
        _soundOption.SetActive(true);
        _auidoClipManager.PlaySFX(ESFXAudioType.Button);
    }

    public void ExitGameButton()
    {
        GenericSingleton<JsonManager>.Instance.WriteSoundDataFile();
        Application.Quit();
    }
}