using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LoadInstructionsScriptableObject _instructions;
    [SerializeField] private List<GameObject> _playerInputLines;
    private int _wormsPerPlayer;
    private int _numberOfPlayers;

    private void Awake()
    {
        _wormsPerPlayer = 2;
        _numberOfPlayers = 1;
        _instructions.Clear();
    }

    public void StartGame()
    {
        Debug.Log($"Starting game with {_numberOfPlayers} players");
        List<string> names = new List<string>();

        for (int i = 0; i < _numberOfPlayers; i++)
        {
            names.Add(_playerInputLines[i].GetComponent<NameInputField>().GetName());
            Debug.Log(i);
        }
        _instructions.wormsPerPlayer = _wormsPerPlayer;
        _instructions.playerNames = names;
        SceneManager.LoadScene("Level");
    }

    public void AddNewPlayer()
    {
        _numberOfPlayers++;
    }
}
