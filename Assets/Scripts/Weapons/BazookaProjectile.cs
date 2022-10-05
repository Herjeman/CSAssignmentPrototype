using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaProjectile : BaseProjectile
{
    [SerializeField] private Transform _noseTransform;
    [SerializeField] private GameObject _explosion;

    [SerializeField] private float _screenshakeTime = 0.1f;
    [SerializeField] private float _screenshakeIntensity = 1;
    [SerializeField] private float _screenshakeDuration = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent == null)
        {
            if (collision.gameObject == shootingPlayer)
            {
                return;
            }
        }

        SpawnExplosionEffect();
        soundManager.PlayExplosionSound();
        CheckForHits();
        Destroy(this.gameObject);
    }

    private void CheckForHits()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider collider in collidersHit)
        {
            if (collider.gameObject.tag == "Player")
            {
                Impact impact = ImpactCalculator.GetImpact(_noseTransform.position, collider.transform.position, blastRadius);
                float damage = baseDamage * impact.intensity;

                WormController hitWorm = collider.gameObject.GetComponent<WormController>();
                hitWorm.TakeDamage(((int)damage));
                hitWorm.ApplyKnockback(impact.direction, impact.intensity);
            }
            else if (collider.gameObject.tag == "Destructible")
            {
                Impact impact = ImpactCalculator.GetImpact(_noseTransform.position, collider.ClosestPoint(transform.position), blastRadius);
                float damage = baseDamage * impact.intensity + fixedDamage;
                collider.gameObject.GetComponent<Destructible>().TakeDamage(((int)damage));
            }
        }
    }

    private void SpawnExplosionEffect()
    {
        ExplosionBehaviour explosion = Instantiate(_explosion, transform.position, Quaternion.identity).GetComponent<ExplosionBehaviour>();
        explosion.Init(_screenshakeTime, _screenshakeIntensity, _screenshakeDuration);
    }
}
