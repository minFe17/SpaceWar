using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class SelectPassiveUI : MonoBehaviour
{
    [SerializeField] List<PassiveButton> _passiveButton = new List<PassiveButton>();
    [SerializeField] GameObject _passiveInfoPanel;
    [SerializeField] Text _passiveName;
    [SerializeField] Text _passiveInfo;

    public List<PassiveButton> PassiveButton { get => _passiveButton; }

    public void SelectedPassive(int buttonIndex)
    {
        _passiveButton[buttonIndex].Passive.AddPassive();

        gameObject.SetActive(false);
        SceneManager.LoadScene($"{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}");
    }

    public void ShowInfo(int buttonIndex)
    {
        if(_passiveInfoPanel.activeSelf == false)
            _passiveInfoPanel.SetActive(true);

        _passiveName.text = _passiveButton[buttonIndex].Passive.Name;
        _passiveInfo.text = _passiveButton[buttonIndex].Passive.Info;
    }
}
