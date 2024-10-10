using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class MapAssetManager : MonoBehaviour
{
    // �̱���
    AddressableManager _addressableManager;
    Dictionary<EEventRoomType, GameObject> _eventRoom = new Dictionary<EEventRoomType, GameObject>();

    public Dictionary<EEventRoomType, GameObject> EventRooms { get => _eventRoom; }

    public GameObject PlayerSpawnPos { get; private set; }
    public GameObject EnemyController { get; private set; }
    public GameObject DeadZone { get; private set; }

    public Material FloorMaterial { get; private set; }
    public GameObject HorizontalDoor { get; private set; }
    public GameObject VerticalDoor { get; private set; }
    public GameObject HorizontalWall { get; private set; }
    public GameObject VerticalWall { get; private set; }

    async void LoadEventRoomAsset()
    {

        if (_eventRoom.Count != 0)
            return;

        for (int i = 0; i < (int)EEventRoomType.Max; i++)
        {
            GameObject eventRoom = await _addressableManager.GetAddressableAsset<GameObject>($"{(EEventRoomType)i}");
            _eventRoom.Add((EEventRoomType)i, eventRoom);
        }
    }

    async void LoadPlayerSpawnPosAsset()
    {
        if (PlayerSpawnPos != null)
            return;
        PlayerSpawnPos = await _addressableManager.GetAddressableAsset<GameObject>("PlayerSpawnPos");
    }

    async void LoadEnemyControllerAsset()
    {
        if (EnemyController != null)
            return;
        EnemyController = await _addressableManager.GetAddressableAsset<GameObject>("EnemyController");
    }

    async void LoadDeadZoneAsset()
    {
        if (DeadZone != null)
            return;
        DeadZone = await _addressableManager.GetAddressableAsset<GameObject>("DeadZone");
    }

    async Task LoadMapAsset(EWorldType worldType)
    {
        FloorMaterial = await _addressableManager.GetAddressableAsset<Material>($"{worldType}/FloorMaterial.mat");
        HorizontalDoor = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/HorizontalDoor.prefab");
        VerticalDoor = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/VerticalDoor.prefab");
        HorizontalWall = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/HorizontalWall.prefab");
        VerticalWall = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/VerticalWall.prefab");
    }

    public async Task LoadAsset(EWorldType worldType)
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        LoadEventRoomAsset();
        LoadPlayerSpawnPosAsset();
        LoadEnemyControllerAsset();
        LoadDeadZoneAsset();

        await LoadMapAsset(worldType);
    }

    public void ReleaseAsset()
    {
        if (FloorMaterial == null)
            return;
        _addressableManager.Release(FloorMaterial);
        _addressableManager.Release(HorizontalDoor);
        _addressableManager.Release(VerticalDoor);
        _addressableManager.Release(HorizontalWall);
        _addressableManager.Release(VerticalWall);
    }
}