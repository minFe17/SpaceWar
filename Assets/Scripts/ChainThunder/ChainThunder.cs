using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

// ThunderBall에게 넣기?
public class ChainThunder : MonoBehaviour
{
    [SerializeField] EnemyDetector _enemyDetector;
    [SerializeField] [Range(1, 10)] int _maxEnemyChain;
    [SerializeField] float _refreshTime = 0.01f;
    [SerializeField] float _delayBetweenEachChain = 0.01f;

    List<GameObject> _spawnedLineRenderer = new List<GameObject>();
    List<Enemy> _enemiesIsChain = new List<Enemy>();

    AddressableManager _addressableManager;
    GameObject _prefab;

    int _counter;
    bool _isChain;
    bool _isShot;

    async void Init()
    {
        // 이건 나중에 총알 관리하는 곳에서
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
        _prefab = await _addressableManager.GetAddressableAsset<GameObject>("ChainThunder");

        if (_enemyDetector.EnemiesInRange.Count > 0)
        {
            if (!_isChain)
                StartChain();
        }
        else
            StopChain();
    }

    void StartChain()
    {
        _isChain = true;

        if (_enemyDetector != null && _prefab != null)
        {
            if(!_isShot)
            {
                _isShot = true;
                NewLineRenderer(transform, _enemyDetector.GetClosestEnemy().transform);

                if (_maxEnemyChain > 1)
                    StartCoroutine(ChainReaction(_enemyDetector.GetClosestEnemy()));
            }
        }
    }

    void NewLineRenderer(Transform startPos, Transform endPos)
    {
        GameObject lineRenderer = Instantiate(_prefab);
        _spawnedLineRenderer.Add(lineRenderer);
        StartCoroutine(UpdateLineRenderer(lineRenderer, startPos, endPos));
    }

    void StopChain()
    {
        _isChain = false;
        _isShot= false;
        _counter = 1;

        for(int i=0; i<_spawnedLineRenderer.Count; i++)
        {
            Destroy(_spawnedLineRenderer[i]);
        }

        _spawnedLineRenderer.Clear();
    }

    IEnumerator UpdateLineRenderer(GameObject lineRenderer, Transform startPos, Transform endPos)
    {
        if(_isChain && _isShot)
        {
            lineRenderer.GetComponent<LineRendererController>().SetPosition(startPos, endPos);
            yield return new WaitForSeconds(_refreshTime);
            StartCoroutine(UpdateLineRenderer(lineRenderer, startPos, endPos));
        }
    }

    IEnumerator ChainReaction(Enemy closesetEnemy)
    {
        yield return new WaitForSeconds(_delayBetweenEachChain);

        if(_counter == _maxEnemyChain)
            yield return null;
        else
        {
            if(_isChain)
            {
                _counter++;
                _enemiesIsChain.Add(closesetEnemy);

                Enemy nextEnemy = closesetEnemy.GetComponent<EnemyDetector>().GetClosestEnemy();
                if(!_enemiesIsChain.Contains(nextEnemy))
                {
                    NewLineRenderer(closesetEnemy.transform, nextEnemy.transform);
                    StartCoroutine(ChainReaction(nextEnemy));
                }
            }
        }
    }
}