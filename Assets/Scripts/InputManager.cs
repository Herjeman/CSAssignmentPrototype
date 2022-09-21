using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //public GameObject activePlayer;
    [SerializeField] private GameObject _gameManager;

    private GameObject _activePlayer;
    private TurnsManager _turnsManager;
    private PlayerController _playerController;
    private Vector2 _moveValue;

    private void Start()
    {        
        _turnsManager = _gameManager.GetComponent<TurnsManager>();
        _activePlayer = _turnsManager.GetActivePlayer();
        _playerController = _activePlayer.GetComponent<PlayerController>();
        TurnsManager.OnTurnEnd += UpdateActivePlayer;
    }

    public void Move(InputAction.CallbackContext context) //also rotates...
    {
        _moveValue = context.ReadValue<Vector2>();
        _playerController.inputVector = _moveValue;
    }


    public void Shoot(InputAction.CallbackContext context)
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

    public void TiltWeapon(InputAction.CallbackContext context)
    {
        //Debug.Log("TiltWeapon was called with value: " + context.ReadValue<float>());
        _playerController.TiltWeapon(context.ReadValue<float>());
 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump was called");
            _playerController?.Jump();
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
        _activePlayer = _turnsManager.GetActivePlayer();
        _playerController = _activePlayer.GetComponent<PlayerController>();
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= UpdateActivePlayer;
    }
}
