using UnityEngine;

[System.Serializable]
public class GemData
{
    [SerializeField] int _gem = 90000;

    public int Gem { get => _gem; set => _gem = value; }
}
