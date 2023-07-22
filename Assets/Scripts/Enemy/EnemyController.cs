using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _spawnDelay;
    [SerializeField] int _minEnemy;
    [SerializeField] int _maxEnemy;

    List<Enemy> _enemyList = new List<Enemy>();

    DoorList _doorList;
    BoxCollider _ground;
    Player _player;

    Vector3 _createPos;
    Vector3 _basePos;
    Vector3 _size;

    int _wave;
    int _waveIndex;

    bool _isClear;
    bool _isBossRoom;

    public List<Enemy> EnemyList { get => _enemyList; set => _enemyList = value; }

    void Awake()
    {
        _ground = GetComponent<BoxCollider>();
        _wave = Random.Range(1, 3);
        _isClear = false;
    }

    public void Init(Vector3 createPos, DoorList doorList, bool isBossRoom)
    {
        _createPos = createPos;
        _doorList = doorList;
        _isBossRoom = isBossRoom;
        _basePos = _createPos + _ground.center;
        _size = _ground.size;
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
            if(transform.parent.GetComponent<ClearRoom>() == null)
                transform.parent.AddComponent<ClearRoom>();
            GenericSingleton<GameManager>.Instance.Clear(_doorList);
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();

        if (GenericSingleton<EnemyManager>.Instance.Enemys.Count > 0)
        {
            int ramdom = Random.Range(0, GenericSingleton<EnemyManager>.Instance.Enemys.Count);
            GameObject enemy = Instantiate(GenericSingleton<EnemyManager>.Instance.Enemys[ramdom], spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(this);
        }
    }

    public Vector3 GetRandomSpawnPosition()
    {
        float posX = _basePos.x + Random.Range(-_size.x / 2f, _size.x / 2f);
        float posZ = _basePos.z + Random.Range(-_size.z / 2f, _size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, _basePos.y, posZ);

        return spawnPos;
    }

    public void SpawnBoss()
    {
        GenericSingleton<GameManager>.Instance.Portal.SetActive(false);
        EWorldType eWorld = (EWorldType)GenericSingleton<GameManager>.Instance.MapStage;
        GameObject temp = Resources.Load($"Prefabs/Enemys/{eWorld}/Boss") as GameObject;
        GameObject boss = Instantiate(temp);
        boss.transform.position = _basePos;
        boss.GetComponent<Enemy>().Init(this);
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
            _player = other.gameObject.GetComponent<Player>();
            _player.EnemyController = this;
            GenericSingleton<GameManager>.Instance.Battle(_doorList);
            _ground.enabled = false;
            if (!_isBossRoom)
                StartCoroutine(SpawnEnemyRoutine());
            else
                SpawnBoss();
        }
    }
}