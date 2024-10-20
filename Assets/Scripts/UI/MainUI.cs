using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] EPlayerPoolType _playerPoolType;
    [SerializeField] IngameUI _ingameUI;
    [SerializeField] GameObject _aimPoint;
    [SerializeField] GameOverUI _gameOverUI;
    [SerializeField] GameObject _optionUI;
    [SerializeField] SelectPassiveUI _selectPassiveUI;

    public void Init(UIManager uiManager)
    {
        uiManager.IngameUI = _ingameUI;
        uiManager.AimPoint = _aimPoint;
        uiManager.GameOverUI = _gameOverUI;
        uiManager.OptionUI = _optionUI;
        uiManager.SelectPassiveUI = _selectPassiveUI;
    }
}