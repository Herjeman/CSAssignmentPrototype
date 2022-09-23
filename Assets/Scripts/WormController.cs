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
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityAccelleration = 0.01f;
    [SerializeField] private float _weaponTiltSpeed;

    private Bazooka _bazooka;
    private FaceSwapper _faceSwapper;
    private PlayerAnimations _animations;
    private Player _controllingPlayer;

    private Vector3 _weaponTilt = Vector3.zero;
    private float _slowMoveSpeed;
    private float _slowRotationSpeed;
    private float _fastMoveSpeed;
    private float _fastRotationSpeed;
    private float _ySpeed;
    private bool _didAction;
    private int _minimumYLevel = -20; 

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _faceSwapper = GetComponentInChildren<FaceSwapper>();
        _animations = GetComponentInChildren<PlayerAnimations>();
        _bazooka = GetComponentInChildren<Bazooka>();

        stats = new Stats();
        stats.SetHp(_startingHp);

        _slowMoveSpeed = _moveSpeed * 0.25f;
        _slowRotationSpeed = _rotationSpeed * 0.25f;
        _fastMoveSpeed = _moveSpeed;
        _fastRotationSpeed = _rotationSpeed;

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
            Die();
        }
        else if(hp < 5)
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
        ApplyBazookaTilt();
        Move();
    }


    private void Die()
    {
        if (TurnsManager.GetInstance().GetActiveWorm() == this.gameObject)
        {
            TurnsManager.GetInstance().UpdateTurn();
        }
        _controllingPlayer.RemoveDeadWorm(this.gameObject);
        Destroy(gameObject);
        Debug.Log(gameObject.name + " died");
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            _ySpeed -= _gravityAccelleration;
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
    //    void EndTurn()
    //    {
    //        TurnsManager.GetInstance().UpdateTurn();
    //    }

    //    void ProcessInput()
    //    {
    //        if (Input.GetKey(KeyCode.W))
    //        {
    //            _rb.velocity = transform.forward * _moveSpeed;
    //        }

    //        if (Input.GetKey(KeyCode.S))
    //        {
    //            _rb.velocity = -transform.forward * _moveSpeed;
    //        }

    //        if (Input.GetKey(KeyCode.A))
    //        {
    //            transform.Rotate(new Vector3(0, -_rotationSpeed, 0));
    //        }

    //        if (Input.GetKey(KeyCode.D))
    //        {
    //            transform.Rotate(new Vector3(0, _rotationSpeed, 0));
    //        }

    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            ShootManager.Shoot(transform.position, transform.forward, _shootDamage);
    //        }
    //    }

}
