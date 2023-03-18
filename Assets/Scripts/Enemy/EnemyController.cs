using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager; //�� ���� �� �޾ƿ;���
    [SerializeField] DoorList _doorList;       //�� ���� �� �޾ƿ;���
    [SerializeField] Player _player;           //�� ���� �� �޾ƿ;���
    [SerializeField] Transform _target;        //�� ���� �� �޾ƿ;���
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
        _basePos = transform.position + _ground.center; // �ڽ� �ݶ��̴� ���ʹ� �ʿ��������
        _wave = Random.Range(0, 2);
        _size = _ground.size;   //Init���� �ű��
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
        // �ݶ��̴� ������
        // ���ӸŴ���
        // ������Ʈ
        // �÷��̾�
        // Ÿ��
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
