using UnityEngine;

[System.Serializable]
public class GemData
{
    [SerializeField] int _gem = 90000;

    public int Gem { get => _gem; }

    public void AddGem()
    {

    }

    public void UseGem(int value)
    { 
        _gem -= value;
    }
}
