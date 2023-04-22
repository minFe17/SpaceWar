using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public List<GameObject> shotModeImageList = new List<GameObject>();
    [SerializeField] TMP_Text _shotModeText;

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
        switch(shotMode)
        {
            case EShotModeType.Single:
                shotModeImageList[0].SetActive(true);
                shotModeImageList[1].SetActive(false);
                shotModeImageList[2].SetActive(false);
                _shotModeText.text = "Single";
                break;
            case EShotModeType.Burst:
                shotModeImageList[0].SetActive(false);
                shotModeImageList[1].SetActive(true);
                shotModeImageList[2].SetActive(false);
                _shotModeText.text = "Burst";
                break;
            case EShotModeType.Auto:
                shotModeImageList[0].SetActive(false);
                shotModeImageList[1].SetActive(false);
                shotModeImageList[2].SetActive(true);
                _shotModeText.text = "Auto";
                break;
        }
    }

    
}
