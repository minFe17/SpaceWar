using UnityEngine;

public class ClearRoom : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground") && other.gameObject.layer != LayerMask.NameToLayer("CanWalkCorridor"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Corridor");
        }
    }
}
