using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
