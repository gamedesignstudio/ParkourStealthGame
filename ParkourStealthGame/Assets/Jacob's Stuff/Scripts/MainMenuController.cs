using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Scene SceneToLoad;
    [SerializeField] private Scene LeaderboardScene;

    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Opening Scene: " + SceneToLoad.name);
        SceneManager.LoadScene(SceneToLoad.handle);
    }

    public void LeaderboardMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Opening Scene: " + LeaderboardScene.name);
        SceneManager.LoadScene(LeaderboardScene.handle);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
