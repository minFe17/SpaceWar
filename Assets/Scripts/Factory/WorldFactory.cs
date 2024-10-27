using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WorldFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> _worldFactorys = new List<GameObject>();

    WorldManager _worldManager;

    public void Init()
    {
        if (_worldManager == null)
            _worldManager = GenericSingleton<WorldManager>.Instance;

        ChangeWorldFactory();
    }

    public void ChangeWorldFactory()
    {
        for (int i = 0; i < _worldFactorys.Count; i++)
        {
            if (i == (int)_worldManager.WorldType)
                _worldFactorys[i].SetActive(true);
            else
                _worldFactorys[i].SetActive(false);
        }
    }
}