using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _lifeTime;

    CapsuleCollider _collider;
    protected Rigidbody _rigidbody;

    public float _speed;

    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Invoke("Remove", _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);

        if(other.gameObject.tag == "Player")
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);

        Remove();
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
