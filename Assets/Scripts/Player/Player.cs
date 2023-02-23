using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] int _maxAmmo;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _splintSpeed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _idleTime;
    [SerializeField] float _fireDelay;
    [SerializeField] Transform _bulletPos;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _zoomCamera;
    [SerializeField] GameObject _model;
    [SerializeField] GameObject _aimPoint;


    Animator _animator;
    Rigidbody _rigidbody;

    Transform _idleBulletPos;
    ShotMode _shotMode;

    float mouseX;
    float mouseY;
    float idleTimer;

    int _curHp;
    int _curAmmo;

    bool _isJump;
    bool _isAiming;
    bool _isShot;
    bool _isReload;
    bool _isDie;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _shotMode = ShotMode.Single;
        _curAmmo = _maxAmmo;
        _curHp = _maxHp;
        _idleBulletPos = _bulletPos;
    }

    void Update()
    {
        Move();
        Turn();
        Jump();
        Zoom();
        Fire();
        ChangeShotMode();
        Reload();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move()
    {
        if (_isDie)
            return;
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

    public void Zoom()
    {
        if(!_isDie)
        {
            if (Input.GetMouseButton(1))
            {
                _zoomCamera.SetActive(true);
                AimingEnemy();
            }
            if (Input.GetMouseButtonUp(1))
            {
                _zoomCamera.SetActive(false);
                StopAimingEnemy();
            }
        }
    }

    public void AimingEnemy()
    {
        _isAiming = true;
        _aimPoint.gameObject.SetActive(true);
        _animator.SetBool("isZoom", true);
        mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;
        mouseY += Input.GetAxis("Mouse Y") * _rotateSpeed;
        Rotate(mouseY);
        transform.eulerAngles = new Vector3(0, mouseX, 0);
        // 상체 움직일 방법?
        // 조준점 만들기(UI)
    }

    public void StopAimingEnemy()
    {
        _isAiming = false;
        _aimPoint.gameObject.SetActive(false);
        Invoke("EndZoom", 0.1f);
        _bulletPos = _idleBulletPos;
        mouseY = 0;
        Rotate(mouseY);
    }

    void EndZoom()
    {
        _animator.SetBool("isZoom", false);
    }

    public void Rotate(float y)
    {
        Vector3 rotate = new Vector3(-y, mouseX, 0);
        _model.transform.eulerAngles = rotate;
        _zoomCamera.transform.eulerAngles = rotate;
        _bulletPos.transform.eulerAngles = rotate;
    }

    public void Fire()
    {
        if (Input.GetButton("Fire") && !_isShot && !_isDie)
        {
            switch (_shotMode)
            {
                case ShotMode.Single:
                    StartCoroutine(SingleShotRoutine());
                    break;
                case ShotMode.Burst:
                    StartCoroutine(BurstShotRoutine());
                    break;
                case ShotMode.Auto:
                    StartCoroutine(AutoShotRoutine());
                    break;
                default:
                    break;
            }
        }
        if (Input.GetButtonUp("Fire"))
        {
            _animator.SetBool("isShotIdle", true);
            Invoke("ShotIdle", 0.5f);
        }
    }

    void ShotIdle()
    {
        _animator.SetBool("isShotIdle", false);
    }

    public void Reload()
    {
        if (!_isDie)
        {
            if (Input.GetKeyDown(KeyCode.R) || _curAmmo <= 0)
            {
                _isReload = true;
                _animator.SetTrigger("doReload");
                Invoke("ReloadAmmo", 2.5f);
            }
        }
    }

    void ReloadAmmo()
    {
        _curAmmo = _maxAmmo;
        _isReload = false;
    }

    public void ChangeShotMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _shotMode = ShotMode.Single;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _shotMode = ShotMode.Burst;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            _shotMode = ShotMode.Auto;
    }

    public void Turn()
    {
        if (!_isAiming)
        {
            mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;

            Vector3 rotate = new Vector3(0, mouseX, 0);
            transform.eulerAngles = rotate;
            _bulletPos.eulerAngles = rotate;
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && !_isJump && !_isDie)
            StartCoroutine(JumpRoutine());
    }

    public void MakeBullet()
    {
        GameObject bullet = Instantiate(_bullet);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.rotation = _bulletPos.rotation;
    }

    public void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;

        if (_curHp <= 0)
        {
            _isDie = true;
            _animator.SetTrigger("doDie");
        }
    }

    IEnumerator SingleShotRoutine()
    {
        if (_curAmmo > 0 && !_isReload)
        {
            _isShot = true;
            _animator.SetTrigger("doSingleShot");
            yield return new WaitForSeconds(0.3f);
            MakeBullet();
            _curAmmo--;
            _animator.SetBool("isShotIdle", true);
            yield return new WaitForSeconds(_fireDelay);
            _isShot = false;
        }

        if (_curAmmo <= 0)
            Reload();
    }

    IEnumerator BurstShotRoutine()
    {
        if (_curAmmo > 0 && !_isReload)
        {
            _isShot = true;
            _animator.SetTrigger("doBurstShot");

            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 3; i++)
            {
                if (_curAmmo <= 0)
                {
                    Reload();
                    break;
                }
                yield return new WaitForSeconds(0.1f);
                MakeBullet();
                _curAmmo--;
            }
            _animator.SetBool("isShotIdle", true);
            yield return new WaitForSeconds(_fireDelay);
            _isShot = false;
        }

        if (_curAmmo <= 0)
            Reload();
    }

    IEnumerator AutoShotRoutine()
    {
        if (_curAmmo > 0 && !_isReload)
        {
            _isShot = true;
            _animator.SetTrigger("doAutoShot");
            yield return new WaitForSeconds(0.2f);
            MakeBullet();
            _curAmmo--;
            yield return null;
            _isShot = false;
        }

        if (_curAmmo <= 0)
            Reload();
    }

    IEnumerator JumpRoutine()
    {
        _isJump = true;
        _animator.SetTrigger("doJump");
        yield return new WaitForSeconds(0.1f);
        _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            _isJump = false;
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

enum ShotMode
{
    Single,
    Burst,
    Auto
}
