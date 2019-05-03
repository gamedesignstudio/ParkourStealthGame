using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private InputField input;
    [SerializeField] private Scene sceneToLoad;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int finalScore;

    [SerializeField] private TextMeshProUGUI timeText;
    private string finalTime;

    private void Awake() {
        //Setup InputField Restrictions
        //input = GameObject.Find("InputField").GetComponent<InputField>();
        input.characterLimit = 3;
        input.ActivateInputField();

        //Setup Score and Time display
        finalScore = PlayerPrefs.GetInt("PlayerScore");
        scoreText.text = finalScore.ToString();

        finalTime = PlayerPrefs.GetString("PlayerTimeString");
        timeText.text = finalTime;
    }

    public void GetInput(string name) {
        name = input.text;
        name = name.ToUpper();
        PlayerPrefs.SetString("PlayerName", name);
        Debug.Log("Saved Name: " + PlayerPrefs.GetString("PlayerName"));
    }

    public void LoadScene() {
        Debug.Log("Opening Scene: " + sceneToLoad.name);
        SceneManager.LoadScene(sceneToLoad.handle);
        PlayerPrefs.SetInt("AddEntry", 1);
    }
}
