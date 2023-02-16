using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Enemy
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        LookTarget();
    }

}
