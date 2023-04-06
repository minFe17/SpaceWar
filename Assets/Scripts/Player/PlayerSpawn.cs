using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    GameObject _player;
    GameObject _mainCamera;
    GameObject _followCam;
    
    public void Spawn()
    {
        GameObject player = Resources.Load("Prefabs/Player") as GameObject;
        _player = Instantiate(player);
        _player.transform.position = transform.position;
        SpawnCamera();
        GenericSingleton<EnemyManager>.GetInstance().Init(_player);
    }

    public void SpawnCamera()
    {
        GameObject camera = Resources.Load("Prefabs/Main Camera") as GameObject;
        _mainCamera = Instantiate(camera);
        GameObject followCamera = Resources.Load("Prefabs/Follow Cam") as GameObject;
        _followCam = Instantiate(followCamera);
        _followCam.GetComponent<FollowCamera>().Init(_player.transform);
    }

    public Player GetPlayer()
    {
        return _player.GetComponent<Player>();
    }
    
    public void DestroyPlayer()
    {
        Destroy(_player);
        Destroy(_mainCamera);
        Destroy(_followCam);
    }
}
