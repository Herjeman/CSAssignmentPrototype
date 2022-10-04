using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CCProjectile : BaseProjectile
{
    [SerializeField] private GameObject _clusterCharge;
    [SerializeField] private GameObject _releaseParticles;
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
        GameObject charge = Instantiate(_clusterCharge, transform.position, transform.rotation);
        Rigidbody[] chargeRigidbodies = charge.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in chargeRigidbodies)
        {
            rb.velocity = _myRigidbody.velocity;
        }
        Instantiate(_releaseParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
