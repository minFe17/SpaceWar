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
        GenericSingleton<PlayerDataManager>.Instance.Passive.Add(_passiveButton[buttonIndex].Passive.Name);
        GenericSingleton<PassiveManager>.Instance.RemovePassive(_passiveButton[buttonIndex].Passive.Index);
        GenericSingleton<CsvController>.Instance.WriteDataFile();
        gameObject.SetActive(false);

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
