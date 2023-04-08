using System.Collections.Generic;
using UnityEngine;

public class DoorList : MonoBehaviour
{
    public List<Door> _doors = new List<Door>();

    public void LockDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].Lock();
        }
    }

    public void UnlockDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].enabled = true;
        }
    }
}
