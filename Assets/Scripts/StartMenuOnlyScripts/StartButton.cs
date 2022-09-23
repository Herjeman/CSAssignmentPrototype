using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private MainMenu _mainMenu;
    private void Awake()
    {
        _mainMenu = GetComponentInParent<MainMenu>();
    }

    public void ButtonPushed()
    {
        _mainMenu.StartGame();
    }
}
