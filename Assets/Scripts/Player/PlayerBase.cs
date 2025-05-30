using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] GameObject _InfoKeyUI;
    [SerializeField] Text _InfoMseeage;
    [SerializeField] float _jumpPower;
    [SerializeField] float _idleTime;

    [SerializeField] protected EPlayerType _playerType;
    [SerializeField] protected Transform _idleBulletPos;
    [SerializeField] protected float _rotateSpeed;

    protected EBulletPoolType _bulletType;
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

    protected abstract void StartNormalAttack();
    protected abstract void StartFirstSkillAttack();
    protected abstract void StartSecondSkillAttack();

    protected virtual void CharacterUpdate() { }
    protected virtual void StopAiming() { }

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
        ShowOptionUI();
        Reload();
        ChangeShootMode();
        Attack();
    }

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

    protected virtual void Attack()
    {
        if (Input.GetButton("Fire") && !_isDie && !_isOpenOption)
        {
            switch (_playerData.ShootMode)
            {
                case EShootModeType.Normal:
                    StartNormalAttack();
                    break;
                case EShootModeType.FirstSkill:
                    StartFirstSkillAttack();
                    break;
                case EShootModeType.SecondSkill:
                    StartSecondSkillAttack();
                    break;
            }
        }
    }

    protected virtual bool CheckTurn()
    {
        if (!_isOpenOption && !_isDie)
            return true;
        return false;
    }

    protected virtual bool CheckOpenMap()
    {
        if (!_isDie && !_isOpenOption)
            return true;
        return false;
    }

    protected virtual void Die()
    {
        _isDie = true;
        _animator.SetTrigger("doDie");
    }

    // 애니메이션 이벤트 함수
    protected virtual void EndShoot()
    {
        _isShoot = false;
        if (_playerData.CurBullet <= 0)
            Reload();
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

    void Turn()
    {
        if (CheckTurn())
        {
            _mouseX += Input.GetAxis("Mouse X") * _rotateSpeed;

            Vector3 rotate = new Vector3(0, _mouseX, 0);
            transform.eulerAngles = rotate;
            _bulletPos.eulerAngles = rotate;
            FollowCam.m_XAxis.Value = rotate.y;
        }
    }

    // 애니메이션 이벤트 함수
    void ShootBullet()
    {
        if (_playerData.CurBullet <= 0)
            Reload();
        else
        {
            MakeBullet();
            _playerData.CurBullet--;
            _uiManager.IngameUI.ShowBullet();
        }
    }

    // 수정 필요
    protected virtual void MakeBullet()
    {
        GameObject bullet = _factoryManager.MakeObject<EBulletPoolType, GameObject>(_bulletType);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.rotation = _bulletPos.rotation;
    }

    // 애니메이션 이벤트 함수
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
                _playerData.ShootMode = EShootModeType.Normal;
            else if (Input.GetKeyDown(KeyCode.Alpha2) && _playerData.UnlockFirstSkill)
                _playerData.ShootMode = EShootModeType.FirstSkill;
            else if (Input.GetKeyDown(KeyCode.Alpha3) && _playerData.UnlockSecondSkill)
                _playerData.ShootMode = EShootModeType.SecondSkill;

            _uiManager.IngameUI.ShowShootMode();
        }
    }

    bool CheckShowOptionUI()
    {
        if (_uiManager.IsKeyInfoUI || _uiManager.IsSoundOption)
            return false;
        if (Input.GetKeyDown(KeyCode.Escape))
            return true;
        return false;
    }

    void OpenMap()
    {
        if (CheckOpenMap())
        {
            if (Input.GetKey(KeyCode.Tab))
                _uiManager.IngameUI.ShowMap();
            else if (Input.GetKeyUp(KeyCode.Tab))
                _uiManager.IngameUI.HideMap();
        }
        else
            _uiManager.IngameUI.HideMap();
    }

    void HPUpByMoney()
    {
        if (_playerData.HPUpByMoney && !_isDie)
        {
            _addMaxHP += (_playerData.Money / 50) - _addMaxHP;
            _playerData.MaxHp += _addMaxHP;
        }
    }

    void AddGem()
    {
        if (_gameManager.IsClear)
            DataSingleton<GemData>.Instance.AddGem(_playerData.Money * 10);
        else
            DataSingleton<GemData>.Instance.AddGem(_playerData.Money * 5);
    }

    protected void PlaySFX(ESFXAudioType type)
    {
        _audioManager.PlaySFX(type);
    }

    protected bool CheckAttack()
    {
        if (_playerData.CurBullet <= 0 || _isReload)
            return false;
        return true;
    }

    protected void Reload()
    {
        if (!_isDie && !_isReload)
        {
            int curBullet = _playerData.CurBullet;
            if (Input.GetKeyDown(KeyCode.R) || curBullet <= 0)
            {
                _isReload = true;
                _isShoot = false;
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

    public void ShowOptionUI()
    {
        if (CheckShowOptionUI())
        {
            StopAiming();

            _uiManager.OnOffOptionUI(!_isOpenOption);
            PlaySFX(ESFXAudioType.Button);
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

    // 애니메이션 이벤트에서도 호출함
    public void GameOver()
    {
        _uiManager.GameOverUI.gameObject.SetActive(true);
        _uiManager.GameOverUI.GameOver();
        Cursor.lockState = CursorLockMode.None;
        GenericSingleton<JsonManager>.Instance.DestroyDataFiles();
        GenericSingleton<DoorManager>.Instance.ClearDoors();
        AddGem();

        PlaySFX(ESFXAudioType.Die);

        if (EnemyController != null)
        {
            if (EnemyController.EnemyList.Count != 0)
            {
                for (int i = EnemyController.EnemyList.Count - 1; i >= 0; i--)
                    EnemyController.EnemyList[i].RemoveEnemy();
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
            GameOver();
        }
    }
}
