using UnityEngine;

[System.Serializable]
public class GemData
{
    [SerializeField] int _gem;

    public int Gem { get => _gem; set => _gem = value; }
}
