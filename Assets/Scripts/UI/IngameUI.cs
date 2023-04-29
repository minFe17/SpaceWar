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

    [SerializeField] TMP_Text _ammoText;

    [SerializeField] List<GameObject> _shotModeImageList = new List<GameObject>();
    [SerializeField] TMP_Text _shotModeText;

    [SerializeField] GameObject _bossHpBarBase;
    [SerializeField] Image _bossHpBar;

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

    public void ShowAmmo()
    {
        int curAmmo = GenericSingleton<PlayerDataManager>.Instance.CurAmmo;
        int maxAmmo = GenericSingleton<PlayerDataManager>.Instance.MaxAmmo;
        _ammoText.text = $"{curAmmo} / {maxAmmo}";
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
}
