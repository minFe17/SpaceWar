using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class SelectPassiveUI : MonoBehaviour
{
    [SerializeField] List<PassiveButton> _passiveButton = new List<PassiveButton>();
    public List<PassiveButton> PassiveButton { get => _passiveButton; }

    public void SelectedPassive()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene($"{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}");
    }

    public void Passive1()
    {
        SelectedPassive();
    }

    public void Passive2()
    {
        SelectedPassive();
    }

    public void Passive3()
    {
        SelectedPassive();
    }
}
