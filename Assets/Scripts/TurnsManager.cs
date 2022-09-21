using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// turns manager should instantiate players and save references to each one... or something...
public class TurnsManager : MonoBehaviour
{
    public int numberOfTurns;
    public int playerTurn;

    public List<GameObject> _playerList;

    [SerializeField] private LoadInstructionsScriptableObject _instructions;
    [SerializeField] private InputManager _inputManager;

    private GameObject _activePlayer;
    private static TurnsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Debug.Log(_instructions.message);
    }

    public static TurnsManager GetInstance()
    {
        return instance;
    }

    public delegate void TurnManagerUpdate();
    public static event TurnManagerUpdate OnTurnEnd;

    private void Start()
    {
        _activePlayer = _playerList[playerTurn];
    }

    private void Update() //Move this to InputManager??
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UpdateTurn();
        }
    }


    public void UpdateTurn(bool playerHasDied = false)
    {
        if (!playerHasDied)
        {
            playerTurn++;
            numberOfTurns++;
        }
        else if (playerTurn > 0)
        { 
            playerTurn--;
        }

        if (playerTurn >= _playerList.Count)
        {
            playerTurn = 0;
        }

        _activePlayer = _playerList[playerTurn];
        OnTurnEnd();
        Debug.Log("NextPlayer was called, player turn is " + playerTurn);
    }

    public GameObject GetActivePlayer()
    {
        return _activePlayer;
    }
}

