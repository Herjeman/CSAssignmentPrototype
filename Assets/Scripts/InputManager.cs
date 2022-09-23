using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _gameUiObject;

    private GameObject _activeWorms;
    private TurnsManager _turnsManager;
    private WormController _playerController;
    private GameUI _gameUi;
    private Vector2 _moveValue;
    private bool _allowWormControl = true;
    private float _nextPlayerScreenGraceTime = 1;
    private float _graceTimer;

    private void Start()
    {        
        _turnsManager = _gameManager.GetComponent<TurnsManager>();
        _activeWorms = _turnsManager.GetActiveWorm();
        _playerController = _activeWorms.GetComponent<WormController>();
        _gameUi = _gameUiObject.GetComponent<GameUI>();
        TurnsManager.OnTurnEnd += UpdateActivePlayer;
        TurnsManager.OnTurnEnd += DisableControl;
        TurnsManager.OnTurnStart += EnableControl;
    }

    private void FixedUpdate()
    {
        if (_graceTimer > 0)
        {
            _graceTimer -= Time.fixedDeltaTime;
        }
    }

    public void StartTurn(InputAction.CallbackContext context)
    {
        Debug.Log("Any key was pressed");
        if (!_allowWormControl && _graceTimer <= 0)
        {
            Debug.Log($"If statement passed allowWormControl is {_allowWormControl} and gracetimer is {_graceTimer}");
            if (context.canceled)
            {
                _turnsManager.StartNewTurn();
            }
        }
           _gameUi.GoToMainMenu();
    }

    public void Move(InputAction.CallbackContext context) //also rotates...
    {
        if (_allowWormControl)
        {
            _moveValue = context.ReadValue<Vector2>();
            _playerController.inputVector = _moveValue;
        }
    }


    public void Shoot(InputAction.CallbackContext context)
    {
        if (_allowWormControl)
        {
            if (context.started)
            {
                _playerController.StartCharge();
            }
            else if (context.canceled)
            {
                _playerController.Shoot();
            }
        }
    }

    public void TiltWeapon(InputAction.CallbackContext context)
    {
        if (_allowWormControl)
        {
            _playerController.TiltWeapon(context.ReadValue<float>());
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_allowWormControl)
        {
            if (context.performed)
            {
                Debug.Log("Jump was called");
                _playerController?.Jump();
            }
        }
    }

    public void EndTurn(InputAction.CallbackContext context)
    {
        Debug.Log("End turn was called");
    }

    public void ToggleAimMode(InputAction.CallbackContext context)
    {
        Debug.Log("Toggle aim mode was called");
    }

    private void UpdateActivePlayer()
    {
        _activeWorms = _turnsManager.GetActiveWorm();
        _playerController = _activeWorms.GetComponent<WormController>();
    }

    private void EnableControl()
    {
        _allowWormControl = true;
    }

    private void DisableControl()
    {
        _allowWormControl = false;
        _graceTimer = _nextPlayerScreenGraceTime;
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= UpdateActivePlayer;
        TurnsManager.OnTurnEnd -= DisableControl;
        TurnsManager.OnTurnStart -= EnableControl;
    }
}
