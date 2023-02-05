using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _gun;

    void Start()
    {
        
    }

    void Update()
    {
        _gun.transform.LookAt(_target.position);
        //생성되면 몬스터 리스트에 추가
        //플레이어 바라보기(터렛만 움직이기)
        //바라보고 몇 초 뒤에 공격(코루틴)(공격 전 범위 알려주기)
        //총알 충돌
        //파괴(이펙트)
        //파괴이펙트 없어지면 터렛 없애기
        //몬스터 리스트에서 지우기
        //Enemy 상속받기? (만들고 상속할지 말지)

    }
}
