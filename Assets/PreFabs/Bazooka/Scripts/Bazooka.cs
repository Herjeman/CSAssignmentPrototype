using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka  : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _chargeIndicator;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _charge;
    [SerializeField] private float _force;

    private float _chargeTimer;
    private float _maxCharge = 3;
    private bool _charging;

    private void Update()
    {
        //Debug.Log("Bazooka forward is: " + transform.forward);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCharge();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log($"Launched rocket in {transform.forward} direction at {_force} speed");
            GameObject rocket = LaunchRocket(transform.forward, _force);
            rocket.GetComponent<RocketBehaviour>().Init(transform.parent.gameObject);
        }
        if (_charging)
        {
            ContinueCharging();
        }
    }

    public GameObject LaunchRocket(Vector3 direction, float force)
    {
        GameObject launchedRocket = Instantiate(_rocket, _spawnPoint.position, transform.rotation);
        launchedRocket.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
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
