using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber;


    Rigidbody rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _startingHp; 
    [SerializeField] private int _shootDamage;

    private RaycastHit _hitObject;
    public TurnsManager turnsManager; //this is an instantiation of turns Manager, this won't do...See if you can create another type of class?!
    public Stats stats;

    private void Start()
    {
        turnsManager = FindObjectOfType<TurnsManager>();
        rb = GetComponent<Rigidbody>();
        stats = new Stats();
        stats.SetHp(_startingHp);
    }

    void Update()
    {
        if (turnsManager.playerTurn != playerNumber)
        {
            if(stats.GetHp() < 0)
            {
                Destroy(gameObject);
                Debug.Log(gameObject.name + " died");
            }
            return;
        }
        ProcessInput();
    }

    void EndTurn()
    {
        turnsManager.NextPlayer();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * _moveSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * _moveSpeed;
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

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            EndTurn();
        }
    }
}
