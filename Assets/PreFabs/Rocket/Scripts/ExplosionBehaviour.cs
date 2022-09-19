using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ExplosionBehaviour : MonoBehaviour
{
    private float _timer;
    private float _lifetime = 0.7f;
    private float _lightLifetime = 0.1f;

    private Light _pointLight;

    private void Awake()
    {
        _timer = 0.0f;
        _pointLight = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _lightLifetime)
        {
            _pointLight.intensity = 0;
        }
        if (_timer > _lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}
