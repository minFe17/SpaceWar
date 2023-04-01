using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    [SerializeField] int _money;
    [SerializeField] GameObject _effect;

    Animator _animator;

    bool _isEat;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void Move()
    {
        // 플레이어 근처면 따라가기? 
        // 코인 먹힌 애니메이션
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _isEat == false)
            PlayerGetCoin();
    }

    void PlayerGetCoin()
    {
        _isEat = true;
        GenericSingleton<EnemyManager>.GetInstance().GetPlayer().GetMoney(_money);
        _animator.SetBool("isGetCoin", true);
    }

    void OnEffect()
    {
        _effect.SetActive(true);
        Invoke("DestroyCoin()", 1f);
    }

    void DestroyCoin()
    {
        Destroy(_parent);
    }
}
