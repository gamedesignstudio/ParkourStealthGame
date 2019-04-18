using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject timerUI;

    [SerializeField] private Object SceneToLoad;

    // Update is called once per frame
    void Update()
    {
        CursorLock();

        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming Game...");
        pauseMenuUI.SetActive(false);
        timerUI.SetActive(true);
        Camera.main.GetComponent<PlayerLook>().enabled = true;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        pauseMenuUI.SetActive(true);
        timerUI.SetActive(false);
        Camera.main.GetComponent<PlayerLook>().enabled = false;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Exitting to Main Menu...");
        Debug.Log("Opening Scene: " + SceneToLoad);
        SceneManager.LoadScene(SceneToLoad.name);
    }

    public void CursorLock()
    {
        if(isPaused) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
