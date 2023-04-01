using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    Player _player;
    Transform _target;

    public void Init(GameObject player)
    {
        _player = player.GetComponent<Player>();
        _target = player.transform;
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public Transform GetTarget()
    {
        return _target;
    }
}
