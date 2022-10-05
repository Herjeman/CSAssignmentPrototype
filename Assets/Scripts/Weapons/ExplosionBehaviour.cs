using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ExplosionBehaviour : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float _lifetime = 0.7f;
    [SerializeField] private float _lightLifetime = 0.1f;
    private bool _hasLight;
    private bool _appliedScreenShake;

    private float _screenshakeTime = 0.1f;
    private float _screenshakeIntensity = 1;
    private float _screenshakeDuration = 1;
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

    public void Init(float screenshakeTime=0.1f, float screenshakeIntensity=1, float screenshakeDuration=1 )
    {
        _screenshakeTime = screenshakeTime;
        _screenshakeIntensity = screenshakeIntensity;
        _screenshakeDuration = screenshakeDuration;

    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_hasLight && _timer > _lightLifetime)
        {
            _pointLight.intensity = 0;
        }
        if (!_appliedScreenShake && _timer > _screenshakeTime)
        {
            CameraOrbit.GetInstance().ApplyScreenShake(_screenshakeIntensity, _screenshakeDuration);
            _appliedScreenShake = true;
        }
        if (_timer > _lifetime)
        {
            Destroy(this.gameObject);
        }
    }
}
