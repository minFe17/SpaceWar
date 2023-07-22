using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _idleBulletPos;
    [SerializeField] GameObject _zoomCamera;
    [SerializeField] GameObject _InfoKeyUI;
    [SerializeField] Text _InfoMseeage;

    [SerializeField] float _jumpPower;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _idleTime;
    [SerializeField] float _fireDelay;

    PlayerDataManager _playerDataManager;
    UIManager _uiManager;
    GameManager _gameManager;
    SoundController _soundController;

    GameObject _bullet;
    Animator _animator;
    Rigidbody _rigidbody;
    Transform _bulletPos;

    AudioClip _shotSound;
    AudioClip _dieSound;

    Vector3 _move;

    int _addMaxHP;

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

    public EnemyController EnemyController { get; set; }
    public CinemachineFreeLook FollowCam { get; set; }
    public bool IsDie { get => _isDie; }

    void Start()
    {
        _bulletPos = _idleBulletPos;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerDataManager = GenericSingleton<PlayerDataManager>.Instance;
        _gameManager = GenericSingleton<GameManager>.Instance;
        _soundController = GenericSingleton<SoundManager>.Instance.SoundController;
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        Cursor.lockState = CursorLockMode.Locked;
        _playerDataManager.Player = this;

        SettingUI();
        Init();
        SettingAudio();
    }

    void Init()
    {

        _uiManager.IngameUI.ShowHp();
        _uiManager.IngameUI.ShowMoney();
        _uiManager.IngameUI.ShowBullet();
        _uiManager.IngameUI.ShowShotMode();
        _gameManager.StageUI();
    }

    void SettingAudio()
    {
        _shotSound = Resources.Load("Prefabs/SoundClip/Shot") as AudioClip;
        _dieSound = Resources.Load("Prefabs/SoundClip/Die") as AudioClip;
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
        ChangeShotMode();
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

        _move = (transform.forward * z + transform.right * x).normalized * _playerDataManager.MoveSpeed;
        _move.y = _rigidbody.velocity.y;
        if (_move.magnitude > 1f)
        {
            _idleTimer = 0f;
            _animator.SetBool("isMove", true);
            _rigidbody.velocity = _move;
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

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed += Time.deltaTime * _playerDataManager.SplintSpeed;
            if (_speed > 2)
                _speed = 2;
            _rigidbody.velocity = new Vector3(_move.x * _speed, _rigidbody.velocity.y, _move.z * _speed);
        }
        else
        {
            _speed -= Time.deltaTime * _playerDataManager.SplintSpeed;
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
            {
                AimingEnemy();
            }
            if (Input.GetMouseButtonUp(1))
            {
                StopAimingEnemy();
            }
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
            switch (_playerDataManager.ShotMode)
            {
                case EShotModeType.Single:
                    StartSingleShot();
                    break;
                case EShotModeType.Burst:
                    StartBurstShot();
                    break;
                case EShotModeType.Auto:
                    StartAutoShot();
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

    void Reload()
    {
        if (!_isDie && !_isReload)
        {
            int curBullet = _playerDataManager.CurBullet;
            if (Input.GetKeyDown(KeyCode.R) || curBullet <= 0)
            {
                _isReload = true;
                _animator.SetTrigger("doReload");
            }
        }
    }

    void ReloadBullet()
    {
        _playerDataManager.CurBullet = _playerDataManager.MaxBullet;
        _isReload = false;
        _uiManager.IngameUI.ShowBullet();
    }

    void ChangeShotMode()
    {
        if (!_isDie && !_isOpenOption)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _playerDataManager.ShotMode = EShotModeType.Single;
            else if (Input.GetKeyDown(KeyCode.Alpha2) && _playerDataManager.UnlockBurstMode)
                _playerDataManager.ShotMode = EShotModeType.Burst;
            else if (Input.GetKeyDown(KeyCode.Alpha3) && _playerDataManager.UnlockAutoMode)
                _playerDataManager.ShotMode = EShotModeType.Auto;

            _uiManager.IngameUI.ShowShotMode();
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
        _playerDataManager.Money += money;
        _uiManager.IngameUI.ShowMoney();
    }

    void MakeBullet()
    {
        GameObject bullet = Instantiate(_bullet);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.rotation = _bulletPos.rotation;
    }

    void HPUpByMoney()
    {
        if (_playerDataManager.HPUpByMoney && !_isDie)
        {
            _addMaxHP += (_playerDataManager.Money / 50) - _addMaxHP;
            _playerDataManager.MaxHp += _addMaxHP;
        }
    }

    public void Vampirism()
    {
        if (!_isDie && _playerDataManager.Vampirism)
        {
            int random = Random.Range(0, 3);
            _playerDataManager.CurHp += random;
        }
    }

    public void ShowOptionUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_uiManager.IsKeyInfoUI && !_uiManager.IsSoundOption)
        {
            if (_isAiming == true)
                StopAimingEnemy();

            _uiManager.OnOffOptionUI(!_isOpenOption);
            AudioClip uiButtonSound = Resources.Load("Prefabs/SoundClip/UIButton") as AudioClip;
            GenericSingleton<SoundManager>.Instance.SoundController.PlaySFXAudio(uiButtonSound);
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

        _playerDataManager.CurHp -= damage;
        _uiManager.IngameUI.ShowHp();

        if (_playerDataManager.CurHp <= 0)
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
        _uiManager.GameOverUI.ShowWave();
        _uiManager.GameOverUI.ShowPlayTime();
        _uiManager.GameOverUI.ShowKillEnemy();
        _uiManager.GameOverUI.ShowMoney();
        Cursor.lockState = CursorLockMode.None;
        GenericSingleton<CsvController>.Instance.DestroyDataFile();
        _soundController.PlaySFXAudio(_dieSound);

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

    void StartSingleShot()
    {
        if (_playerDataManager.CurBullet > 0 && !_isReload && !_isShot)
        {
            _isShot = true;
            _animator.SetTrigger("doSingleShot");
        }
    }

    void ShotBullet()
    {
        if (_playerDataManager.CurBullet <= 0)
        {
            Reload();
        }
        else
        {
            MakeBullet();
            _playerDataManager.CurBullet--;
            _uiManager.IngameUI.ShowBullet();
            _soundController.PlaySFXAudio(_shotSound);
        }
    }

    void EndShot()
    {
        _animator.SetBool("isShotIdle", true);
        if (_playerDataManager.CurBullet <= 0)
            Reload();
        _isShot = false;
    }

    void StartBurstShot()
    {
        if (_playerDataManager.CurBullet > 0 && !_isReload && !_isShot)
        {
            _isShot = true;
            _animator.SetTrigger("doBurstShot");
        }
    }

    void StartAutoShot()
    {
        if (_playerDataManager.CurBullet > 0 && !_isReload)
        {
            _isShot = true;
            _animator.SetTrigger("doAutoShot");
        }
    }

    void EndAutoShot()
    {
        _isShot = false;
        if (_playerDataManager.CurBullet <= 0)
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

public enum EShotModeType
{
    Single,
    Burst,
    Auto,
    Max
}
