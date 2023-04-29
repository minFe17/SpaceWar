using UnityEngine;

public class ScavengerAttack : MonoBehaviour
{
    [SerializeField] Scavenger _boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (_boss.AttackType)
            {
                case EAttackType.RightSlice:
                    other.GetComponent<Player>().TakeDamage(_boss.Damage);
                    break;
                case EAttackType.BothHands:
                    other.GetComponent<Player>().TakeDamage(_boss.Damage * 2);
                    break;
            }
        }
    }
}
