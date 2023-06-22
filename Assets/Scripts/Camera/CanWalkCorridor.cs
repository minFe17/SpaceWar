using UnityEngine;

public class CanWalkCorridor : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.gameObject.layer != LayerMask.NameToLayer("CanGoRoom"))
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("ClearRoom"))
                collision.gameObject.layer = LayerMask.NameToLayer("CanGoRoom");
        }
    }
}
