using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] EPlayerPoolType _playerPoolType;
    [SerializeField] Transform _idleBulletPos;
    [SerializeField] GameObject _zoomCamera;
    [SerializeField] GameObject _InfoKeyUI;
    [SerializeField] Text _InfoMseeage;

    [SerializeField] float _jumpPower;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _idleTime;
    [SerializeField] float _fireDelay;

    PlayerData _playerData;
    FactoryManager _factoryManager;
    UIManager _uiManager;
    GameManager _gameManager;
    AudioClipManager _audioManager;

    Animator _animator;
    Rigidbody _rigidbody;
    Transform _bulletPos;

    Vector3 _move;

    int _addMaxHP;

    float _mouseX;
    float _mouseY;
    float _idleTimer;
    float _speed;

    bool _isJump;
    bool _isAiming;
    bool _isShoot;
    bool _isReload;
    bool _isOpenOption;
    bool _isDie;

    public EnemyController EnemyController { get; set; }
    public CinemachineFreeLook FollowCam { get; set; }
    public bool IsDie { get => _isDie; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        SetManager();
        Init();
    }

    public void Init()
    {
        _mouseX = 0;
        _mouseY = 0;
        _idleTimer = 0;

        _isAiming = false;
        _isShoot = false;
        _isOpenOption = false;
        _isDie = false;

        _bulletPos = _idleBulletPos;
        Cursor.lockState = CursorLockMode.Locked;
        SettingUI();
        ShowUI();
    }

    void SetManager()
    {
        GenericSingleton<PlayerDataManager>.Instance.Player = this;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _gameManager = GenericSingleton<GameManager>.Instance;
        _audioManager = GenericSingleton<AudioClipManager>.Instance;
        _playerData = DataSingleton<PlayerData>.Instance;
    }

    void ShowUI()
    {
        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowMoney();
        _uiManager.IngameUI.ShowBullet();
        _uiManager.IngameUI.ShowShootMode();
        _gameManager.StageUI();
    }

    void SettingUI()
    {
        _uiManager = GenericSingleton<UIManager>.Instance;
        _uiManager.CreateUI();
        _uiManager.Player = this;
        _uiManager.InfoKey = _InfoKeyUI;
        _uiManager.InfoMessage = _InfoMseeage;
    }

    private void FixedUpdate()
    {
        Move();
        Sprint();
    }

    void Update()
    {
        Turn();
        OpenMap();
        Jump();
        Zoom();
        Fire();
        ChangeShootMode();
        Reload();
        ShowOptionUI();
        HPUpByMoney();
    }

    void Move()
    {
        if (_isDie)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _animator.SetFloat("AxisX", x);
        _animator.SetFloat("AxisZ", z);

        _move = (transform.forward * z + transform.right * x).normalized * _playerData.MoveSpeed;
        _move.y = _rigidbody.velocity.y;
        if (_move.magnitude > 1f)
        {
            _idleTimer = 0f;
            _animator.SetBool("isMove", true);
            _rigidbody.velocity = _move;
        }
        else
            IdleTimer();
    }

    void IdleTimer()
    {
        if (_idleTimer >= _idleTime)
            _animator.SetBool("isMove", false);
        else
            _idleTimer += Time.deltaTime;
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed += Time.deltaTime * _playerData.SplintSpeed;
            if (_speed > 2)
                _speed = 2;
            _rigidbody.velocity = new Vector3(_move.x * _speed, _rigidbody.velocity.y, _move.z * _speed);
        }
        else
        {
            _speed -= Time.deltaTime * _playerData.SplintSpeed;
            if (_speed < 0)
                _speed = 0;
        }
        _animator.SetFloat("Speed", _speed);
    }

    void Zoom()
    {
        if (!_isDie && !_isOpenOption)
        {
            if (Input.GetMouseButton(1))
                AimingEnemy();

            if (Input.GetMouseButtonUp(1))
                StopAimingEnemy();
        }
    }

    void AimingEnemy()
    {
        _isAiming = true;
        _zoomCamera.SetActive(true);
        _bulletPos.position = _zoomCamera.transform.position;
        _uiManager.AimPoint.SetActive(true);
        _animator.SetBool("isZoom", true);
        _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;
        _mouseY += Input.GetAxis("Mouse Y") * _rotateSpeed;
        Rotate(_mouseY);
    }

    void StopAimingEnemy()
    {
        _isAiming = false;
        _zoomCamera.SetActive(false);
        _uiManager.AimPoint.SetActive(false);
        Invoke("EndZoom", 0.1f);
        _bulletPos = _idleBulletPos;
        _mouseY = 0;
        Rotate(_mouseY);
        FollowCam.m_XAxis.Value = transform.eulerAngles.y;
    }

    void EndZoom()
    {
        _animator.SetBool("isZoom", false);
    }

    void Rotate(float y)
    {
        Vector3 rotate = new Vector3(-y, _mouseX, 0);
        transform.eulerAngles = rotate;
        _zoomCamera.transform.eulerAngles = rotate;
        _bulletPos.transform.eulerAngles = rotate;
    }

    void Fire()
    {
        if (Input.GetButton("Fire") && !_isDie && !_isOpenOption)
        {
            switch (_playerData.ShootMode)
            {
                case EShootModeType.Single:
                    StartSingleShoot();
                    break;
                case EShootModeType.Burst:
                    StartBurstShoot();
                    break;
                case EShootModeType.Auto:
                    StartAutoShoot();
                    break;
                default:
                    break;
            }
        }
        if (Input.GetButtonUp("Fire"))
        {
            _animator.SetBool("isShootIdle", true);
            Invoke("ShootIdle", 0.5f);
        }
    }

    void ShootIdle()
    {
        _animator.SetBool("isShootIdle", false);
    }

    void Reload()
    {
        if (!_isDie && !_isReload)
        {
            int curBullet = _playerData.CurBullet;
            if (Input.GetKeyDown(KeyCode.R) || curBullet <= 0)
            {
                _isReload = true;
                _animator.SetTrigger("doReload");
            }
        }
    }

    void ReloadBullet()
    {
        _playerData.CurBullet = _playerData.MaxBullet;
        _isReload = false;
        _uiManager.IngameUI.ShowBullet();
    }

    void ChangeShootMode()
    {
        if (!_isDie && !_isOpenOption)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _playerData.ShootMode = EShootModeType.Single;
            else if (Input.GetKeyDown(KeyCode.Alpha2) && _playerData.UnlockBurstMode)
                _playerData.ShootMode = EShootModeType.Burst;
            else if (Input.GetKeyDown(KeyCode.Alpha3) && _playerData.UnlockAutoMode)
                _playerData.ShootMode = EShootModeType.Auto;

            _uiManager.IngameUI.ShowShootMode();
        }

    }

    void Turn()
    {
        if (!_isAiming && !_isOpenOption && !_isDie)
        {
            _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;

            Vector3 rotate = new Vector3(0, _mouseX, 0);
            transform.eulerAngles = rotate;
            _bulletPos.eulerAngles = rotate;
            FollowCam.m_XAxis.Value = rotate.y;
        }
    }

    void OpenMap()
    {
        if (!_isAiming && !_isDie && !_isOpenOption)
        {
            if (Input.GetKey(KeyCode.Tab))
                _uiManager.IngameUI.ShowMap();
            else if (Input.GetKeyUp(KeyCode.Tab))
                _uiManager.IngameUI.HideMap();
        }
        else
            _uiManager.IngameUI.HideMap();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !_isJump && !_isDie)
            StartCoroutine(JumpRoutine());
    }

    public void GetMoney(int money)
    {
        _playerData.Money += money;
        _uiManager.IngameUI.ShowMoney();
    }

    void MakeBullet()
    {
        GameObject bullet = _factoryManager.MakeObject<EPlayerPoolType, GameObject>(EPlayerPoolType.Bullet);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.rotation = _bulletPos.rotation;
    }

    void HPUpByMoney()
    {
        if (_playerData.HPUpByMoney && !_isDie)
        {
            _addMaxHP += (_playerData.Money / 50) - _addMaxHP;
            _playerData.MaxHp += _addMaxHP;
        }
    }

    public void Vampirism()
    {
        if (!_isDie && _playerData.Vampirism)
        {
            int random = Random.Range(0, 3);
            _playerData.CurHp += random;
        }
    }

    public void ShowOptionUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_uiManager.IsKeyInfoUI && !_uiManager.IsSoundOption)
        {
            if (_isAiming == true)
                StopAimingEnemy();

            _uiManager.OnOffOptionUI(!_isOpenOption);
            _audioManager.PlaySFX(ESFXAudioType.Button);
            if (!_isOpenOption)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                _isOpenOption = true;
                FollowCam.enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                _isOpenOption = false;
                FollowCam.enabled = true;
            }
        }
    }

    public void OptionUIState(bool isOpenOtion)
    {
        _isOpenOption = isOpenOtion;
        FollowCam.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _playerData.CurHp -= damage;
        _uiManager.IngameUI.ShowHp();

        if (_playerData.CurHp <= 0)
        {
            _isDie = true;
            _animator.SetTrigger("doDie");
            if (_isAiming == true)
                StopAimingEnemy();
        }
    }

    public void GameOver()
    {
        _uiManager.GameOverUI.gameObject.SetActive(true);
        _uiManager.GameOverUI.GameOver();
        Cursor.lockState = CursorLockMode.None;
        GenericSingleton<CsvManager>.Instance.DestroyDataFiles();
        _audioManager.PlaySFX(ESFXAudioType.Die);

        if (EnemyController != null)
        {
            if (EnemyController.EnemyList.Count != 0)
            {
                for (int i = EnemyController.EnemyList.Count - 1; i >= 0; i--)
                {
                    EnemyController.EnemyList[i].RemoveEnemy();
                }
            }
        }
        FollowCam.enabled = false;
    }

    void StartSingleShoot()
    {
        if (_playerData.CurBullet > 0 && !_isReload && !_isShoot)
        {
            _isShoot = true;
            _animator.SetTrigger("doSingleShoot");
        }
    }

    void ShootBullet()
    {
        if (_playerData.CurBullet <= 0)
            Reload();
        else
        {
            MakeBullet();
            _playerData.CurBullet--;
            _uiManager.IngameUI.ShowBullet();
            _audioManager.PlaySFX(ESFXAudioType.Shoot);
        }
    }

    void EndShoot()
    {
        _animator.SetBool("isShootIdle", true);
        if (_playerData.CurBullet <= 0)
            Reload();
        _isShoot = false;
    }

    void StartBurstShoot()
    {
        if (_playerData.CurBullet > 0 && !_isReload && !_isShoot)
        {
            _isShoot = true;
            _animator.SetTrigger("doBurstShoot");
        }
    }

    void StartAutoShoot()
    {
        if (_playerData.CurBullet > 0 && !_isReload)
        {
            _isShoot = true;
            _animator.SetTrigger("doAutoShoot");
        }
    }

    void EndAutoShoot()
    {
        _isShoot = false;
        if (_playerData.CurBullet <= 0)
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
        {
            _isJump = false;
            if (collision.gameObject.layer != LayerMask.NameToLayer("Room"))
                collision.gameObject.layer = LayerMask.NameToLayer("Room");
            if (collision.gameObject.GetComponentInChildren<EventRoom>())
            {
                if (collision.gameObject.GetComponent<ClearRoom>() == null)
                    collision.gameObject.AddComponent<ClearRoom>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            _isDie = true;
            GameOver();
        }
    }
}