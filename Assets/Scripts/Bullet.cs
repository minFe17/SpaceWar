using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        Invoke("Remove", _lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
            Remove();
        if(collision.gameObject.tag == "Enemy")
        {
            //������ �ֱ�
            Remove();
        }
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
