using UnityEngine;

[System.Serializable]
public class PlayerLevelData
{
    // ������ �̱���
    [SerializeField] int _level;
    [SerializeField] bool _isUnlock;

    public int Level { get => _level; set => _level = value; }
    public bool IsUnlock { get => _isUnlock; set => _isUnlock = value; }
}
