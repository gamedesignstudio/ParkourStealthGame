using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Scene SceneToLoad;
    [SerializeField] private Scene LeaderboardScene;
    [SerializeField] private Button leaderBoardButton;

    private void Awake()
    {
        //Uncomment to delete Leaderboard Entries
        //PlayerPrefs.DeleteKey("LeaderboardEntries");

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(!PlayerPrefs.HasKey("LeaderboardEntries")) {
            leaderBoardButton.interactable = false;
        }
        else {
            leaderBoardButton.interactable = true;
        }
        Debug.Log("Scene Opened: " + SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("AddEntry", 1);
        PlayerPrefs.SetString("PlayerTimeString", "");
        SceneManager.LoadScene(SceneToLoad.handle);
    }

    public void LeaderboardMenu()
    {
        SceneManager.LoadScene(LeaderboardScene.handle);
        PlayerPrefs.SetInt("AddEntry", 0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}