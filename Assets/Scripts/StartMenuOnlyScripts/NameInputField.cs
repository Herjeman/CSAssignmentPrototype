using TMPro;
using UnityEngine;

public class NameInputField: MonoBehaviour
{
    private MainMenu _mainMenu;
    private TMP_InputField _field;
    private string _name;

    private void Awake()
    {
        _mainMenu = GetComponentInParent<MainMenu>();
        _field = GetComponent<TMP_InputField>();
        _name = _field.text;
        Debug.Log(_field.text);
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
