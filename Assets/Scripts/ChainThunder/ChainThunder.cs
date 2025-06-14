using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ChainThunder : MonoBehaviour
{
    // �̱���
    List<GameObject> _spawnedLineRenderer = new List<GameObject>();
    List<Enemy> _enemiesIsChain = new List<Enemy>();

    EnemyDetector _enemyDetector;
    BulletAssetManager _bulletAssetManager;
    FactoryManager _factoryManager;
    ObjectPoolManager _poolManager;

    int _maxEnemyChain = 5;
    int _counter;
    float _refreshTime = 0.01f;
    float _delayBetweenEachChain = 0.01f;
    bool _isChain;
    bool _isShot;

    public void Init(EnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
        SetManager();
        if (_enemyDetector == null)
            return;
        if (_enemyDetector.EnemiesInRange.Count > 0)
        {
            if (!_isChain)
                StartChain();
        }
        else
            StopChain();
        Invoke("StopChain", 0.5f);
    }

    void SetManager()
    {
        if (_bulletAssetManager == null)
            _bulletAssetManager = GenericSingleton<BulletAssetManager>.Instance;
        if (_factoryManager == null)
            _factoryManager = GenericSingleton<FactoryManager>.Instance;
        if (_poolManager == null)
            _poolManager = GenericSingleton<ObjectPoolManager>.Instance;
    }

    void StartChain()
    {
        _isChain = true;

        if (_enemyDetector != null && _bulletAssetManager.ChainThunder != null)
        {
            if (!_isShot)
            {
                Enemy nextEnemy = _enemyDetector.GetClosestEnemy();
                _isShot = true;
                if (nextEnemy != null)
                {
                    NewLineRenderer(_enemyDetector.transform, nextEnemy.transform);

                    if (_maxEnemyChain > 1)
                        StartCoroutine(ChainReaction(_enemyDetector.GetClosestEnemy()));
                }
                else
                {
                    Enemy enemy = _enemyDetector.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(DataSingleton<PlayerData>.Instance.BulletDamage);
                }
            }
        }
    }

    void NewLineRenderer(Transform startPos, Transform endPos)
    {
        GameObject lineRenderer = _factoryManager.MakeObject<EBulletPoolType, GameObject>(EBulletPoolType.ChainThunder);
        _spawnedLineRenderer.Add(lineRenderer);
        StartCoroutine(UpdateLineRenderer(lineRenderer, startPos, endPos));
    }

    void StopChain()
    {
        _isChain = false;
        _isShot = false;
        _counter = 1;

        for (int i = 0; i < _spawnedLineRenderer.Count; i++)
            _poolManager.Pull(EBulletPoolType.ChainThunder, _spawnedLineRenderer[i]);

        for (int i = 0; i < _enemiesIsChain.Count; i++)
        {
            if (_enemiesIsChain[i] != null)
                _enemiesIsChain[i].IsChainHit = false;
        }

        _spawnedLineRenderer.Clear();
        _enemiesIsChain.Clear();
    }

    IEnumerator UpdateLineRenderer(GameObject lineRenderer, Transform startPos, Transform endPos)
    {
        if (_isChain && _isShot)
        {
            lineRenderer.GetComponent<LineRendererController>().SetPosition(startPos, endPos);
            yield return new WaitForSeconds(_refreshTime);
            StartCoroutine(UpdateLineRenderer(lineRenderer, startPos, endPos));
        }
    }

    IEnumerator ChainReaction(Enemy closesetEnemy)
    {
        if (!closesetEnemy.IsChainHit)
        {
            closesetEnemy.TakeDamage(DataSingleton<PlayerData>.Instance.BulletDamage);
            closesetEnemy.IsChainHit = true;
        }

        yield return new WaitForSeconds(_delayBetweenEachChain);

        if (_counter == _maxEnemyChain)
            yield return null;
        else
        {
            if (_isChain)
            {
                _counter++;
                _enemiesIsChain.Add(closesetEnemy);

                Enemy nextEnemy = closesetEnemy.GetComponent<EnemyDetector>().GetClosestEnemy();
                if (!_enemiesIsChain.Contains(nextEnemy))
                {
                    if (nextEnemy != null)
                    {
                        NewLineRenderer(closesetEnemy.transform, nextEnemy.transform);
                        StartCoroutine(ChainReaction(nextEnemy));
                    }
                }
            }
        }
    }
}