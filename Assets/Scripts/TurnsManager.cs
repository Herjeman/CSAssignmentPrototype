using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// turns manager should instantiate players and save references to each one... or something...
public class TurnsManager : MonoBehaviour
{
    public int numberOfTurns;
    public int playerTurn;

    public List<GameObject> _playerList;

    private CameraOrbit _cameraOrbit;
    private static TurnsManager instance; // make this a singleton, maybe do this to input manager??
    private int numberOfPlayers;

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

    private void Start()
    {
        _cameraOrbit = Camera.main.GetComponent<CameraOrbit>();
        numberOfPlayers = _playerList.Count;
    }

    public static TurnsManager GetInstance()
    {
        return instance;
    }

    public void SetNumberOfPlayers(int amount)
    {
        numberOfPlayers = amount;
    }

    private void Update() //Move this to singleton InputManager??
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            NextPlayer();
            _cameraOrbit.SetTargetTransform(_playerList[playerTurn].transform); // <-- active playerobject transform goes here
            AddTurn();
        }
    }

    public void AddTurn()
    {
        numberOfTurns++;
    }

    public void NextPlayer()
    {
        playerTurn++;
        AddTurn();
        if (playerTurn >= numberOfPlayers)
        {
            playerTurn = 0;
        }
        Debug.Log("NextPlayer was called, player turn is " + playerTurn);
    }
}
