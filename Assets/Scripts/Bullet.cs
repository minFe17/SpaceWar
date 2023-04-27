using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _lifeTime;
    [SerializeField] protected float _speed;

    protected Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Invoke("Remove", _lifeTime);
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);

        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);

        Remove();
    }

}
