using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �̱���
    GameObject _ui;
    IngameUI _ingameUI;
    GameObject _gameOverUI;
    GameObject _aimPoint;

    public void CreateUI()
    {
        _ui = Resources.Load("Prefabs/UI") as GameObject;
        GameObject ui = Instantiate(_ui);
        _ingameUI = ui.GetComponentInChildren<IngameUI>();
        _gameOverUI = _ingameUI.SetGameOverUI();
        _aimPoint = _ingameUI.SetAimPoint();
    }

    //IngameUI
    public void ShowStage(int mapStage, int levelStage)
    {
        _ingameUI.ShowStage(mapStage, levelStage);
    }

    public void ShowHp(int curHp, int maxHp)
    {
        _ingameUI.ShowHp(curHp, maxHp);
    }

    public void ShowCurrentMoney(int money)
    {
        _ingameUI.ShowMoney(money);
    }

    public void ShowAmmo(int curAmmo, int maxAmmo)
    {
        _ingameUI.ShowAmmo(curAmmo, maxAmmo);
    }

    public void ShowShotMode(EShotModeType shotMode)
    {
        _ingameUI.ShowShotMode(shotMode);
    }

    // AimPoint
    public void OnAimPoint()
    {
        _aimPoint.SetActive(true);
    }

    public void OffAimPoint()
    {
        _aimPoint.SetActive(false);
    }

    // gameOverUI
    public void Die()
    {
        _gameOverUI.gameObject.SetActive(true);
    }

    public void ShowPlayTime(float time)
    {
        _gameOverUI.GetComponent<GameOverUI>().ShowPlayTime(time);
    }

    public void ShowKillEnemy(int killEnemy)
    {
        _gameOverUI.GetComponent<GameOverUI>().ShowKillEnemy(killEnemy);
    }

    public void ShowMoney(int money)
    {
        _gameOverUI.GetComponent<GameOverUI>().ShowMoney(money);
    }
}