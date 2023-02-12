using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;

    CapsuleCollider _collider;
    Rigidbody _rigidbody;

    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        Invoke("Remove", _lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
            Remove();
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Turret>().TakeDamage(_damage);
            Remove();
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
        }
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
