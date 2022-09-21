using UnityEngine;
using UnityEngine.SceneManagement;

public class InputField: MonoBehaviour
{
    private MainMenu _mainMenu;

    private void Awake()
    {
        _mainMenu = GetComponentInParent<MainMenu>();
    }

    public void ChangeMessage(string msg)
    {
        _mainMenu.SetMessage(msg);
    }
}
