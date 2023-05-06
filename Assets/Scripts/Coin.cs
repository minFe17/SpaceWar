using UnityEngine;
using Utils;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    [SerializeField] int _money;
    [SerializeField] GameObject _effect;

    Animator _animator;
    Player _player;

    bool _isEat;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GenericSingleton<EnemyManager>.Instance.Target.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _isEat == false)
            PlayerGetCoin();
    }

    void PlayerGetCoin()
    {
        _isEat = true;
        _player.GetMoney(_money);
        _animator.SetBool("isGetCoin", true);
    }

    void OnEffect()
    {
        _effect.SetActive(true);
        Invoke("DestroyCoin", 1f);
    }

    void DestroyCoin()
    {
        Destroy(_parent);
    }
}
