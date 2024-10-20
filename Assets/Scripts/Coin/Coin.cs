using UnityEngine;
using Utils;

public class Coin : MonoBehaviour
{
    [SerializeField] ECoinType _coinType;
    [SerializeField] GameObject _parent;
    [SerializeField] int _money;
    [SerializeField] GameObject _effect;

    Player _player;

    bool _isEat;

    void Start()
    {
        _player = GenericSingleton<EnemyManager>.Instance.Target.GetComponent<Player>();
    }

    public void Init()
    {
        _isEat = false;
        _effect.SetActive(false);
    }

    void PlayerGetCoin()
    {
        _isEat = true;
        _player.GetMoney(_money + GenericSingleton<PlayerDataManager>.Instance.BonusMoney);
        OnEffect();
    }

    void OnEffect()
    {
        _effect.SetActive(true);
        _effect.transform.position = transform.position;
        Invoke("DestroyCoin", 1f);
    }

    void DestroyCoin()
    {
        Destroy(_parent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _isEat == false)
            PlayerGetCoin();
    }
}