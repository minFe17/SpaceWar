using UnityEngine;

public class Soldier : PlayerBase
{
    [SerializeField] GameObject _zoomCamera;

    // playerBase에서?
    [SerializeField] float _fireDelay;

    float _mouseY;
    bool _isAiming;

    protected override void Init()
    {
        base.Init();
        _mouseY = 0;
        _isAiming = false;
    }

    protected override void CharacterUpdate()
    {
        Zoom();
        Fire();
        ChangeShootMode();
        Reload();
        ShowOptionUI();
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

    // playerBase에서?
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

    // playerBase에서?
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

    void StartSingleShoot()
    {
        if (_playerData.CurBullet > 0 && !_isReload && !_isShoot)
        {
            _isShoot = true;
            _animator.SetTrigger("doSingleShoot");
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

    protected override void OpenMap()
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

    protected override void Die()
    {
        base.Die();
        if (_isAiming == true)
            StopAimingEnemy();
    }
}