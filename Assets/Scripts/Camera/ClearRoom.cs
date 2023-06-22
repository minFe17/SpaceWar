using UnityEngine;

public class ClearRoom : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.gameObject.layer != LayerMask.NameToLayer("CanWalkCorridor"))
        {
            collision.gameObject.layer = LayerMask.NameToLayer("CanWalkCorridor");
            collision.gameObject.AddComponent<CanWalkCorridor>();
        }
    }
}
