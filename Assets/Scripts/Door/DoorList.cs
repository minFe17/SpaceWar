using System.Collections.Generic;
using UnityEngine;

public class DoorList : MonoBehaviour
{
    public List<BoxCollider> _doors = new List<BoxCollider>();

    public void LockDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].enabled = false;
            _doors[i].gameObject.GetComponent<Door>().Lock();
        }
    }

    public void UnlockDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].enabled = true;
        }
    }

    public void AddDoor(GameObject door)
    {
        if (door.GetComponent<BoxCollider>() != null)
            _doors.Add(door.GetComponent<BoxCollider>());
    }
}
