using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Bazooka  : BaseWeapon
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _chargeIndicator;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _charge;
    [SerializeField] private float _force;
    [SerializeField] private float _blastRadius;
    [SerializeField] private int _blastBaseDamage;
    [SerializeField] private int _fixedDamage;

    private float _chargeTimer;
    private float _maxCharge = 3;
    private bool _charging;

    private void Update()
    {
        if (_charging)
        {
            ContinueCharging();
        }
    }

    public override GameObject Shoot()
    {
        GameObject launchedRocket = Instantiate(_rocket, _spawnPoint.position, transform.rotation);
        launchedRocket.GetComponent<Rigidbody>().AddForce(transform.forward * _force, ForceMode.Impulse);
        launchedRocket.GetComponent<BaseProjectile>().Init(transform.parent.gameObject, _blastRadius, _blastBaseDamage, _fixedDamage);
        _charging = false;
        _chargeIndicator.SetActive(false);
        return launchedRocket;
    }

    public override void StartCharge()
    {
        _force = 0;
        _charging = true;
        _chargeIndicator.SetActive(true);
        _chargeTimer = 0;

    }

    private void ContinueCharging()
    {
        if (_chargeTimer < _maxCharge)
        {
            _force += _charge * Time.deltaTime;
            _chargeTimer += Time.deltaTime;
        }
    }
}
