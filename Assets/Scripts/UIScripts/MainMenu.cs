using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string _message;

    public string GetMessage() 
    { 
        return _message; 
    }

    public void SetMessage(string message)
    {
        _message = message;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(_message);
        }
    }

}
