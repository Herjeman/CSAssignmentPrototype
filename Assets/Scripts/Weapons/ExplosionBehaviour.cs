using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ExplosionBehaviour : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float _lifetime = 0.7f;
    [SerializeField] private float _lightLifetime = 0.1f;
    [SerializeField] private float _screenShakeTime = 0.1f;
    private bool _hasLight;
    private bool _appliedScreenShake;

    [SerializeField] private float _screenShakeIntensity = 1;
    [SerializeField] private float _screenShakeDuration = 5;
    private Light _pointLight;

    private void Awake()
    {
        _timer = 0.0f;
        _pointLight = GetComponentInChildren<Light>();
        _appliedScreenShake = false;
        if (_pointLight != null)
        {
            _hasLight = true;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_hasLight && _timer > _lightLifetime)
        {
            _pointLight.intensity = 0;
        }
        if (!_appliedScreenShake && _timer > _screenShakeTime)
        {
            CameraOrbit.GetInstance().ApplyScreenShake(_screenShakeIntensity, _screenShakeDuration);
            _appliedScreenShake = true;
        }
        if (_timer > _lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}
