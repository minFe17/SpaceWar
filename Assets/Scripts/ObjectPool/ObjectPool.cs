using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObjectPool<TEnum> : MonoBehaviour, IObjectPool where TEnum : Enum
{
    Dictionary<TEnum, Queue<GameObject>> _objectPool = new Dictionary<TEnum, Queue<GameObject>>();
    Queue<GameObject> _queue;

    TEnum[] _enumValue;

    Transform _parent;

    void IObjectPool.Init()
    {
        _enumValue = (TEnum[])Enum.GetValues(typeof(TEnum));

        for (int i = 0; i < _enumValue.Length; i++)
        {
            _objectPool.Add(_enumValue[i], new Queue<GameObject>());
        }
    }

    GameObject CreateObject(GameObject prefab)
    {
        return Instantiate(prefab);
    }

    GameObject IObjectPool.Push(Enum type, GameObject prefab)
    {
        _queue = null;
        GameObject returnObject = null;
        _objectPool.TryGetValue((TEnum)type, out _queue);
        if (_queue != null && _queue.Count > 0)
        {
            returnObject = _queue.Dequeue();
            returnObject.SetActive(true);
        }
        else
            returnObject = CreateObject(prefab);
        return returnObject;
    }

    void IObjectPool.Pull(Enum type, GameObject obj)
    {
        _queue = null;
        _objectPool.TryGetValue((TEnum)type, out _queue);
        obj.SetActive(false);
        _queue.Enqueue(obj);
        if (_parent == null)
            _parent = GenericSingleton<ObjectPoolManager>.Instance.transform;
        obj.transform.parent = _parent;
    }

    void IObjectPool.ClearChild()
    {
        if (_enumValue != null && _enumValue.Length > 0)
        {
            for (int i = 0; i < _enumValue.Length; i++)
            {
                while (_objectPool[_enumValue[i]].Count > 0)
                {
                    GameObject temp = _objectPool[_enumValue[i]].Dequeue();
                    Destroy(temp);
                }
            }
        }
    }

    void IObjectPool.Clear()
    {
        if (_enumValue != null && _enumValue.Length > 0)
        {
            for (int i = 0; i < _enumValue.Length; i++)
            {
                while (_objectPool[_enumValue[i]].Count > 0)
                {
                    GameObject temp = _objectPool[_enumValue[i]].Dequeue();
                    if (temp != null)
                        Destroy(temp);
                }
            }
            _objectPool.Clear();
        }
    }
}