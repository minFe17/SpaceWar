using UnityEngine;

public class Breath : MonoBehaviour
{
    [SerializeField] float _breathTime;

    PlayerBase _player;
    Dragon _base;
    float _coolTime;
    float _curBreathTime;

    public void Init(PlayerBase player, Dragon dragon)
    {
        _player = player;
        _base = dragon;
        _coolTime = 1;
    }

    void Update()
    {
        CheckCoolTime();
    }

    void CheckCoolTime()
    {
        if (_coolTime < 1f)
            _coolTime += Time.deltaTime;
        if (_curBreathTime < _breathTime)
            _curBreathTime += Time.deltaTime;
        else
        {
            _base.EndBreath();
            _curBreathTime -= _breathTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _coolTime >= 1f)
        {
            _player.TakeDamage(2);
            _coolTime = 0;
        }
    }
}