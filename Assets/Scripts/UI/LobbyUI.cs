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

    float _disabledColorAlpha;

    void Start()
    {
        _clickText.SetActive(true);
        _buttonPanel.SetActive(false);
        _continueGameButton.interactable = false;
        _disabledColorAlpha = _continueGameButton.colors.disabledColor.a;
        _continueGameText.color = new Color(0, 0, 0, _disabledColorAlpha);
    }

    public void ClickButton()
    {
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

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
