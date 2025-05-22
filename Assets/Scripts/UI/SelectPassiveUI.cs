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
        CsvManager csvManager = GenericSingleton<CsvManager>.Instance;

        _passiveButton[buttonIndex].Passive.AddPassive();
        DataSingleton<SelectPassiveData>.Instance.PassiveList.Add(_passiveButton[buttonIndex].Passive);
        GenericSingleton<PassiveManager>.Instance.RemovePassive(_passiveButton[buttonIndex].Passive);
        csvManager.WriteDataFile();
        while (csvManager.IsWriting == true || csvManager.CheckDataFiles() == false)
        {
            if (csvManager.IsWriting == false && csvManager.CheckDataFiles() == true)
                break;
        }

        gameObject.SetActive(false);
        GenericSingleton<DoorManager>.Instance.ClearDoors();
        SceneManager.LoadScene($"{(EWorldType)DataSingleton<GameData>.Instance.MapStage}");
    }

    public void ShowInfo(int buttonIndex)   // Event Trigger�� ����ϴ� �Լ�
    {
        if (_passiveInfoPanel.activeSelf == false)
            _passiveInfoPanel.SetActive(true);

        _passiveNameText.text = _passiveButton[buttonIndex].Passive.Name;
        _passiveInfoText.text = _passiveButton[buttonIndex].Passive.Info;
    }
}