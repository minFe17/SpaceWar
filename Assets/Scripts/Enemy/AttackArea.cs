using UnityEngine;
using Utils;

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
        GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_enemy.Damage);
    }
}