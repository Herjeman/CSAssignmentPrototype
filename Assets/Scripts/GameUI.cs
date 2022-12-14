using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiTimer;
    [SerializeField] private GameObject _nextPlayerScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameOverTextObject;
    [SerializeField] private GameObject _controlsExplanation;
    [SerializeField] private GameObject _showControlsPrompt;
    [SerializeField] private GameObject _noAmmo;
    [SerializeField] private TextMeshProUGUI _nextPlayerText;


    public bool gameIsRunning = true;
    public string gameOverMessage;

    private TurnsManager _turnsManager;
    private TextMeshProUGUI _timerText;
    private TextMeshProUGUI _gameOverMessage;
    private static GameUI _instance;

    private void Awake()
    {
        _timerText = _uiTimer.GetComponent<TextMeshProUGUI>(); // serialize these
        _gameOverMessage = _gameOverTextObject.GetComponent<TextMeshProUGUI>();
        _turnsManager = TurnsManager.GetInstance();
        _instance = this;
        TurnsManager.OnTurnEnd += ShowNextPlayerScreen;
        TurnsManager.OnTurnStart += HideNextPlayerScreen;
    }

    public static GameUI GetInstance()
    {
        return _instance;
    }

    public void ToggleControlsExplanation()
    {
        if (_controlsExplanation.activeInHierarchy)
        {
            _controlsExplanation.SetActive(false);
            _showControlsPrompt.SetActive(true);
        }
        else
        {
            _controlsExplanation.SetActive(true);
            _showControlsPrompt.SetActive(false);
        }

    }

    private void HideNextPlayerScreen()
    {
        _nextPlayerScreen.SetActive(false);
    }

    private void ShowNextPlayerScreen()
    {
        if (gameIsRunning)
        {
            _nextPlayerText.text = _turnsManager.GetCurrentPlayer().playerName;
            _nextPlayerScreen.SetActive(true);   
        } 
    }

    public void GoToMainMenu()
    {
        if (!gameIsRunning)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
    
    public void ShowNoAmmo()
    {
        _noAmmo.SetActive(true);
    }

    public void HideNoAmmo()
    {
        _noAmmo.SetActive(false);
    }

    private void Update()
    {
        if (!gameIsRunning)
        {
            _uiTimer.SetActive(false);
            _gameOverScreen.SetActive(true);
            _gameOverMessage.SetText(gameOverMessage);
        }
        _timerText.SetText(TurnsManager.GetInstance().GetRemainingTurnTime().ToString());
    }

    private void OnDestroy()
    {
        TurnsManager.OnTurnEnd -= ShowNextPlayerScreen;
        TurnsManager.OnTurnStart -= HideNextPlayerScreen;
    }
}
