using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string _message;
    [SerializeField] private LoadInstructionsScriptableObject _instructions;

    private void Awake()
    {
        _instructions.players = new List<Player>();
        _instructions.message = "";
    }

    public string GetMessage() 
    { 
        return _message; 
    }

    public void SetMessage(string message)
    {
        _message = message;
        _instructions.message = message;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
}
