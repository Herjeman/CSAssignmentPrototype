using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka  : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _force;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject rocket = LaunchRocket(transform.forward, _force);
        }
    }

    public GameObject LaunchRocket(Vector3 direction, float force)
    {
        GameObject launchedRocket = Instantiate(_rocket, _spawnPoint);
        launchedRocket.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        return launchedRocket;
    }
}
