using UnityEngine;

[System.Serializable]
public class PlayerLevelData
{
    // 데이터 싱글턴
    [SerializeField] int _level;
    [SerializeField] int _unlockCost;
    [SerializeField] bool _isUnlock;

    public int Level { get => _level; }
    public int UnlockCost {  get => _unlockCost; }
    public bool IsUnlock { get => _isUnlock; }
}
