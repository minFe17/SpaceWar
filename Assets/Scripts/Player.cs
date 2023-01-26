using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _splintSpeed;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _idleTime;

    Animator _animator;
    public MainCamera _mainCamera;

    float mouseX;
    float mouseY;
    float idleTimer;

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
        //Splint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 splint = Vector3.forward * Time.deltaTime * _splintSpeed;
            transform.Translate(splint);
            _animator.SetBool("isRun", true);
            _animator.SetBool("isWalk", false);
        }
        //Walk
        else
        {
            _animator.SetBool("isRun", false);
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(x, 0, z).normalized * Time.deltaTime * _moveSpeed;
            if (move != Vector3.zero)
            {
                idleTimer = 0f;
                transform.Translate(move);
                _animator.SetBool("isWalk", true);

                if (x < 0)
                {
                    _animator.SetInteger("moveDirection", (int)MoveDirection.Left);
                }
                else if (x > 0)
                {
                    _animator.SetInteger("moveDirection", (int)MoveDirection.Right);
                }
                else if (z < 0)
                {
                    _animator.SetInteger("moveDirection", (int)MoveDirection.Back);
                }
                else
                {
                    _animator.SetInteger("moveDirection", (int)MoveDirection.Forward);
                }
            }
            else
            {
                _animator.SetInteger("moveDirection", (int)MoveDirection.Idle);
                if (idleTimer >= _idleTime)
                    _animator.SetBool("isWalk", false);
                else
                    idleTimer += Time.deltaTime;
            }
        }
    }

    public void Turn()
    {
        mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;
        mouseY += Input.GetAxis("Mouse Y") * _rotateSpeed;

        Vector3 rotate = new Vector3(0, mouseX, 0);
        transform.eulerAngles = rotate;
        Vector3 cameraRotate = new Vector3(-mouseY, mouseX, 0);
        _mainCamera.Rotate(cameraRotate);
    }
}

enum MoveDirection
{
    Idle,
    Forward,
    Left,
    Right,
    Back
}
