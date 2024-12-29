using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // ╫л╠шео

    List<Door> _doors = new List<Door>();
    public List<Door> Doors { get => _doors; }

    public void LockDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
            _doors[i].Lock();
    }

    public void UnlockDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
            _doors[i].Unlock();
    }

    public void ClearDoors()
    {
        _doors.Clear();
    }
}