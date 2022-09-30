using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TurnsManager _turnsManager;
    [SerializeField] private GameUI _gameUi;

    private GameObject _activeWorm;
    private WormController _wormController;
    private Vector2 _moveValue;
    private bool _allowWormControl;
    private float _nextPlayerScreenGraceTime = 1;
    private float _graceTimer;

    private void Start()
    {        
        _activeWorm = _turnsManager.GetActiveWorm();
        _wormController = _activeWorm.GetComponent<WormController>();
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
        if (!_allowWormControl && _graceTimer <= 0)
        {
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
            _wormController.inputVector = _moveValue;
        }
    }


    public void Shoot(InputAction.CallbackContext context)
    {
        if (_allowWormControl)
        {
            if (context.started)
            {
                _wormController.StartCharge();
            }
            else if (context.canceled)
            {
                _wormController.Shoot();
            }
        }
    }

    public void TiltWeapon(InputAction.CallbackContext context)
    {
        if (_allowWormControl)
        {
            _wormController.TiltWeapon(context.ReadValue<float>());
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_allowWormControl)
        {
            if (context.performed)
            {
                _wormController.Jump();
            }
        }
    }

    public void ToggleControlsUI(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _gameUi.ToggleControlsExplanation();
        }

    }

    private void UpdateActivePlayer()
    {
        _activeWorm = _turnsManager.GetActiveWorm();
        _wormController = _activeWorm.GetComponent<WormController>();
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
