using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] IngameUI _ingameUI;
    [SerializeField] GameObject _aimPoint;
    [SerializeField] GameOverUI _gameOverUI;
    [SerializeField] GameObject _optionUI;

    public void Init(UIManager uiManager)
    {
        uiManager.IngameUI = _ingameUI;
        uiManager.AimPoint = _aimPoint;
        uiManager.GameOverUI = _gameOverUI;
        uiManager.OptionUI = _optionUI;
    }
}