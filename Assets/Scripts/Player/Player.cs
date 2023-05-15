using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _bulletPos;
    [SerializeField] GameObject _zoomCamera;
    [SerializeField] GameObject _model;
    [SerializeField] GameObject _InfoKeyUI;
    [SerializeField] Text _InfoMseeage;

    [SerializeField] float _jumpPower;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _idleTime;
    [SerializeField] float _fireDelay;

    GameObject _bullet;

    Animator _animator;
    Rigidbody _rigidbody;
    Transform _idleBulletPos;

    Vector3 _move;

    float _mouseX;
    float _mouseY;
    float _idleTimer;
    float _speed;

    bool _isJump;
    bool _isAiming;
    bool _isShot;
    bool _isReload;
    bool _isOpenOption;
    bool _isDie;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        Cursor.lockState = CursorLockMode.Locked;
        GenericSingleton<UIManager>.Instance.CreateUI();
        GenericSingleton<UIManager>.Instance.Player = this;
        GenericSingleton<UIManager>.Instance.InfoKey = _InfoKeyUI;
        GenericSingleton<UIManager>.Instance.InfoMessage = _InfoMseeage;
        Init();
    }

    void Init()
    {
        _idleBulletPos = _bulletPos;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowShotMode();
        GenericSingleton<GameManager>.Instance.StageUI();
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
        ShowOptionUI();
    }

    public void Move()
    {
        if (_isDie)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _animator.SetFloat("AxisX", x);
        _animator.SetFloat("AxisZ", z);

        _move = new Vector3(x, 0, z).normalized * Time.deltaTime * GenericSingleton<PlayerDataManager>.Instance.MoveSpeed;

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
            _speed += Time.deltaTime * GenericSingleton<PlayerDataManager>.Instance.SplintSpeed;
            if (_speed > 1)
                _speed = 1;
            transform.Translate(_move * _speed);
        }
        else
        {
            _speed -= Time.deltaTime * GenericSingleton<PlayerDataManager>.Instance.SplintSpeed;
            if (_speed < 0)
                _speed = 0;
        }
        _animator.SetFloat("Speed", _speed);
    }

    public void Zoom()
    {
        if (!_isDie && !_isOpenOption)
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
        GenericSingleton<UIManager>.Instance.AimPoint.SetActive(true);
        _animator.SetBool("isZoom", true);
        _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;
        _mouseY += Input.GetAxis("Mouse Y") * _rotateSpeed;
        Rotate(_mouseY);
        transform.eulerAngles = new Vector3(0, _mouseX, 0);
    }

    public void StopAimingEnemy()
    {
        _isAiming = false;
        GenericSingleton<UIManager>.Instance.AimPoint.SetActive(false);
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
            switch (GenericSingleton<PlayerDataManager>.Instance.ShotMode)
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
        if (!_isDie && !_isReload)
        {
            int curBullet = GenericSingleton<PlayerDataManager>.Instance.CurBullet;
            if (Input.GetKeyDown(KeyCode.R) || curBullet <= 0)
            {
                _isReload = true;
                _animator.SetTrigger("doReload");
                Invoke("ReloadBullet", 2.5f);
            }
        }
    }

    void ReloadBullet()
    {
        GenericSingleton<PlayerDataManager>.Instance.CurBullet = GenericSingleton<PlayerDataManager>.Instance.MaxBullet;
        _isReload = false;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
    }

    public void ChangeShotMode()
    {
        if (!_isDie && !_isOpenOption)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                GenericSingleton<PlayerDataManager>.Instance.ShotMode = EShotModeType.Single;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                GenericSingleton<PlayerDataManager>.Instance.ShotMode = EShotModeType.Burst;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                GenericSingleton<PlayerDataManager>.Instance.ShotMode = EShotModeType.Auto;

            GenericSingleton<UIManager>.Instance.IngameUI.ShowShotMode();
        }

    }

    public void Turn()
    {
        if (!_isAiming && !_isOpenOption)
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
        GenericSingleton<PlayerDataManager>.Instance.Money += money;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney();
    }

    public void ShowOptionUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GenericSingleton<UIManager>.Instance.IsKeyInfoUI == false)
        {
            GenericSingleton<UIManager>.Instance.OnOffOptionUI();
            if (!_isOpenOption)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                _isOpenOption = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                _isOpenOption = false;
            }
        }
    }

    public void OptionUIState(bool isOpenOtion)
    {
        _isOpenOption = isOpenOtion;
    }

    public void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        GenericSingleton<PlayerDataManager>.Instance.CurHp -= damage;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowHp();

        if (GenericSingleton<PlayerDataManager>.Instance.CurHp <= 0)
        {
            _isDie = true;
            _animator.SetTrigger("doDie");
        }
    }

    void EndDie()
    {
        GenericSingleton<UIManager>.Instance.GameOverUI.gameObject.SetActive(true);
        GenericSingleton<UIManager>.Instance.GameOverUI.ShowMoney();
        GenericSingleton<GameManager>.Instance.GameOver();
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator SingleShotRoutine()
    {
        int curBullet = GenericSingleton<PlayerDataManager>.Instance.CurBullet;
        if (curBullet > 0 && !_isReload)
        {
            _isShot = true;
            _animator.SetTrigger("doSingleShot");
            yield return new WaitForSeconds(0.3f);
            MakeBullet();
            GenericSingleton<PlayerDataManager>.Instance.CurBullet--;
            GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
            _animator.SetBool("isShotIdle", true);
            yield return new WaitForSeconds(_fireDelay);
            _isShot = false;
        }

        if (GenericSingleton<PlayerDataManager>.Instance.CurBullet <= 0)
            Reload();
    }

    IEnumerator BurstShotRoutine()
    {
        int curBullet = GenericSingleton<PlayerDataManager>.Instance.CurBullet;
        if (curBullet > 0 && !_isReload)
        {
            _isShot = true;
            _animator.SetTrigger("doBurstShot");

            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 3; i++)
            {
                if (GenericSingleton<PlayerDataManager>.Instance.CurBullet <= 0)
                {
                    Reload();
                    break;
                }
                yield return new WaitForSeconds(0.1f);
                MakeBullet();
                GenericSingleton<PlayerDataManager>.Instance.CurBullet--;
                GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
            }
            _animator.SetBool("isShotIdle", true);
            yield return new WaitForSeconds(_fireDelay);
            _isShot = false;
        }

        if (GenericSingleton<PlayerDataManager>.Instance.CurBullet <= 0)
            Reload();
    }

    IEnumerator AutoShotRoutine()
    {
        int curBullet = GenericSingleton<PlayerDataManager>.Instance.CurBullet;
        if (curBullet > 0 && !_isReload)
        {
            _isShot = true;
            if (!_animator.GetCurrentAnimatorStateInfo(1).IsName("Shoot_Autoshot"))
                _animator.SetTrigger("doAutoShot");
            yield return new WaitForSeconds(0.2f);
            MakeBullet();
            GenericSingleton<PlayerDataManager>.Instance.CurBullet--;
            GenericSingleton<UIManager>.Instance.IngameUI.ShowBullet();
        }
        _isShot = false;

        if (GenericSingleton<PlayerDataManager>.Instance.CurBullet <= 0)
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
        if (collision.gameObject.CompareTag("Ground"))
            _isJump = false;
    }
}

public enum EShotModeType
{
    Single,
    Burst,
    Auto,
    Max
}
