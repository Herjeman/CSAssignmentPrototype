using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber;

    Rigidbody rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _startingHp; 
    [SerializeField] private int _shootDamage;

    //TurnsManager turnsManager; //this is an instantiation of turns Manager, this won't do...See if you can create another type of class? : Made singleton...
    public Stats stats;

    private void Start()
    {
        //turnsManager = FindObjectOfType<TurnsManager>(); : Made singleton
        rb = GetComponent<Rigidbody>();
        stats = new Stats();
        stats.SetHp(_startingHp);
    }

    void Update()
    {
        if (TurnsManager.GetInstance().playerTurn != playerNumber)
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
        TurnsManager.GetInstance().NextPlayer();
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

        // this can't be here, get's called =to the amount of scripts in project... make input manager separate script? singleton?
        //if (Input.GetKeyDown(KeyCode.G)) 
        //{
        //    Debug.Log("EndTurn was called");
        //    EndTurn();
        //}
    }
}
