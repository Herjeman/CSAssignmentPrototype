using UnityEngine;
using UnityEngine.SceneManagement;


//legacy
public class ContinueScreen : MonoBehaviour
{
    [SerializeField] private GameObject _weaponTutorial;
    [SerializeField] private string _scene;

    public void Continue()
    {

    }

    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(_scene);
    }
}
