using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] IngameUI _ingameUI;
    [SerializeField] GameObject _aimPoint;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] GameObject _optionUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IngameUI GetIngameUI()
    {
        return _ingameUI;
    }

    public GameObject GetGameOverUI()
    {
        return _gameOverUI;
    }

    public GameObject GetAimPoint()
    {
        return _aimPoint;
    }

    public GameObject GetOptionUI()
    {
        return _optionUI;
    }
}
