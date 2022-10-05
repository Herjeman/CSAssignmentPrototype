using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CCProjectile : BaseProjectile
{
    [SerializeField] private GameObject _clusterCharge;
    [SerializeField] private GameObject _releaseParticles;
    [SerializeField] private float _explosionDelay;
    [SerializeField] private float _explosionSequenceSpeed;

    private Rigidbody _myRigidbody;
    private float _timer;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _timer = 1.5f;
    }

    private void LateUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            ReleaseCharge();
        }
    }

    private void ReleaseCharge()
    {
        GameObject chargeCluster = Instantiate(_clusterCharge, transform.position, transform.rotation);
        Rigidbody[] chargeRigidbodies = chargeCluster.GetComponentsInChildren<Rigidbody>();
        float delay = _explosionDelay;

        foreach (Rigidbody rb in chargeRigidbodies)
        {
            rb.velocity = _myRigidbody.velocity;
            rb.GetComponent<CCCharge>().Init(delay, blastRadius, baseDamage, fixedDamage);
            delay += _explosionSequenceSpeed;
        }

        Instantiate(_releaseParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
