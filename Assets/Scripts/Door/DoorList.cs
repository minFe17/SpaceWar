using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorList : MonoBehaviour
{
    public List<BoxCollider> doors = new List<BoxCollider>();
    
    public void LockDoor()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].enabled = false;
            doors[i].gameObject.GetComponent<Door>().Lock();
        }
    }

    public void UnlockDoor()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].enabled = true;
        }
    }
}
