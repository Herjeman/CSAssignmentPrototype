using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Vector3 _rotation;
    private Rigidbody _rb;
    private float _blastRadius;
    [SerializeField] private GameObject _explosion;

    private GameObject _shootingPlayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        // add turnsmanager OnTurnEnd Event Listener
    }


    void Update()
    {
        _rotation = _rb.velocity;
        _rotation.Normalize();

        Vector3 newDirection = Vector3.RotateTowards(transform.position, _rotation, 10F, 10F);
        transform.rotation = Quaternion.LookRotation(newDirection);

        Debug.Log($"Rocket forward direction is: {transform.forward}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject == _shootingPlayer || collision.gameObject.transform.parent.gameObject == _shootingPlayer)
        {
            return;
        }
        SpawnExplosionEffect();
        CheckForHits();
        Destroy(this.gameObject);
    }

    public void Init(GameObject player)
    {
        _shootingPlayer = player;
    }

    private void CheckForHits()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, _blastRadius); // detta tar ALLT in range, gör ev. en raycast till träffade object för att se om de är i skydd eller inte!
        foreach(Collider collider in collidersHit)
        {
            //Debug.Log($"Rocketblast hit {collider.gameObject.name}, at {collider.transform.position}");
        }
    }

    private void SpawnExplosionEffect()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        //Remove listener for end turn
    }
}
