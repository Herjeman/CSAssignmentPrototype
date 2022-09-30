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

    private Bazooka _bazooka;
    private FaceSwapper _faceSwapper;
    private PlayerAnimations _animations;
    private Player _controllingPlayer;
    private HpBar _hpBar;

    private Vector3 _weaponTilt = Vector3.zero;
    private float _slowMoveSpeed;
    private float _slowRotationSpeed;

    private float _fastMoveSpeed;
    private float _fastRotationSpeed;

    private float _xSpeed;
    private float _ySpeed;
    private float _zSpeed;

    private bool _didAction;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _faceSwapper = GetComponentInChildren<FaceSwapper>();
        _animations = GetComponentInChildren<PlayerAnimations>();
        _bazooka = GetComponentInChildren<Bazooka>();
        _hpBar = GetComponentInChildren<HpBar>();

        stats = new Stats(_startingHp);

        _slowMoveSpeed = _moveSpeed * 0f; // replace this with changing the bazooka, so it fires automatically at full charge. maybe?
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
        ApplyBazookaTilt();
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

    private void ApplyBazookaTilt()
    {
        _bazooka.transform.Rotate(_weaponTilt * _weaponTiltSpeed);
    }

    private void Move()
    {
        transform.Rotate(0, inputVector.x * _rotationSpeed, 0);
        Vector3 moveVector = transform.rotation * new Vector3(0, _ySpeed, inputVector.y * _moveSpeed);
        moveVector.x += _xSpeed;
        moveVector.z += _zSpeed;
        _characterController.Move(moveVector);
    }

    public void StartCharge()
    {
        if (!_didAction)
        {
            _bazooka.StartCharge();
            _faceSwapper.SetAngryFace();
            _rotationSpeed = _slowRotationSpeed;
            _moveSpeed = _slowMoveSpeed;
            TurnsManager.GetInstance().StopTurnTimer();
        }
    }

    public void Shoot()
    {
        if (!_didAction)
        {
            _bazooka.LaunchRocket();
            _faceSwapper.SetNeutralFace();
            _rotationSpeed = _fastRotationSpeed;
            _moveSpeed = _fastMoveSpeed;
            _didAction = true;
            TurnsManager.GetInstance().StartTurnTimer(_didAction);
        }
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _ySpeed = _jumpHeight;
            PlayerSounds.GetInstance().PlayJumpSound();
            _animations.PlayJumpAnimation();
            _faceSwapper.SetNeutralFace();
        }
    }

    public void TiltWeapon(float direction)
    {
        _weaponTilt.x = -direction;
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
        if (hit.gameObject.tag == "Pickup")
        {
            Destroy(hit.gameObject.transform.parent.gameObject);
            stats.SetHp(stats.GetHp() + 25); //hardcoded variable!!! fix pls
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
