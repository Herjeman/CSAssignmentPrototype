using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Vector3 _rotation;
    private Rigidbody _rb;
    private float _blastRadius;
    [SerializeField] private GameObject _explosion;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        _rotation = _rb.velocity;
        _rotation.Normalize();

        Vector3 newDirection = Vector3.RotateTowards(transform.position, _rotation, 10F, 10F);
        transform.rotation = Quaternion.LookRotation(newDirection);

        Debug.Log(_rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnExplosionEffect();
        CheckForHits();
        Destroy(this.gameObject);
    }

    private void CheckForHits()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, _blastRadius); // detta tar ALLT in range, gör ev. en raycast till träffade object för att se om de är i skydd eller inte!
        foreach(Collider collider in collidersHit)
        {
            Debug.Log($"Rocketblast hit {collider.name}, at {collider.transform.position}");
        }
    }

    private void SpawnExplosionEffect()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}
