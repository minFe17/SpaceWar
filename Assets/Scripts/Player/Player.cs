using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _bulletPos;
    [SerializeField] GameObject _zoomCamera;
    [SerializeField] GameObject _model;
    [SerializeField] GameObject _aimPoint;
    [SerializeField] UIManager _uiManager;

    [SerializeField] int _maxHp;
    [SerializeField] int _maxAmmo;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _splintSpeed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _idleTime;
    [SerializeField] float _fireDelay;

    GameObject _bullet;

    Animator _animator;
    Rigidbody _rigidbody;
    Transform _idleBulletPos;
    
    EShotModeType _shotMode;

    Vector3 _move;

    float _mouseX;
    float _mouseY;
    float _idleTimer;
    float _speed;

    int _curHp;
    int _curAmmo;
    int _money;

    bool _isJump;
    bool _isAiming;
    bool _isShot;
    bool _isReload;
    bool _isDie;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _money = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Init();
    }

    void Init()
    {
        _shotMode = EShotModeType.Single;
        _idleBulletPos = _bulletPos;

        _curAmmo = _maxAmmo;
        _curHp = _maxHp;

        _uiManager.ShowHp(_curHp, _maxHp);
        _uiManager.ShowMoney(_money);
        _uiManager.ShowAmmo(_curAmmo, _maxAmmo);
        _uiManager.ShowShotMode(_shotMode);
    }

    void Update()
    {
        Move();
        Sprint();
        Turn();
        Jump();
        Zoom();
        Fire();
        ChangeShotMode();
        Reload();
    }

    public void Move()
    {
        if (_isDie)
            return;

        //Walk
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        _animator.SetFloat("AxisX", x);
        _animator.SetFloat("AxisZ", z);

        _move = new Vector3(x, 0, z).normalized * Time.deltaTime * _moveSpeed;

        if (_move != Vector3.zero)
        {
            _idleTimer = 0f;
            _animator.SetBool("isMove", true);
            transform.Translate(_move);
        }
        else
        {
            IdleTimer();
        }
    }

    void IdleTimer()
    {
        if (_idleTimer >= _idleTime)
            _animator.SetBool("isMove", false);
        else
            _idleTimer += Time.deltaTime;
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed += Time.deltaTime * _splintSpeed;
            if (_speed > 1)
                _speed = 1;
            transform.Translate(_move * _speed);
        }
        else
        {
            _speed -= Time.deltaTime * _splintSpeed;
            if (_speed < 0)
                _speed = 0;
        }
        _animator.SetFloat("Speed", _speed);
    }
        
    public void Zoom()
    {
        if (!_isDie)
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
        _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;
        _mouseY += Input.GetAxis("Mouse Y") * _rotateSpeed;
        Rotate(_mouseY);
        transform.eulerAngles = new Vector3(0, _mouseX, 0);
        // ?????? ????????? ???????
        // ????????? ?????????(UI)
    }

    public void StopAimingEnemy()
    {
        _isAiming = false;
        _aimPoint.gameObject.SetActive(false);
        Invoke("EndZoom", 0.1f);
        _bulletPos = _idleBulletPos;
        _mouseY = 0;
        Rotate(_mouseY);
    }

    void EndZoom()
    {
        _animator.SetBool("isZoom", false);
    }

    public void Rotate(float y)
    {
        Vector3 rotate = new Vector3(-y, _mouseX, 0);
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
                case EShotModeType.Single:
                    StartCoroutine(SingleShotRoutine());
                    break;
                case EShotModeType.Burst:
                    StartCoroutine(BurstShotRoutine());
                    break;
                case EShotModeType.Auto:
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
        _uiManager.ShowAmmo(_curAmmo, _maxAmmo);
    }

    public void ChangeShotMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _shotMode = EShotModeType.Single;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _shotMode = EShotModeType.Burst;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            _shotMode = EShotModeType.Auto;

        _uiManager.ShowShotMode(_shotMode);
    }

    public void Turn()
    {
        if (!_isAiming)
        {
            _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;

            Vector3 rotate = new Vector3(0, _mouseX, 0);
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

    public void GetMoney(int money)
    {
        _money += money;
        _uiManager.ShowMoney(_money);
    }

    public void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;
        _uiManager.ShowHp(_curHp, _maxHp);

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
            _uiManager.ShowAmmo(_curAmmo, _maxAmmo);
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
                _uiManager.ShowAmmo(_curAmmo, _maxAmmo);
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
            _uiManager.ShowAmmo(_curAmmo, _maxAmmo);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isJump = false;
    }
}

public enum EShotModeType
{
    Single,
    Burst,
    Auto
}
