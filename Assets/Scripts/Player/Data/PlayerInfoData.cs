using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerInfoData
{
    // 데이터 싱글턴
    // 읽기 전용
    [SerializeField] protected List<PlayerStatData> _statDataList = new List<PlayerStatData>();
    [SerializeField] protected List<string> _skillName = new List<string>();
    [SerializeField] protected int _unlockCost;

    protected EPlayerType _playerType;
    protected string _name;

    public List<PlayerStatData> StatDataList { get => _statDataList; }
    public List<string> SkillName { get => _skillName; }
    public string Name { get => _name; }
    public int UnlockCost { get => _unlockCost; }

    // 호출 필요
    public abstract void Init();
}