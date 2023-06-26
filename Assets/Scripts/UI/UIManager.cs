using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    GameObject _ui;
    MainUI _mainUI;

    public IngameUI IngameUI { get; set; }
    public GameObject AimPoint { get; set; }
    public GameOverUI GameOverUI { get; set; }
    public GameObject OptionUI { get; set; }
    public SelectPassiveUI SelectPassiveUI { get; set; }

    public Player Player { get; set; }

    public GameObject InfoKey { get; set; }
    public Text InfoMessage {  get; set; }

    public bool IsKeyInfoUI { get; set; }
    public bool IsSoundOption {  get; set; }

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

    public void OnOffOptionUI(bool isOpen)
    {
        OptionUI.SetActive(isOpen);
    }
}