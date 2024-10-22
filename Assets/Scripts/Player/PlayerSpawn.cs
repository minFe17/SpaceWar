using Cinemachine;
using UnityEngine;
using Utils;

public class PlayerSpawn : MonoBehaviour
{
    Player _player;
    GameObject _mainCamera;
    GameObject _followCam;
    GameObject _miniMapCam;
    GameObject _mapCamera;

    FactoryManager _factoryManager;
    PlayerAssetManager _playerAssetManager;
    CameraAssetManager _cameraAssetManager;

    public void Spawn()
    {
        if (_factoryManager == null)
            _factoryManager = GenericSingleton<FactoryManager>.Instance;
       
        _player = _factoryManager.MakeObject<EPlayerPoolType, Player>(EPlayerPoolType.Player);

        _player.transform.position = transform.position;
        SpawnCamera();
        GenericSingleton<EnemyManager>.Instance.Target = _player.transform;
        _player.FollowCam = _followCam.GetComponent<CinemachineFreeLook>();
    }

    public void SpawnCamera()
    {
        if (_cameraAssetManager == null)
            _cameraAssetManager = GenericSingleton<CameraAssetManager>.Instance;

        GameObject camera = _cameraAssetManager.MainCamera;
        _mainCamera = Instantiate(camera);

        GameObject followCamera = _cameraAssetManager.FollowCamera;
        _followCam = Instantiate(followCamera);
        _followCam.GetComponent<FollowCamera>().Init(_player.transform);

        GameObject miniMapCam = _cameraAssetManager.MiniMapCamera;
        _miniMapCam = Instantiate(miniMapCam);
        _miniMapCam.GetComponent<MiniMapCam>().Init(_player.transform);

        GameObject mapCamera = _cameraAssetManager.MapCamera;
        _mapCamera = Instantiate(mapCamera);
    }

    public void DestroyPlayer()
    {
        Destroy(_player);
        Destroy(_mainCamera);
        Destroy(_followCam);
        Destroy(_miniMapCam);
        Destroy(_mapCamera);
    }
}