using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] protected MovableEnemy _enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            HitPlayer();
    }

    public virtual void HitPlayer()
    {
        _enemy.Player.TakeDamage(_enemy.Damage);
    }
}
