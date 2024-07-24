using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class MapAssetManager : MonoBehaviour
{
    // ╫л╠шео
    AddressableManager _addressableManager;

    public Dictionary<EEventRoomType, GameObject> EventRooms { get; }

    public GameObject PlayerSpawnPos { get; set; }
    public GameObject EnemyController { get; set; }
    public GameObject DeadZone { get; set; }

    public Material FloorMaterial { get; set; }
    public GameObject HorizontalDoor { get; set; }
    public GameObject VerticalDoor { get; set; }
    public GameObject HorizontalWall { get; set; }
    public GameObject VerticalWall { get; set; }

    async void LoadEventRoomAsset()
    {
        if (EventRooms.Count != 0)
            return;

        for (int i = 0; i < (int)EEventRoomType.Max; i++)
        {
            GameObject eventRoom = await _addressableManager.GetAddressableAsset<GameObject>($"{(EEventRoomType)i}");
            EventRooms.Add((EEventRoomType)i, eventRoom);
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
        FloorMaterial = await _addressableManager.GetAddressableAsset<Material>($"{worldType}/FloorMaterial");
        HorizontalDoor = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/HorizontalDoor");
        VerticalDoor = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/VerticalDoor");
        HorizontalWall = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/HorizontalWall");
        VerticalWall = await _addressableManager.GetAddressableAsset<GameObject>($"{worldType}/VerticalWall");
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