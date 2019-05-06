using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlController : MonoBehaviour
{
    [SerializeField] private Scene sceneToLoad;

    //Return to Main Menu
    public void LoadMainMenu() {
        Debug.Log("Opening Scene: " + sceneToLoad.name);
        SceneManager.LoadScene(sceneToLoad.handle);
    }
}
