using UnityEngine;
using Utils;

[System.Serializable]
public class GemData
{
    [SerializeField] int _gem = 90000;

    public int Gem { get => _gem; }

    public void AddGem(int value)
    {
        _gem += value;
        GenericSingleton<JsonManager>.Instance.WriteGemDataFile();
    }

    public void UseGem(int value)
    { 
        _gem -= value;
        GenericSingleton<JsonManager>.Instance.WriteGemDataFile();
    }
}
