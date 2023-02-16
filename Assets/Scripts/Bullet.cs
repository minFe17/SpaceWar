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
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        Invoke("Remove", _lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);

        if(collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);

        Remove();
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }

    public virtual void ABC()
    {
        Debug.Log("abc");
    }
}
