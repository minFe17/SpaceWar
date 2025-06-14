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
        JsonManager jsonManager = GenericSingleton<JsonManager>.Instance;

        _passiveButton[buttonIndex].Passive.AddPassive();
        DataSingleton<SelectPassiveData>.Instance.PassiveList.Add(_passiveButton[buttonIndex].Passive);
        GenericSingleton<PassiveManager>.Instance.RemovePassive(_passiveButton[buttonIndex].Passive);
        jsonManager.WriteDataFile();
        while (jsonManager.IsWriting == true || jsonManager.CheckDataFiles() == false)
        {
            if (jsonManager.IsWriting == false && jsonManager.CheckDataFiles() == true)
                break;
        }

        gameObject.SetActive(false);
        GenericSingleton<DoorManager>.Instance.ClearDoors();
        SceneManager.LoadScene($"{(EWorldType)DataSingleton<GameData>.Instance.MapStage}");
    }

    // Event Trigger를 사용하는 함수
    public void ShowInfo(int buttonIndex)   
    {
        if (_passiveInfoPanel.activeSelf == false)
            _passiveInfoPanel.SetActive(true);

        _passiveNameText.text = _passiveButton[buttonIndex].Passive.Name;
        _passiveInfoText.text = _passiveButton[buttonIndex].Passive.Info;
    }
}