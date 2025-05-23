using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UIManager : MonoBehaviour
{
    // ╫л╠шео
    GameObject _ui;
    GameObject _uiPrefab;
    MainUI _mainUI;

    public IngameUI IngameUI { get; set; }
    public GameObject AimPoint { get; set; }
    public GameOverUI GameOverUI { get; set; }
    public GameObject OptionUI { get; set; }
    public SelectPassiveUI SelectPassiveUI { get; set; }

    public PlayerBase Player { get; set; }

    public GameObject InfoKey { get; set; }
    public Text InfoMessage {  get; set; }

    public bool IsKeyInfoUI { get; set; }
    public bool IsSoundOption {  get; set; }

    public async Task LoadAsset()
    {
        if (_uiPrefab != null)
            return;
        AddressableManager addressableManager = GenericSingleton<AddressableManager>.Instance;
        _uiPrefab = await addressableManager.GetAddressableAsset<GameObject>("UI");
    }

    public void CreateUI()
    {
        _ui = Instantiate(_uiPrefab);

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