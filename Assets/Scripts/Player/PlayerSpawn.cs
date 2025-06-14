using Cinemachine;
using UnityEngine;
using Utils;

public class PlayerSpawn : MonoBehaviour
{
    PlayerBase _player;
    GameObject _mainCamera;
    GameObject _followCam;
    GameObject _miniMapCam;
    GameObject _mapCamera;

    FactoryManager _factoryManager;
    ObjectPoolManager _objectPoolManager;

    public void Spawn()
    {
        if (_factoryManager == null)
            _factoryManager = GenericSingleton<FactoryManager>.Instance;

        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;
        GameObject temp = _factoryManager.MakeObject<EPlayerType, GameObject>(playerType);
        _player = temp.GetComponent<PlayerBase>();

        _player.transform.position = transform.position;
        SpawnCamera();
        GenericSingleton<EnemyManager>.Instance.Target = _player.transform;
        _player.FollowCam = _followCam.GetComponent<CinemachineFreeLook>();
    }

    public void SpawnCamera()
    {
        _mainCamera = _factoryManager.MakeObject<ECameraType, GameObject>(ECameraType.MainCamera);

        _followCam = _factoryManager.MakeObject<ECameraType, GameObject>(ECameraType.FollowCamera);
        _followCam.GetComponent<FollowCamera>().Init(_player.transform);

        _miniMapCam = _factoryManager.MakeObject<ECameraType, GameObject>(ECameraType.MiniMapCamera);
        _miniMapCam.GetComponent<MiniMapCam>().Init(_player.transform);

        _mapCamera = _factoryManager.MakeObject<ECameraType, GameObject>(ECameraType.MapCamera);
    }

    public void DestroyPlayer()
    {
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;

        _objectPoolManager.Pull(playerType, _player.gameObject);
        _objectPoolManager.Pull(ECameraType.MainCamera, _mainCamera);
        _objectPoolManager.Pull(ECameraType.FollowCamera, _followCam);
        _objectPoolManager.Pull(ECameraType.MiniMapCamera, _miniMapCam);
        _objectPoolManager.Pull(ECameraType.MapCamera, _mapCamera);
    }
}