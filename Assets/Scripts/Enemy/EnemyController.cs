using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] BattleField _battleField;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _turret;
    [SerializeField] float _spawnDelay;
    [SerializeField] int _minEnemy;
    [SerializeField] int _maxEnemy;

    public List<GameObject> _enemyList = new List<GameObject>();

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
        //미니맵으로 클리어 표시?
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

    IEnumerator SpawnEnemyRoutine()
    {
        for (_waveIndex = 0; _waveIndex <= _wave; _waveIndex++)
        {

            yield return new WaitForSeconds(_spawnDelay);
            int enemy = Random.Range(_minEnemy, _maxEnemy);
            for (int j = 0; j <= enemy; j++)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                GameObject turret = Instantiate(_turret, spawnPos, Quaternion.identity);
                turret.GetComponent<Turret>().Init(this, _target);
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
