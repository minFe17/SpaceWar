using UnityEngine;

public class Cactus : MonoBehaviour
{
    float _coolTime;

    void Update()
    {
        CheckCoolTime();
    }

    void CheckCoolTime()
    {
        if (_coolTime < 1f)
            _coolTime += Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _coolTime >= 1f)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            _coolTime = 0;
        }
    }
}
