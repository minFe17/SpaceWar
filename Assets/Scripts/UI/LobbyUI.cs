using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        _continueGameText.color = new Color(0,0,0, _disabledColorAlpha);
    }

    public void ClickButton()
    {
        _clickText.SetActive(false);
        _buttonPanel.SetActive(true);
        // 데이터 있으면 이어하기 버튼 활성화
        // 없으면 비활성화
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("FirstWorld");
    }

    public void ContinueGameButton()
    {

    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
