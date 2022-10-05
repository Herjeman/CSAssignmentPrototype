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
    [SerializeField] private string _scene;

    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _controlScreen;
    [SerializeField] private GameObject _weaponScreen;

    private int _wormsPerPlayer;
    private int _numberOfPlayers;

    private void Awake()
    {
        _wormsPerPlayer = 1;
        _numberOfPlayers = 2;
        _instructions.Clear();
        _numberOfWormsText.SetText(_wormsPerPlayer.ToString());
        ColorSelector.SetColorList(_colors);
        ColorSelector.unavailableIndexes = new List<int>();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowTutorialScreen()
    {
        Debug.Log($"Starting game with {_numberOfPlayers} players");
        List<string> names = new List<string>();
        List<Color> colors = new List<Color>();

        for (int i = 0; i < _numberOfPlayers; i++)
        {
            NameInputField inputField = _playerInputLines[i].GetComponent<NameInputField>();
            names.Add(inputField.GetName());
            colors.Add(inputField.GetColor());
        }
        _instructions.wormsPerPlayer = _wormsPerPlayer;
        _instructions.playerNames = names;
        _instructions.colors = colors;

        _mainMenuScreen.SetActive(false);
        _controlScreen.SetActive(true);
    }

    public void NextTutorialScreen()
    {
        _controlScreen.SetActive(false);
        _weaponScreen.SetActive(true);
    }

    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(_scene);
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
