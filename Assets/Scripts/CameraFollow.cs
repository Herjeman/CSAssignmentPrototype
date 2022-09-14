using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;
    [SerializeField] private float _smoothness;

    private Vector3 _initialOffset;
    private Vector3 _cameraPosition;

    private void Start()
    {
        _initialOffset = transform.position - _targetObject.position;
    }

    private void FixedUpdate()
    {
        _cameraPosition = _targetObject.position + _initialOffset;
        transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);
    }
}
