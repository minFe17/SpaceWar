using UnityEngine;

public class Soldier : PlayerBase
{
    [SerializeField] GameObject _zoomCamera;

    // playerBase¿¡¼­?
    [SerializeField] float _fireDelay;

    float _mouseY;
    bool _isAiming;

    protected override void Init()
    {
        _bulletType = EBulletPoolType.Bullet;
        base.Init();
        _mouseY = 0;
        _isAiming = false;
    }

    protected override void CharacterUpdate()
    {
        Zoom();
    }

    protected override void Attack()
    {
        base.Attack();
        if (Input.GetButtonUp("Fire"))
        {
            _animator.SetBool("isShootIdle", true);
            Invoke("ShootIdle", 0.5f);
        }
    }

    protected override void StartNormalAttack()
    {
        if (CheckAttack() && !_isShoot)
        {
            _isShoot = true;
            _animator.SetTrigger("doSingleShoot");
        }
    }

    protected override void StartFirstSkillAttack()
    {
        if (CheckAttack() && !_isShoot)
        {
            _isShoot = true;
            _animator.SetTrigger("doBurstShoot");
        }
    }

    protected override void StartSecondSkillAttack()
    {
        if (CheckAttack())
        {
            _isShoot = true;
            _animator.SetTrigger("doAutoShoot");
        }
    }

    protected override void MakeBullet()
    {
        base.MakeBullet();
        PlaySFX(ESFXAudioType.Shoot);
    }

    protected override bool CheckTurn()
    {
        if (_isAiming)
            return false;
        return base.CheckTurn();
    }

    protected override void StopAiming()
    {
        if (_isAiming)
            StopAimingEnemy();
    }

    protected override bool CheckOpenMap()
    {
        if (_isAiming)
            return false;
        return base.CheckOpenMap();
    }

    protected override void EndShoot()
    {
        base.EndShoot();
        _animator.SetBool("isShootIdle", true);
    }

    protected override void Die()
    {
        base.Die();
        if (_isAiming == true)
            StopAimingEnemy();
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

    void ShootIdle()
    {
        _animator.SetBool("isShootIdle", false);
    }

    void EndAutoShoot()
    {
        _isShoot = false;
        if (_playerData.CurBullet <= 0)
            Reload();
    }
}