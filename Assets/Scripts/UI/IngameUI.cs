using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class IngameUI : MonoBehaviour
{
    [SerializeField] Image _hpBar;
    [SerializeField] TMP_Text _hpText;
    [SerializeField] Text _vendingMachineResultText;

    [SerializeField] TMP_Text _coinText;

    [SerializeField] TMP_Text _stageText;

    [SerializeField] TMP_Text _BulletText;

    [SerializeField] List<GameObject> _shootModeImageList = new List<GameObject>();
    [SerializeField] TMP_Text _shootModeText;

    [SerializeField] GameObject _bossHpBarBase;
    [SerializeField] Image _bossHpBar;

    [SerializeField] GameObject _miniBossHpBarBase;
    [SerializeField] Image _firstMiniBossHpBar;
    [SerializeField] Image _secondMiniBossHpBar;

    [SerializeField] GameObject _mapPanel;

    MiniScavenger _firstMiniScavenger;
    MiniScavenger _secondMiniScavenger;

    PlayerData _playerData = DataSingleton<PlayerData>.Instance;

    public void ShowHp()
    {
        int curHp = _playerData.CurHp;
        int maxHp = _playerData.MaxHp;
        _hpBar.fillAmount = (float)curHp / maxHp;
        if (curHp < 0)
            curHp = 0;
        _hpText.text = $"{curHp} / {maxHp}";
    }

    public void ShowVendingMachineResult(string result)
    {
        _vendingMachineResultText.gameObject.SetActive(true);
        _vendingMachineResultText.text = result;
        _vendingMachineResultText.GetComponent<VendingMachineResult>().Init();
    }

    public void ShowMoney()
    {
        int money = _playerData.Money;
        String text;
        if (money < 100)
            text = string.Format("{0:D3}", money);
        else
            text = String.Format("{0:#,0}", money);
        _coinText.text = text;
    }

    public void ShowMap()
    {
        if (_mapPanel.activeSelf == false)
            _mapPanel.SetActive(true);
    }

    public void HideMap()
    {
        _mapPanel.SetActive(false);
    }

    public void ShowStage()
    {
        GameData gameData = DataSingleton<GameData>.Instance;
        int mapStage = gameData.MapStage;
        int levelStage = gameData.LevelStage;
        _stageText.text = $"{mapStage} - {levelStage}";
    }

    public void ShowBullet()
    {
        int curBullet = _playerData.CurBullet;
        int maxBullet = _playerData.MaxBullet;
        _BulletText.text = $"{curBullet} / {maxBullet}";
    }

    // 수정 필요
    // 이미지 스프라이트를 리스트로?
    // Init할때 세팅?
    public void ShowShootMode()
    {
        EShootModeType shootMode = _playerData.ShootMode;
        for (int i = 0; i < (int)EShootModeType.Max; i++)
        {
            if (i == (int)shootMode)
                _shootModeImageList[i].SetActive(true);
            else
                _shootModeImageList[i].SetActive(false);
        }
        _shootModeText.text = shootMode.ToString();
    }

    public void ShowBossHpBar(int curHp, int maxHp)
    {
        if (_bossHpBarBase.activeSelf == false)
            _bossHpBarBase.SetActive(true);
        _bossHpBar.fillAmount = (float)curHp / maxHp;
    }

    public void HideBossHpBar()
    {
        _bossHpBarBase.SetActive(false);
    }

    public void CreateMiniBossHpBar(MiniScavenger firstMiniScavenger, MiniScavenger secondMiniScavenger)
    {
        if (_firstMiniScavenger == null || _secondMiniScavenger == null)
        {
            _firstMiniScavenger = firstMiniScavenger;
            _secondMiniScavenger = secondMiniScavenger;
        }
        ShowMiniBossHpBar();
    }

    public void ShowMiniBossHpBar()
    {
        if (_miniBossHpBarBase.activeSelf == false)
            _miniBossHpBarBase.SetActive(true);

        _firstMiniBossHpBar.fillAmount = (float)_firstMiniScavenger.CurHp / _firstMiniScavenger.MaxHp;
        _secondMiniBossHpBar.fillAmount = (float)_secondMiniScavenger.CurHp / _firstMiniScavenger.MaxHp;
    }

    public void HideMiniBossHpBar()
    {
        _miniBossHpBarBase.SetActive(false);
    }
}