using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnsManager : MonoBehaviour
{
    public int numberOfTurns;


    public List<Player> players;
    public List<GameObject> _activeWormsList;

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameSetUp _gameSetUp;
    [SerializeField] private Spawner _spawner;

    private static TurnsManager instance;
    private GameObject _activeWorm;

    private Player _currentPlayer;
    private int _currentPlayerIndex;

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
    }

    public static TurnsManager GetInstance()
    {
        return instance;
    }

    public delegate void TurnManagerUpdate();
    public static event TurnManagerUpdate OnTurnEnd;

    private void Start()
    {
        players = _gameSetUp.CreatePlayers();
        _spawner.SpawnWorms(players);
        _currentPlayer = players[0];
        _activeWorm = _currentPlayer.GetWorm();
        _currentPlayer.NextWorm();
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
        _currentPlayerIndex++;
        if (_currentPlayerIndex > players.Count - 1)
        {
            _currentPlayerIndex = 0;
        }

        _currentPlayer = players[_currentPlayerIndex];
        SkipPlayersWithoutWorms();     

        _activeWorm = _currentPlayer.GetWorm();
        _currentPlayer.NextWorm();
        OnTurnEnd();
    }

    private void SkipPlayersWithoutWorms()
    {
        while(_currentPlayer.worms.Count <= 0) // make sure to break out of this if all players have no worms...
        {
            _currentPlayerIndex++;
            _currentPlayer = players[_currentPlayerIndex];
        }
    }

    public GameObject GetActiveWorm()
    {
        return _activeWorm;
    }
}

