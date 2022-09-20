using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    private FaceSwapper _faceSwapper;
    private PlayerAnimations _animations;

    private float _ySpeed = 0;
    private bool _isGrounded;



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _turnsManager = TurnsManager.GetInstance();
        _rb = GetComponent<Rigidbody>();
        _faceSwapper = GetComponentInChildren<FaceSwapper>();
        _animations = GetComponentInChildren<PlayerAnimations>();

        stats = new Stats();
        stats.SetHp(_startingHp);
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
        return;
    }

    public void FixedUpdate()
    {
        ApplyGravity();
        Move();
        //Debug.Log(gameObject.name + "is grunded = " + _characterController.isGrounded);
    }


    private void Die()
    {
        bool died = true;
        Destroy(gameObject);
        _turnsManager._playerList.Remove(this.gameObject); // don't use player turn... 
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

    private bool CheckForGround()
    {
        return false;
    }

    public void Move()
    {
        transform.Rotate(0, inputVector.x * _rotationSpeed, 0);
        Vector3 moveVector = transform.rotation * new Vector3(0, _ySpeed, inputVector.y * _moveSpeed);
        _characterController.Move(moveVector);
    }

    public void Shoot()
    {
        ShootManager.Shoot(transform.position, transform.forward, _shootDamage);
        _faceSwapper.SetAngryFace();
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

    void EndTurn()
    {
        TurnsManager.GetInstance().UpdateTurn();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.velocity = transform.forward * _moveSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rb.velocity = -transform.forward * _moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -_rotationSpeed, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, _rotationSpeed, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootManager.Shoot(transform.position, transform.forward, _shootDamage);
        }

        // this can't be here, get's called =to the amount of scripts in project... make input manager separate script? singleton?
        //if (Input.GetKeyDown(KeyCode.G)) 
        //{
        //    Debug.Log("EndTurn was called");
        //    EndTurn();
        //}
    }
}
