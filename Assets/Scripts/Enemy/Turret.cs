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
        //�����Ǹ� ���� ����Ʈ�� �߰�
        //�÷��̾� �ٶ󺸱�(�ͷ��� �����̱�)
        //�ٶ󺸰� �� �� �ڿ� ����(�ڷ�ƾ)(���� �� ���� �˷��ֱ�)
        //�Ѿ� �浹
        //�ı�(����Ʈ)
        //�ı�����Ʈ �������� �ͷ� ���ֱ�
        //���� ����Ʈ���� �����
        //Enemy ��ӹޱ�? (����� ������� ����)

    }
}
