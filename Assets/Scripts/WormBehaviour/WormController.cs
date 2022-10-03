using UnityEngine;

public class WormController : MonoBehaviour
{
    public int playerNumber;
    public Vector2 inputVector;
    public Stats stats;

    CharacterController _characterController;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _startingHp;
    [SerializeField] private int _minimumYLevel= 20;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityAccelleration = 0.01f;
    [SerializeField] private float _weaponTiltSpeed;
    [SerializeField][Range(0f, 1f)] private float _frictionIntensity;
    [SerializeField] private float _verticalKnockbackModifier;
    [SerializeField] private int _healthPackRestoreAmount;

    private Bazooka _bazooka;
    private OtherWeapon _otherWeapon;
    private int _equippedWeaponIndex;

    private FaceSwapper _faceSwapper;
    private PlayerAnimations _animations;
    private Player _controllingPlayer;
    private HpBar _hpBar;
    private Inventory _inventory;

    private Vector3 _weaponTilt = Vector3.zero;
    private float _slowMoveSpeed;
    private float _slowRotationSpeed;

    private float _fastMoveSpeed;
    private float _fastRotationSpeed;

    private float _xSpeed;
    private float _ySpeed;
    private float _zSpeed;

    private bool _didAction;
    private bool _hasAmmo = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _faceSwapper = GetComponentInChildren<FaceSwapper>();
        _animations = GetComponentInChildren<PlayerAnimations>();
        _bazooka = GetComponentInChildren<Bazooka>();
        _otherWeapon = GetComponentInChildren<OtherWeapon>();
        _hpBar = GetComponentInChildren<HpBar>();

        _inventory = new Inventory(_bazooka.gameObject, _otherWeapon.gameObject);
        stats = new Stats(_startingHp);

        _slowMoveSpeed = _moveSpeed * 0f;
        _slowRotationSpeed = _rotationSpeed * 0.25f;
        _fastMoveSpeed = _moveSpeed;
        _fastRotationSpeed = _rotationSpeed;

        _faceSwapper.SetNeutralFace();
        _animations.Init(this);

        TurnsManager.OnTurnEnd += Deactivate;
    }

    public void Init(Player controllingPlayer)
    {
        _controllingPlayer = controllingPlayer;
    }

    void Update()
    {
        int hp = stats.GetHp();
        if(hp <= 0)
        {
            InitializeDying();
        }
        else if(hp < 3)
        {
            _faceSwapper.SetConcernedFace();
        }
        if (transform.position.y < _minimumYLevel)
        {
            Die();
        }
    }

    public void FixedUpdate()
    {
        ApplyGravity();
        ApplyFriction();
        ApplyWeaponTilt();
        Move();
    }

    public void InitializeDying()
    {
        _faceSwapper.SetDeadFace();
        _animations.PlayDeathAnimation();
    }

    public void Die()
    {
        if (TurnsManager.GetInstance().GetActiveWorm() == this.gameObject)
        {
            TurnsManager.GetInstance().UpdateTurn();
        }
        _controllingPlayer.RemoveDeadWorm(this.gameObject);
        Destroy(gameObject);
    }



    private void ApplyGravity()
    { 
        if (!_characterController.isGrounded)
        {
            _ySpeed -= _gravityAccelleration;
        }
    }

    private void ApplyFriction()
    {
        if (_characterController.isGrounded)
        {
            _xSpeed = _xSpeed - _xSpeed * _frictionIntensity;
            _zSpeed = _zSpeed - _zSpeed * _frictionIntensity;
        }
    }


    private void Move()
    {
        transform.Rotate(0, inputVector.x * _rotationSpeed, 0);
        Vector3 moveVector = transform.rotation * new Vector3(0, _ySpeed, inputVector.y * _moveSpeed);
        moveVector.x += _xSpeed;
        moveVector.z += _zSpeed;
        _characterController.Move(moveVector);
    }
    private void ApplyWeaponTilt()
    {
        _bazooka.transform.Rotate(_weaponTilt * _weaponTiltSpeed);
        _otherWeapon.transform.Rotate(_weaponTilt * _weaponTiltSpeed);
    }

    public void Shoot()
    {
        if (!_didAction && _equippedWeaponIndex == 0)
        {
            _bazooka.StartCharge();
            EnterChargeState();
        }
        else if (!_didAction && _equippedWeaponIndex == 1)
        {
            _otherWeapon.StartCharge();
            EnterChargeState();
        }
    }

    public void LaunchRocket()
    {
        if (!_didAction && _equippedWeaponIndex == 0)
        {
            _bazooka.Shoot();
            ExitChargeState();
        }
        else if (!_didAction && _equippedWeaponIndex == 1 && _hasAmmo)
        {
            _otherWeapon.Shoot();
            ExitChargeState();
        }
    }
    private void EnterChargeState()
    {
        _faceSwapper.SetAngryFace();
        _rotationSpeed = _slowRotationSpeed;
        _moveSpeed = _slowMoveSpeed;
        TurnsManager.GetInstance().StopTurnTimer();
    }

    private void ExitChargeState()
    {
        _faceSwapper.SetNeutralFace();
        _rotationSpeed = _fastRotationSpeed;
        _moveSpeed = _fastMoveSpeed;
        _didAction = true;
        TurnsManager.GetInstance().StartTurnTimer(_didAction);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _ySpeed = _jumpHeight;
            SoundManager.GetInstance().PlayJumpSound();
            _animations.PlayJumpAnimation();
        }
    }

    public void TiltWeapon(float direction)
    {
        _weaponTilt.x = -direction;
    }

    public void NextWeapon()
    {
        _equippedWeaponIndex = _inventory.EquipNextWeapon();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " took " + damage + " points of damage");
        stats.TakeDamage(damage);
        _faceSwapper.SetConcernedFace();
        _animations.PlayDamageAnimation();
        _hpBar.UpdateHealthBar(stats.GetNormalizedHp());
    }

    public void ApplyKnockback(Vector3 direction, float intensity)
    {
        Debug.DrawRay(transform.position, direction * 10, Color.red, 5);
        _xSpeed = direction.x * intensity;
        _ySpeed = direction.y * intensity * _verticalKnockbackModifier;
        _zSpeed = direction.z * intensity;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // do landing juice please: https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

        if (hit.gameObject.tag == "Pickup") // do early return?
        {
            Destroy(hit.gameObject.transform.parent.gameObject);
            stats.SetHp(stats.GetHp() + _healthPackRestoreAmount);
            _hpBar.UpdateHealthBar(stats.GetNormalizedHp());
        }
        else if (hit.gameObject.tag == "Pickup") // do ammo pickup
        {
            Destroy(hit.gameObject.transform.parent.gameObject);
            stats.SetHp(stats.GetHp() + _healthPackRestoreAmount);
            _hpBar.UpdateHealthBar(stats.GetNormalizedHp());
        }
    }

    private void Deactivate()
    {
        _weaponTilt = Vector3.zero;
        inputVector.x = 0;
        inputVector.y = 0;
        _didAction = false;
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= Deactivate;
    }
}
