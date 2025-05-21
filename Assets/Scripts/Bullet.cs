using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] float _lifeTime;
    [SerializeField] protected float _speed;

    protected Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Init();
    }

    public void Init()
    {
        Invoke("Remove", _lifeTime);
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    protected virtual void Remove()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().TakeDamage(DataSingleton<PlayerData>.Instance.BulletDamage);

        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);
        Remove();
    }
}