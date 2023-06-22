using UnityEngine;
using Utils;

public class Fire : MonoBehaviour
{
    ParticleSystem _fire;
    Player _player;

    float _coolTime = 1;

    void Start()
    {
        _fire = GetComponent<ParticleSystem>();
        _player = GenericSingleton<PlayerDataManager>.Instance.Player;
        _fire.trigger.AddCollider(_player.transform);
    }

    void Update()
    {
        if (_coolTime < 1f)
            _coolTime += Time.deltaTime;
    }

    private void OnParticleTrigger()
    {
        if (_coolTime >= 1f)
        {
            _player.TakeDamage(1);
            _coolTime = 0f;
        }
    }
}
