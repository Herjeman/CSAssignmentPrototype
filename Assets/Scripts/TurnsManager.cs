using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameSetUp _gameSetUp;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameUI _gameUi;

    [SerializeField] private float _turnTime = 10;
    [SerializeField] private float _retreatTime = 5;

    private List<Player> _players;
    private static TurnsManager instance;
    private GameObject _activeWorm;

    private Player _currentPlayer;
    private int _currentPlayerIndex;
    private float _timer;
    private bool _runTurnTimer;
    private int numberOfTurns;

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
    public static event TurnManagerUpdate OnTurnStart;

    private void Start()
    {
        _players = _gameSetUp.CreatePlayers();
        _spawner.SpawnWorms(_players);
        _currentPlayer = _players[0];
        _activeWorm = _currentPlayer.GetWorm();
        _currentPlayer.NextWorm();
        _timer = _turnTime;
        _runTurnTimer = false;
        OnTurnEnd();
    }

    private void Update() // restructure this if block, it's a mess
    {
        if (_runTurnTimer)
        {
        _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                if (UpdateTurn())
                {
                    GameOver(GetWinner());
                }
            }
        }
    }


    public bool UpdateTurn()
    {
        if (CheckForWin()) // put outside
        {
            return true;
        }

        NextPlayer();

        _currentPlayer.NextWorm();
        _activeWorm = _currentPlayer.GetWorm();
        _timer = _turnTime;
        StopTurnTimer();
        OnTurnEnd();
        return false;
    }

    private void NextPlayer()
    {
        _currentPlayerIndex++;
        if (_currentPlayerIndex >= _players.Count)
        {
            _currentPlayerIndex = 0;
        }
        _currentPlayer = _players[_currentPlayerIndex];

        //Skip players with no worms left, change this to updating the playerslist instead...
        while(_currentPlayer.worms.Count <= 0)
        {
            _currentPlayerIndex++;
            if (_currentPlayerIndex >= _players.Count)
            {
                _currentPlayerIndex = 0;
            }
            _currentPlayer = _players[_currentPlayerIndex];
        }
    }

    private bool CheckForWin()
    {
        bool win = false;
        int playersLeft = _players.Count;
        foreach (Player player in _players) // simplify by updatinh players list every time a player runs out of worms
        {
            if (player.worms.Count <= 0)
            {
                playersLeft--;
            }
        }
        if (playersLeft <= 1)
        {
            win = true;
        }
        return win;
    }

    private Player GetWinner()
    {
        Player winner = new Player("Default winner", 42, 0, Color.white);
        foreach (Player player in _players)
        {
            if (player.worms.Count > 0)
            {
                winner = player;
            }
        }
        return winner;
    }

    private void GameOver(Player winner)
    {
        _gameUi.gameOverMessage = $"Game over, {winner.playerName} has won the game!";
        _gameUi.gameIsRunning = false;
         OnTurnEnd();
    }

    public void StopTurnTimer()
    {
        _runTurnTimer = false;
    }

    public void StartTurnTimer(bool didAction=false)
    {
        if (didAction)
        {
            _timer = _retreatTime;
        }
        _runTurnTimer = true;
    }

    public void StartNewTurn()
    {
        OnTurnStart();
        if (CheckForWin())
        {
            GameOver(GetWinner());
        }
        StartTurnTimer();

    }

    public int GetRemainingTurnTime()
    {
        return ((int)_timer);
    }

    public  Player GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public GameObject GetActiveWorm()
    {
        return _activeWorm;
    }
}

