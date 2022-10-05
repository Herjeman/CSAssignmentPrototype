using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScreen : MonoBehaviour
{
    [SerializeField] private string _scene;

    public void Continue()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(_scene);
    }
}
