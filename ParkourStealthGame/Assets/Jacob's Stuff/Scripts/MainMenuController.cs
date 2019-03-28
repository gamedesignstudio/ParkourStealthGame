using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Object SceneToLoad;


    public void PlayGame()
    {
        Debug.Log("Opening Scene: " + SceneToLoad);
        SceneManager.LoadScene(SceneToLoad.name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
