using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class SelectPassiveUI : MonoBehaviour
{
    [SerializeField] List<PassiveButton> _passiveButton = new List<PassiveButton>();
    [SerializeField] GameObject _passiveInfoPanel;
    [SerializeField] Text _passiveNameText;
    [SerializeField] Text _passiveInfoText;

    public List<PassiveButton> PassiveButton { get => _passiveButton; }

    public void SelectedPassive(int buttonIndex)
    {
        _passiveButton[buttonIndex].Passive.AddPassive();

        gameObject.SetActive(false);
        GenericSingleton<PassiveManager>.Instance.Passive.Remove(_passiveButton[buttonIndex].Passive);
        SceneManager.LoadScene($"{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}");
    }

    public void ShowInfo(int buttonIndex)
    {
        if(_passiveInfoPanel.activeSelf == false)
            _passiveInfoPanel.SetActive(true);

        _passiveNameText.text = _passiveButton[buttonIndex].Passive.Name;
        _passiveInfoText.text = _passiveButton[buttonIndex].Passive.Info;
    }
}
