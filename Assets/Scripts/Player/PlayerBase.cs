using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerBase : MonoBehaviour
{
    // 수정필요
    [SerializeField] GameObject _InfoKeyUI;
    [SerializeField] Text _InfoMseeage;
    [SerializeField] float _jumpPower;
    [SerializeField] float _idleTime;

    [SerializeField] protected EPlayerType _playerType;
    [SerializeField] protected Transform _idleBulletPos;
    [SerializeField] protected float _rotateSpeed;

    protected Animator _animator;
    protected Rigidbody _rigidbody;

    protected PlayerData _playerData;
    protected FactoryManager _factoryManager;
    protected UIManager _uiManager;
    protected GameManager _gameManager;
    protected AudioClipManager _audioManager;

    protected float _mouseX;

    protected bool _isShoot;
    protected bool _isReload;
    protected bool _isOpenOption;
    protected bool _isDie;
    protected Transform _bulletPos;

    int _addMaxHP;
    float _idleTimer;
    float _speed;
    bool _isJump;
    Vector3 _move;

    public CinemachineFreeLook FollowCam { get; set; }
    public EnemyController EnemyController { get; set; }
    public bool IsDie { get => _isDie; }


    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        SetManager();
        Init();
    }

    void FixedUpdate()
    {
        Move();
        Sprint();
    }

    void Update()
    {
        Jump();
        HPUpByMoney();
        CharacterUpdate();
        Turn();
        OpenMap();
    }

    protected virtual void CharacterUpdate() { }

    protected virtual void Init()
    {
        _mouseX = 0;
        _idleTimer = 0;

        _isShoot = false;
        _isJump = false;
        _isReload = false;
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

    // 확인 필요
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

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !_isJump && !_isDie)
        {
            _isJump = true;
            _animator.SetTrigger("doJump");
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    // virtual?
    void Turn()
    {
        if (/*!_isAiming &&*/ !_isOpenOption && !_isDie)
        {
            _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;

            Vector3 rotate = new Vector3(0, _mouseX, 0);
            transform.eulerAngles = rotate;
            _bulletPos.eulerAngles = rotate;
            FollowCam.m_XAxis.Value = rotate.y;
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

    // 확인 필요
    void MakeBullet()
    {
        GameObject bullet = _factoryManager.MakeObject<EPlayerPoolType, GameObject>(EPlayerPoolType.Bullet);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.rotation = _bulletPos.rotation;
    }

    void ReloadBullet()
    {
        _playerData.CurBullet = _playerData.MaxBullet;
        _isReload = false;
        _uiManager.IngameUI.ShowBullet();
    }

    void HPUpByMoney()
    {
        if (_playerData.HPUpByMoney && !_isDie)
        {
            _addMaxHP += (_playerData.Money / 50) - _addMaxHP;
            _playerData.MaxHp += _addMaxHP;
        }
    }

    protected virtual void OpenMap()
    {
        if (!_isDie && !_isOpenOption)
        {
            if (Input.GetKey(KeyCode.Tab))
                _uiManager.IngameUI.ShowMap();
            else if (Input.GetKeyUp(KeyCode.Tab))
                _uiManager.IngameUI.HideMap();
        }
        else
            _uiManager.IngameUI.HideMap();
    }

    protected virtual void Die()
    {
        _isDie = true;
        _animator.SetTrigger("doDie");
    }

    protected void Reload()
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

    public void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _playerData.CurHp -= damage;
        _uiManager.IngameUI.ShowHp();
        if (_playerData.CurHp <= 0)
            Die();
    }

    public void GetMoney(int money)
    {
        _playerData.Money += money;
        _uiManager.IngameUI.ShowMoney();
    }

    public void Vampirism()
    {
        if (!_isDie && _playerData.Vampirism)
        {
            int random = Random.Range(0, 3);
            _playerData.CurHp += random;
        }
    }

    // virtual?
    public void ShowOptionUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_uiManager.IsKeyInfoUI && !_uiManager.IsSoundOption)
        {
            //if (_isAiming == true)
            //    StopAimingEnemy();

            _uiManager.OnOffOptionUI(!_isOpenOption);
            _audioManager.PlaySFX(ESFXAudioType.Button);
            if (!_isOpenOption)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                OptionUIState();
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                OptionUIState();
            }
        }
    }

    public void OptionUIState()
    {
        _isOpenOption = !_isOpenOption;
        FollowCam.enabled = !FollowCam.enabled;
    }

    public void GameOver()
    {
        _uiManager.GameOverUI.gameObject.SetActive(true);
        _uiManager.GameOverUI.GameOver();
        Cursor.lockState = CursorLockMode.None;
        GenericSingleton<JsonManager>.Instance.DestroyDataFiles();
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
            //GameOver();
        }
    }
}
