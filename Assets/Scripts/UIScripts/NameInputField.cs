using UnityEngine;

public class NameInputField: MonoBehaviour
{
    private MainMenu _mainMenu;
    private string _name;

    private void Awake()
    {
        _mainMenu = GetComponentInParent<MainMenu>();
    }

    public void ChangeName(string text)
    {
        _name = text;
    }

    public string GetName()
    {
        return _name;
    }
}
