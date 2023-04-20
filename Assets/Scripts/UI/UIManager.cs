using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    GameObject _ui;
    MainUI _mainUI;

    IngameUI _ingameUI;
    public IngameUI IngameUI { get { return _ingameUI; } set { _ingameUI = value; } }
    GameObject _aimPoint;
    public GameObject AimPoint { get { return _aimPoint; } set { _aimPoint = value; } }
    GameOverUI _gameOverUI;
    public GameOverUI GameOverUI { get { return _gameOverUI; } set { _gameOverUI = value; } }
    GameObject _optionUI;
    public GameObject OptionUI { get { return _optionUI; } set { _optionUI = value; } }

    Player _player;
    public Player Player { get { return _player; } set { _player = value; } }

    GameObject _InfoKey;
    public GameObject InfoKey { get { return _InfoKey; } set { _InfoKey = value; } }
    Text _infoMessage;
    public Text InfoMessage {  get { return _infoMessage; } set { _infoMessage = value; } }

    CinemachineFreeLook _followCam;
    public CinemachineFreeLook FollowCam { get { return _followCam; } set { _followCam = value; } }

    bool _isOpen;
    bool _isKeyInfoUI;
    public bool IsKeyInfoUI { get { return _isKeyInfoUI; } set { _isKeyInfoUI = value; } }

    public void CreateUI()
    {
        GameObject ui = Resources.Load("Prefabs/UI") as GameObject;
        _ui = Instantiate(ui);

        _mainUI = _ui.GetComponent<MainUI>();
        _mainUI.Init(this);
    }

    public void DestroyUI()
    {
        Destroy(_ui);
    }

    //OptionUI
    public void OnOffOptionUI()
    {
        _isOpen = (_optionUI.activeSelf == false);
        _followCam.enabled = !_isOpen;
        _optionUI.SetActive(_isOpen);
    }
}