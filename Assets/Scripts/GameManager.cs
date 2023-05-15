using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    // �̱���
    public GameObject Portal { get; set; }

    int _mapStage = 1;
    int _levelStage = 1;
    int _killEnemy;
    float _playTime;
    bool _isAddPassive = true;

    public int MapStage { get => _mapStage; }
    public int LevelStage { get => _levelStage; }

    void Update()
    {
        PlayTime();
    }

    public void Battle(DoorList doorList)
    {
        doorList.LockDoor();
    }

    public void Clear(DoorList doorList)
    {
        doorList.UnlockDoor();
    }

    public void NextStage()
    {
        if (_levelStage >= 5)
        {
            _mapStage++;
            _levelStage = 1;
            GenericSingleton<EnemyManager>.Instance.ClearWorldEnemy();
            GenericSingleton<EnemyManager>.Instance.WorldEnemyList();
        }
        else
        {
            _levelStage++;
        }

        if (_isAddPassive == true)
        {
            _isAddPassive = false;
            SelectPassive();
        }
        else
            _isAddPassive = true;

        // ������ ����
        SceneManager.LoadScene($"{(EWorldType)_mapStage}");
    }

    public void AddKillEnemy()
    {
        _killEnemy++;
    }

    public void StageUI()
    {
        GenericSingleton<UIManager>.Instance.IngameUI.ShowStage(_mapStage, _levelStage);
    }

    void PlayTime()
    {
        _playTime += Time.deltaTime;
    }

    public void SelectPassive()
    {
        // ui ����
        // ������ �޾Ƽ� 3���� �����ֱ�
        // ������ ������ ���̵� x
        // �̸�, ���� ����� ���̱�
        // ���� �� ui ����� �� �̵�
    }

    public void GameOver()
    {
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowPlayTime(_playTime);
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowKillEnemy(_killEnemy);
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowWave(_mapStage, _levelStage);
    }
}

public enum EWorldType
{
    None,
    FirstWorld,
    SecondWorld,
    ThirdWorld,
}