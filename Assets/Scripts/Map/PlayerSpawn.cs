using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void Spawn()
    {
        GameObject player = Resources.Load("Prefabs/Player") as GameObject;
        GameObject temp = Instantiate(player);
        temp.transform.position = transform.position;
        SpawnCamera(temp.transform);
    }

    public void SpawnCamera(Transform target)
    {
        GameObject camera = Resources.Load("Prefabs/Main Camera") as GameObject;
        GameObject temp = Instantiate(camera);
        GameObject followCamera = Resources.Load("Prefabs/Follow Cam") as GameObject;
        temp = Instantiate(followCamera);
        temp.GetComponent<FollowCamera>().Init(target);
    }
}
