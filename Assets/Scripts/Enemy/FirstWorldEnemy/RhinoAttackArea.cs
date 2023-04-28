using UnityEngine;
using Utils;

public class RhinoAttackArea : MonoBehaviour
{
    [SerializeField] Rhino _boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _boss.Player.TakeDamage(_boss.Damage);
    }
}
