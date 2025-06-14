using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EGroundWorkType _groundWorkType;
    [SerializeField] float _spawnDelay;
    [SerializeField] int _minEnemy;
    [SerializeField] int _maxEnemy;

    UIManager _uiManager;
    FactoryManager _factoryManager;
    EnemyManager _enemyManager;
    List<Enemy> _enemyList = new List<Enemy>();

    DoorManager _doorManager;
    BoxCollider _ground;
    PlayerBase _player;

    Vector3 _createPos;
    Vector3 _basePos;
    Vector3 _size;

    int _wave;
    int _waveIndex;

    float _searchRadius = 20f;
    float _frontAngle = 60f;

    bool _isClear;
    bool _isBossRoom;

    public List<Enemy> EnemyList { get => _enemyList; set => _enemyList = value; }

    void Awake()
    {
        _enemyManager = GenericSingleton<EnemyManager>.Instance;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _doorManager = GenericSingleton<DoorManager>.Instance;
        _ground = GetComponent<BoxCollider>();
    }

    public void Init(Vector3 createPos, bool isBossRoom)
    {
        _isClear = false;
        _ground.enabled = true;
        _wave = Random.Range(1, 3);
        _waveIndex = 0;
        _createPos = createPos;
        _isBossRoom = isBossRoom;
        _basePos = _createPos + _ground.center;
        _size = _ground.size;
        _uiManager = GenericSingleton<UIManager>.Instance;
    }

    void Update()
    {
        ClearCheck();
    }

    public void ClearCheck()
    {
        if (_waveIndex == _wave && _enemyList.Count == 0)
        {
            _isClear = true;
            if (transform.parent.GetComponent<ClearRoom>() == null)
                transform.parent.AddComponent<ClearRoom>();
            _doorManager.UnlockDoor();
        }
    }

    public void SpawnEnemy()
    {
        _enemyManager.EnemyController = this;
        Vector3 spawnPos = GetRandomSpawnPosition();

        if (GenericSingleton<EnemyManager>.Instance.Enemys.Count > 0)
        {
            int random = Random.Range(0, GenericSingleton<EnemyManager>.Instance.Enemys.Count);
            Enum value = _enemyManager.ConvertEnumToInt(random);
            Enemy enemy = _factoryManager.EnemyFactory.MakeObject(value);
            enemy.transform.position = spawnPos;
            enemy.Init(this);
        }
    }

    public Vector3 GetRandomSpawnPosition()
    {
        float posX = _basePos.x + Random.Range(-_size.x / 2f, _size.x / 2f);
        float posZ = _basePos.z + Random.Range(-_size.z / 2f, _size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, _basePos.y, posZ);

        return spawnPos;
    }

    public Transform GetClosestEnemy()
    {
        Vector3 playerPos = _enemyManager.Target.position;
        Vector3 playerForward = _enemyManager.Target.forward;

        List<Transform> enemyList = _enemyList.Where(temp =>
        {
            Vector3 enemy = temp.transform.position - playerPos;
            float distance = enemy.magnitude;
            if (distance > _searchRadius)
                return false;

            float angle = Vector3.Angle(playerForward, enemy);
            return angle <= _frontAngle / 2f;
        }).Select(temp => temp.transform).ToList();

        if(enemyList.Count > 0)
        {
            int randomIndex = Random.Range(0, enemyList.Count);
            return enemyList[randomIndex];
        }
        return null;
    }

    public void SpawnBoss()
    {
        GenericSingleton<GameManager>.Instance.Portal.SetActive(false);
        GameObject bossGameObject = Instantiate(_enemyManager.Boss);
        bossGameObject.transform.position = _basePos;
        Enemy boss = bossGameObject.GetComponent<Enemy>();
        boss.Init(this);
        _uiManager.IngameUI.ShowBossHpBar(boss.CurHp, boss.MaxHp);
    }

    public void StopSpawnEnemy()
    {
        StopCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        for (_waveIndex = 0; _waveIndex < _wave; _waveIndex++)
        {
            if (_player.IsDie == true)
                break;

            yield return new WaitForSeconds(_spawnDelay);
            int enemyCount = Random.Range(_minEnemy, _maxEnemy);
            for (int j = 0; j <= enemyCount; j++)
            {
                SpawnEnemy();
            }

            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                if (_enemyList.Count == 0)
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_isClear)
        {
            _player = other.gameObject.GetComponent<PlayerBase>();
            _player.EnemyController = this;
            _doorManager.LockDoor();
            _ground.enabled = false;
            if (!_isBossRoom)
                StartCoroutine(SpawnEnemyRoutine());
            else
                SpawnBoss();
        }
    }
}