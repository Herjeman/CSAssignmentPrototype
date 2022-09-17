using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;

    [SerializeField] private float _smoothness;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private int _cameraMaxDistance;
    [SerializeField] private int _cameraMinDistance;

    private Transform _targetTransform;
    private TurnsManager _turnsManager;
    private Vector3 _Offset;
    private Vector3 _cameraPosition;
    private float _cameraDistance;

    private void Start()
    {
        _turnsManager = _gameManager.GetComponent<TurnsManager>();
        _targetTransform = _turnsManager.GetActivePlayer().transform;
        _Offset = transform.position - _targetTransform.position;
    }

    //public void SetTargetTransform(Transform targetTransform)
    //{
    //    _targetTransform = targetTransform;
    //}

    private void LateUpdate()
    {
        _Offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _turnSpeed, Vector3.up) * _Offset; // add option to invert?
        _Offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * _turnSpeed, -transform.right) * _Offset;  // add option to invert?

        _cameraDistance += -Input.mouseScrollDelta.y * _zoomSpeed; // add option to invert?
        //Debug.Log(Input.mouseScrollDelta.y);
        //stuff above here should go in at least update, preferably in InputManager

        if (_cameraDistance < _cameraMinDistance)
        {
            _cameraDistance = _cameraMinDistance;
        }
        if (_cameraDistance > _cameraMaxDistance)
        {
            _cameraDistance = _cameraMaxDistance;
        }

        _targetTransform = _turnsManager.GetActivePlayer().transform;
        _cameraPosition = _targetTransform.position + _Offset * _cameraDistance;
        //transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime); // requires LookAt lerping... I think, else jittering
        transform.position = _cameraPosition;
        transform.LookAt(_targetTransform.position); // maybe Lerp this
    }
}
