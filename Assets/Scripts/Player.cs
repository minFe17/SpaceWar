using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotateSpeed;

    Animator _animator;
    public MainCamera _mainCamera;

    float mouseX;
    float mouseY;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Turn();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z).normalized * Time.deltaTime * _moveSpeed;
        if(move != Vector3.zero)
        {
            transform.Translate(move, Space.World);
            _animator.SetBool("isMove", true);
        }
        else
        {
            _animator.SetBool("isMove", false);
        }
    }

    public void Turn()
    {
        mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;
        mouseY += Input.GetAxis("Mouse Y") * _rotateSpeed;

        Vector3 rotate = new Vector3(-mouseY, mouseX, 0);
        transform.eulerAngles = rotate;
        _mainCamera.Rotate(rotate);

        //회전 제한 필요
    }
}
