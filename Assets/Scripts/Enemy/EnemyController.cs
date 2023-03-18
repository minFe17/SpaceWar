using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager; //맵 생성 후 받아와야함
    [SerializeField] DoorList _doorList;       //맵 생성 후 받아와야함
    [SerializeField] Player _player;           //맵 생성 후 받아와야함
    [SerializeField] Transform _target;        //맵 생성 후 받아와야함
    [SerializeField] float _spawnDelay;
    [SerializeField] int _minEnemy;
    [SerializeField] int _maxEnemy;

    public List<Enemy> _enemyList = new List<Enemy>();
    List<GameObject> _enemys = new List<GameObject>();

    BoxCollider _ground;

    Vector3 _basePos;
    Vector3 _size;

    int _wave;
    int _waveIndex;

    bool _isClear;

    void Awake()
    {
        _ground = GetComponent<BoxCollider>();
        _basePos = transform.position + _ground.center; // 박스 콜라이더 센터는 필요없을수도
        _wave = Random.Range(0, 2);
        _size = _ground.size;   //Init으로 옮기기
        AddEnemyList();
    }

    void AddEnemyList()
    {
        _enemys.Add(Resources.Load("Prefabs/Turret") as GameObject);
        _enemys.Add(Resources.Load("Prefabs/Scorpion") as GameObject);
        _enemys.Add(Resources.Load("Prefabs/DeliveryRobot") as GameObject);
    }

    public void Init()
    {
        // 콜라이더 사이즈
        // 게임매니저
        // 문리스트
        // 플레이어
        // 타겟
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
            _gameManager.Clear(_doorList);
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

        int ramdom = Random.Range(0, _enemys.Count);
        GameObject enemy = Instantiate(_enemys[ramdom], spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Init(_gameManager, _player, this, _target);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _gameManager.Battle(_doorList);
            _ground.enabled = false;
            StartCoroutine(SpawnEnemyRoutine());
        }
    }
}
