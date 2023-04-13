using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _spawnDelay;
    [SerializeField] int _minEnemy;
    [SerializeField] int _maxEnemy;

    public List<Enemy> _enemyList = new List<Enemy>();
    List<GameObject> _enemys = new List<GameObject>();

    DoorList _doorList;
    BoxCollider _ground;

    Vector3 _createPos;
    Vector3 _basePos;
    Vector3 _size;

    int _wave;
    int _waveIndex;

    bool _isClear;

    void Awake()
    {
        _ground = GetComponent<BoxCollider>();
        _wave = Random.Range(0, 2);
    }

    public void Init(Vector3 createPos, DoorList doorList)
    {
        _createPos = createPos;
        _doorList = doorList;
        _basePos = _createPos + _ground.center;
        _size = _ground.size;
    }

    void AddEnemyList()
    {
        switch(GenericSingleton<GameManager>.Instance.MapStage)
        {
            case 1:
                FirstWorldEnemy();
                break;
            case 2:
                SecondWorldEnemy();
                break;
            case 3:
                ThirdWorldEnemy();
                break;
        }
    }

    void FirstWorldEnemy()
    {
        for(int i=0; i<(int)EFirstWorldEnemyType.Max; i++)
        {
            _enemys.Add(Resources.Load($"Prefabs/Enemys/FirstWorld/{(EFirstWorldEnemyType)i}") as GameObject);
        }
    }

    void SecondWorldEnemy()
    {

    }

    void ThirdWorldEnemy()
    {

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
            GenericSingleton<GameManager>.Instance.Clear(_doorList);
        }
    }

    public Vector3 GetRandomSpawnPosition()
    {
        float posX = _basePos.x + Random.Range(-_size.x / 2f, _size.x / 2f);
        float posZ = _basePos.z + Random.Range(-_size.z / 2f, _size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, _basePos.y, posZ);

        return spawnPos;
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        int ramdom = Random.Range(0, (int)EFirstWorldEnemyType.Max);
        GameObject enemy = Instantiate(_enemys[ramdom], spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Init(this);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        if (_enemys.Count == 0)
            AddEnemyList();
        for (_waveIndex = 0; _waveIndex <= _wave; _waveIndex++)
        {
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
        if (other.gameObject.tag == "Player")
        {
            GenericSingleton<GameManager>.Instance.Battle(_doorList);
            _ground.enabled = false;
            StartCoroutine(SpawnEnemyRoutine());
        }
    }
}

public enum EFirstWorldEnemyType
{
    Turret,
    Scorpion,
    DeliveryRobot,
    Max,
}
