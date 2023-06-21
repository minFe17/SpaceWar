using Cinemachine;
using UnityEngine;
using Utils;

public class PlayerSpawn : MonoBehaviour
{
    GameObject _player;
    GameObject _mainCamera;
    GameObject _followCam;
    GameObject _miniMapCam;
    
    public void Spawn()
    {
        GameObject player = Resources.Load("Prefabs/Player") as GameObject;
        _player = Instantiate(player);
        _player.transform.position = transform.position;
        SpawnCamera();
        GenericSingleton<EnemyManager>.Instance.Target = _player.transform;
        _player.GetComponent<Player>().FollowCam = _followCam.GetComponent<CinemachineFreeLook>();
    }

    public void SpawnCamera()
    {
        GameObject camera = Resources.Load("Prefabs/Camera/Main Camera") as GameObject;
        _mainCamera = Instantiate(camera);
        GameObject followCamera = Resources.Load("Prefabs/Camera/Follow Cam") as GameObject;
        _followCam = Instantiate(followCamera);
        _followCam.GetComponent<FollowCamera>().Init(_player.transform);
        GameObject miniMapCam = Resources.Load("Prefabs/Camera/MiniMapCam") as GameObject;
        _miniMapCam = Instantiate(miniMapCam);
        _miniMapCam.GetComponent<MiniMapCam>().Init(_player.transform);
    }
    
    public void DestroyPlayer()
    {
        Destroy(_player);
        Destroy(_mainCamera);
        Destroy(_followCam);
    }
}
