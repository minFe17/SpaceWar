using UnityEngine;

[System.Serializable]
public class GameData
{
    // 데이터 싱글턴
    [SerializeField] int _mapStage;
    [SerializeField] int _levelStage;
    [SerializeField] int _killEnemy;
    [SerializeField] float _playTime;
    [SerializeField] bool _isAddPassive;

    public int MapStage { get => _mapStage; set => _mapStage = value; }
    public int LevelStage { get => _levelStage; set => _levelStage = value; }
    public int KillEnemy { get => _killEnemy; set => _killEnemy = value; }
    public float PlayTime { get => _playTime; set => _playTime = value; }
    public bool IsAddPassive { get => _isAddPassive; set => _isAddPassive = value; }
}