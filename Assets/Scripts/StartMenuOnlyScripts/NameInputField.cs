using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour
{
    private MainMenu _mainMenu;
    private TMP_InputField _field;
    [SerializeField] private Image _colorIndicator;
    private string _name;
    private ColorSelector _colorSelector;
    private Color _selectedColor;

    private void Awake()
    {
        _mainMenu = GetComponentInParent<MainMenu>();
        _field = GetComponent<TMP_InputField>();
        _name = _field.text;
    }

    private void OnEnable()
    {
        _colorSelector = new ColorSelector();
        _colorIndicator.color = _colorSelector.GetColor();
        _selectedColor = _colorSelector.GetColor();
    }

    public void ChangeColor()
    {
        _selectedColor = _colorSelector.NextColor();
        _colorIndicator.color = _selectedColor;
    }

    public void ChangeName(string text)
    {
        _name = text;
    }

    public string GetName()
    {
        return _name;
    }

    public Color GetColor()
    {
        return _selectedColor;
    }
}
