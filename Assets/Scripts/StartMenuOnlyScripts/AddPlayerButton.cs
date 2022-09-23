using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerButton : MonoBehaviour
{
    [SerializeField] private GameObject _inputField;
    private MainMenu _mainMenu;
    private void Awake()
    {
        _mainMenu = GetComponentInParent<MainMenu>();
    }

    public void OnClick()
    {
        _mainMenu.AddNewPlayer();
        _inputField.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
