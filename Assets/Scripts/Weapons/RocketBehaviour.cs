using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Vector3 _rotation;
    private Rigidbody _rb;
    private float _blastRadius;
    private int _baseDamage;
    private int _fixedDamage;

    [SerializeField] private Transform _noseTransform;
    [SerializeField] private GameObject _explosion;

    private GameObject _shootingPlayer;
    private SoundManager _soundManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _fixedDamage = 5;
        _soundManager = SoundManager.GetInstance();
        _soundManager.PlayLaunchSound();
    }


    void Update()
    {
        _rotation = _rb.velocity;
        _rotation.Normalize();

        Vector3 newDirection = Vector3.RotateTowards(transform.position, _rotation, 10F, 10F);
        transform.rotation = Quaternion.LookRotation(newDirection);
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
        _soundManager.PlayExplosionSound();
        CheckForHits();
        Destroy(this.gameObject);
    }

    public void Init(GameObject player, float blastRadius, int baseDamage)
    {
        _shootingPlayer = player;
        _blastRadius = blastRadius;
        _baseDamage = baseDamage;
        TurnsManager.OnTurnEnd += RemoveThis;
    }

    private void CheckForHits()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, _blastRadius); // detta tar ALLT in range, g�r ev. en raycast till tr�ffade object f�r att se om de �r i skydd eller inte!
        foreach(Collider collider in collidersHit)
        {
            if (collider.gameObject.tag == "Player")
            {
                Impact impact = ImpactCalculator.GetImpact(_noseTransform.position, collider.transform.position, _blastRadius);
                float damage = _baseDamage * impact.intensity;

                WormController hitWorm = collider.gameObject.GetComponent<WormController>();
                hitWorm.TakeDamage(((int)damage));
                hitWorm.ApplyKnockback(impact.direction, impact.intensity);
            }
            else if (collider.gameObject.tag == "Destructible")
            {
                Impact impact = ImpactCalculator.GetImpact(_noseTransform.position, collider.ClosestPoint(transform.position), _blastRadius);
                float damage = _baseDamage * impact.intensity + _fixedDamage;
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
