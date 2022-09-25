using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LoadInstructionsScriptableObject _instructions;
    [SerializeField] private List<GameObject> _playerInputLines;
    [SerializeField] private List<Color> _colors;
    [SerializeField] private TMP_Text _numberOfWormsText;

    private int _wormsPerPlayer;
    private int _numberOfPlayers;

    private void Awake()
    {
        _wormsPerPlayer = 1;
        _numberOfPlayers = 2;
        _instructions.Clear();
        _numberOfWormsText.SetText(_wormsPerPlayer.ToString());
    }

    public void StartGame()
    {
        Debug.Log($"Starting game with {_numberOfPlayers} players");
        List<string> names = new List<string>();
        List<Color> colors = new List<Color>();

        for (int i = 0; i < _numberOfPlayers; i++)
        {
            names.Add(_playerInputLines[i].GetComponent<NameInputField>().GetName());
            colors.Add(_colors[i]);
        }
        _instructions.wormsPerPlayer = _wormsPerPlayer;
        _instructions.playerNames = names;
        _instructions.colors = colors;
        SceneManager.LoadScene("Level");
    }

    public void AddNewPlayer()
    {
        _numberOfPlayers++;
    }

    public void MoreWorms()
    {
        _wormsPerPlayer++;
        if (_wormsPerPlayer > 4)
        {
            _wormsPerPlayer = 4;
        }
        _numberOfWormsText.SetText(_wormsPerPlayer.ToString());
    }

    public void LessWorms()
    {
        _wormsPerPlayer--;
        if (_wormsPerPlayer < 1)
        {
            _wormsPerPlayer = 1;
        }
        _numberOfWormsText.SetText(_wormsPerPlayer.ToString());
    }
}
