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

    [SerializeField] TMP_Text _coinText;

    [SerializeField] RawImage _miniMapImage;
    [SerializeField] TMP_Text _stageText;

    [SerializeField] TMP_Text _BulletText;

    [SerializeField] List<GameObject> _shotModeImageList = new List<GameObject>();
    [SerializeField] TMP_Text _shotModeText;

    [SerializeField] GameObject _bossHpBarBase;
    [SerializeField] Image _bossHpBar;

    [SerializeField] GameObject _miniBossHpBarBase;
    [SerializeField] Image _firstMiniBossHpBar;
    [SerializeField] Image _secondMiniBossHpBar;

    MiniScavenger _firstMiniScavenger;
    MiniScavenger _secondMiniScavenger;

    public void ShowHp()
    {
        int curHp = GenericSingleton<PlayerDataManager>.Instance.CurHp;
        int maxHp = GenericSingleton<PlayerDataManager>.Instance.MaxHp;
        _hpBar.fillAmount = (float)curHp / maxHp;
        if (curHp < 0)
            curHp = 0;
        _hpText.text = $"{curHp} / {maxHp}";
    }

    public void ShowMoney()
    {
        int money = GenericSingleton<PlayerDataManager>.Instance.Money;
        String text;
        if (money < 100)
            text = string.Format("{0:D3}", money);
        else
            text = String.Format("{0:#,0}", money);
        _coinText.text = text;
    }

    public void ShowStage(int mapStage, int levelStage)
    {
        _stageText.text = $"{mapStage} - {levelStage}";
    }

    public void ShowBullet()
    {
        int curBullet = GenericSingleton<PlayerDataManager>.Instance.CurBullet;
        int maxBullet = GenericSingleton<PlayerDataManager>.Instance.MaxBullet;
        _BulletText.text = $"{curBullet} / {maxBullet}";
    }

    public void ShowShotMode()
    {
        EShotModeType shotMode = GenericSingleton<PlayerDataManager>.Instance.ShotMode;
        for (int i = 0; i < (int)EShotModeType.Max; i++)
        {
            if (i == (int)shotMode)
                _shotModeImageList[i].SetActive(true);
            else
                _shotModeImageList[i].SetActive(false);
        }
        _shotModeText.text = shotMode.ToString();
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

    public void CreateMiniBossHpBar(GameObject firstMiniScavenger, GameObject secondMiniScavenger)
    {
        if (_firstMiniScavenger == null || _secondMiniScavenger == null)
        {
            _firstMiniScavenger = firstMiniScavenger.GetComponent<MiniScavenger>();
            _secondMiniScavenger = secondMiniScavenger.GetComponent<MiniScavenger>();
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
