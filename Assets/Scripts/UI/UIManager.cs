using System;
using UnityEngine;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    GameObject _ui;
    IngameUI _ingameUI;
    GameObject _gameOverUI;
    GameObject _aimPoint;
    GameObject _optionUI;
    CinemachineFreeLook _followCam;

    bool _isOpen;

    public void CreateUI()
    {
        GameObject ui = Resources.Load("Prefabs/UI") as GameObject;
        _ui = Instantiate(ui);
        _ingameUI = ui.GetComponentInChildren<IngameUI>();
        _gameOverUI = _ingameUI.GetGameOverUI();
        _aimPoint = _ingameUI.GetAimPoint();
        _optionUI = _ingameUI.GetOptionUI();
    }

    public void SetFollowCam(CinemachineFreeLook followCam)
    {
        _followCam = followCam;
    }

    public void DestroyUI()
    {
        Destroy(_ui);
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

    //OptionUI
    public void OnOffOptionUI()
    {
        _isOpen = _optionUI.activeSelf == false;
        _followCam.enabled = !_isOpen;
        _optionUI.SetActive(_isOpen);
    }
}