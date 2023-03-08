using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] BattleField _battleField;
    [SerializeField] Player _player;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _turret;
    [SerializeField] float _spawnDelay;
    [SerializeField] int _minEnemy;
    [SerializeField] int _maxEnemy;

    public GameObject[] enemys;
    public List<Enemy> _enemyList = new List<Enemy>();

    BoxCollider _ground;

    Vector3 _basePos;
    Vector3 _size;

    int _wave;
    int _waveIndex;

    bool _isClear;

    void Start()
    {
        _ground = GetComponent<BoxCollider>();
        _basePos = transform.position + _ground.center;
        _wave = Random.Range(0, 2);
        _size = _ground.size;
        StartCoroutine(SpawnEnemyRoutine());
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
            _battleField.Clear();
        }
    }

    public Vector3 GetRandomSpawnPosition()
    {
        float posX = _basePos.x + Random.Range(-_size.x / 2f, _size.x / 2f);
        float posZ = _basePos.z + Random.Range(-_size.z / 2f, _size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, transform.position.y, posZ);

        return spawnPos;
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        int ramdom = Random.Range(0, enemys.Length);
        GameObject enemy = Instantiate(enemys[ramdom], spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Init(_player, this, _target);
    }

    IEnumerator SpawnEnemyRoutine()
    {
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
}
