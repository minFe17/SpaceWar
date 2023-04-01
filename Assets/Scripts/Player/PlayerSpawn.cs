using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    GameObject _player;
    
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
        GameObject temp = Instantiate(camera);
        GameObject followCamera = Resources.Load("Prefabs/Follow Cam") as GameObject;
        temp = Instantiate(followCamera);
        temp.GetComponent<FollowCamera>().Init(_player.transform);
    }
}
