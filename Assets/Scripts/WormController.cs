using UnityEngine;

public class WormController : MonoBehaviour
{
    public int playerNumber;
    public Vector2 inputVector;
    public Stats stats;

    TurnsManager _turnsManager;
    Rigidbody _rb;
    CharacterController _characterController;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _startingHp; 
    [SerializeField] private int _shootDamage;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityAccelleration = 0.01f;
    [SerializeField] private float _weaponTiltSpeed;

    private Bazooka _bazooka;
    private FaceSwapper _faceSwapper;
    private PlayerAnimations _animations;

    private Vector3 _weaponTilt = Vector3.zero;
    private float _slowMoveSpeed;
    private float _slowRotationSpeed;
    private float _fastMoveSpeed;
    private float _fastRotationSpeed;
    private float _ySpeed = 0;
    //private bool _isGrounded;



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _turnsManager = TurnsManager.GetInstance();
        _rb = GetComponent<Rigidbody>();
        _faceSwapper = GetComponentInChildren<FaceSwapper>();
        _animations = GetComponentInChildren<PlayerAnimations>();
        _bazooka = GetComponentInChildren<Bazooka>();

        stats = new Stats();
        stats.SetHp(_startingHp);

        _slowMoveSpeed = _moveSpeed * 0.25f;
        _slowRotationSpeed = _rotationSpeed * 0.25f;
        _fastMoveSpeed = _moveSpeed;
        _fastRotationSpeed = _rotationSpeed;
        Debug.Log("Worm is alive");
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

    }

    public void FixedUpdate()
    {
        ApplyBazookaTilt();
        ApplyGravity();
        Move();
    }


    private void Die()
    {
        bool died = true;
        Destroy(gameObject);
        _turnsManager._activeWormsList.Remove(this.gameObject); // Do something in OnDestroy???
        _turnsManager.UpdateTurn(died);
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

    public void Move()
    {
        transform.Rotate(0, inputVector.x * _rotationSpeed, 0);
        Vector3 moveVector = transform.rotation * new Vector3(0, _ySpeed, inputVector.y * _moveSpeed);
        _characterController.Move(moveVector);
    }

    public void StartCharge()
    {
        _bazooka.StartCharge();
        _faceSwapper.SetAngryFace();
        _rotationSpeed = _slowRotationSpeed;
        _moveSpeed = _slowMoveSpeed;
    }

    public void Shoot()
    {
        _bazooka.LaunchRocket();
        _faceSwapper.SetNeutralFace();
        _rotationSpeed = _fastRotationSpeed;
        _moveSpeed = _fastMoveSpeed;
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
        Debug.Log("TiltWeapon was called with value: " + direction);
        _weaponTilt.x = -direction;
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
