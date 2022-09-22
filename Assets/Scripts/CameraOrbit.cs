using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;

    //[SerializeField] private float _smoothness;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private int _cameraMaxDistance;
    [SerializeField] private int _cameraMinDistance;

    private Transform _targetTransform;
    private TurnsManager _turnsManager;
    private Vector3 _cameraPosition;
    private float _cameraDistance;

    private void Start()
    {
        _turnsManager = _gameManager.GetComponent<TurnsManager>();
        TurnsManager.OnTurnEnd += UpdateActivePlayer;
        _targetTransform = _turnsManager.GetActiveWorm().transform;
    }

    private void LateUpdate()
    {
        _offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _turnSpeed, Vector3.up) * _offset; // add option to invert?
        _offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * _turnSpeed, -transform.right) * _offset;  // add option to invert?

        _cameraDistance += -Input.mouseScrollDelta.y * _zoomSpeed; // add option to invert?

        if (_cameraDistance < _cameraMinDistance){_cameraDistance = _cameraMinDistance;}
        if (_cameraDistance > _cameraMaxDistance){_cameraDistance = _cameraMaxDistance;}

        _cameraPosition = _targetTransform.position + _offset * _cameraDistance;

        transform.position = _cameraPosition;
        transform.LookAt(_targetTransform.position); // maybe Lerp this
    }

    private void UpdateActivePlayer()
    {
        _targetTransform = _turnsManager.GetActiveWorm().transform;
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= UpdateActivePlayer;
    }
}
