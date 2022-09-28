using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Vector3 _rotation;
    private Rigidbody _rb;
    private float _blastRadius;
    private int _baseDamage;

    [SerializeField] private GameObject _explosion;

    private GameObject _shootingPlayer;
    private GameObject _turnsManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        _rotation = _rb.velocity;
        _rotation.Normalize();

        Vector3 newDirection = Vector3.RotateTowards(transform.position, _rotation, 10F, 10F);
        transform.rotation = Quaternion.LookRotation(newDirection);

        //Debug.Log($"Rocket forward direction is: {transform.forward}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent == null)
        {
            if (collision.gameObject == _shootingPlayer)
            {
                return;
            }
        }

        SpawnExplosionEffect();
        CheckForHits();
        Destroy(this.gameObject);
    }

    public void Init(GameObject player, GameObject turnsManager, float blastRadius, int baseDamage)
    {
        _shootingPlayer = player;
        _turnsManager = turnsManager;
        _blastRadius = blastRadius;
        _baseDamage = baseDamage;
        TurnsManager.OnTurnEnd += RemoveThis;
    }

    private void CheckForHits()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, _blastRadius); // detta tar ALLT in range, gör ev. en raycast till träffade object för att se om de är i skydd eller inte!
        foreach(Collider collider in collidersHit)
        {
            if (collider.gameObject.tag == "Player")
            {
                Impact impact = ImpactCalculator.GetImpact(transform.position, collider.transform.position, _blastRadius);
                float damage = _baseDamage * impact.intensity;

                WormController hitWorm = collider.gameObject.GetComponent<WormController>();
                hitWorm.TakeDamage(((int)damage));
                hitWorm.ApplyKnockback(impact.direction, impact.intensity);
            }
            else if (collider.gameObject.tag == "Destructible")
            {
                Impact impact = ImpactCalculator.GetImpact(transform.position, collider.ClosestPoint(transform.position), _blastRadius);
                float damage = _baseDamage * impact.intensity;
                collider.gameObject.GetComponent<Destructible>().TakeDamage(((int)damage));
            }
        }
    }

    private void SpawnExplosionEffect()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }

    private void RemoveThis() 
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= RemoveThis;
    }
}
