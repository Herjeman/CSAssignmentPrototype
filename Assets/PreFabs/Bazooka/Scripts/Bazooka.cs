using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Bazooka  : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _chargeIndicator;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _turnsManager;
    [SerializeField] private float _charge;
    [SerializeField] private float _force;
    [SerializeField] private float _blastRadius;

    private float _chargeTimer;
    private float _maxCharge = 3;
    private bool _charging;

    //Use this code for when eqiupping bazooka...
    //private GameObject _turnsManager;
    //public void Init(GameObject turnsmanager)
    //{
    //    _turnsManager = turnsmanager;
    //}

    private void Update()
    {
        if (_charging)
        {
            ContinueCharging();
        }
    }

    public GameObject LaunchRocket()
    {
        GameObject launchedRocket = Instantiate(_rocket, _spawnPoint.position, transform.rotation);
        launchedRocket.GetComponent<Rigidbody>().AddForce(transform.forward * _force, ForceMode.Impulse);
        launchedRocket.GetComponent<RocketBehaviour>().Init(transform.parent.gameObject, _turnsManager, _blastRadius);
        _charging = false;
        _chargeIndicator.SetActive(false);
        return launchedRocket;
    }

    public void StartCharge()
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
