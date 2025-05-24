using UnityEngine;

[System.Serializable]
public class PlayerLevelData
{
    // ������ �̱���
    [SerializeField] int _level;
    [SerializeField] int _unlockCost;
    [SerializeField] bool _isUnlock;

    public int Level { get => _level; }
    public int UnlockCost {  get => _unlockCost; }
    public bool IsUnlock { get => _isUnlock; }
}
