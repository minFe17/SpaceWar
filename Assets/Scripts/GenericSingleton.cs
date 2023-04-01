using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> where T : MonoBehaviour
{
    private static T _instance;
    public static T GetInstance()
    {
        if(_instance == null)
        {
            GameObject temp = new GameObject(typeof(T).Name);
            _instance = temp.AddComponent<T>();
            Object.DontDestroyOnLoad(temp);
        }
        return _instance;
    }
}