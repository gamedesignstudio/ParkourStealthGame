using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LeaderboardController : MonoBehaviour
{

    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private Scene sceneToLoad;
    private List<Transform> highscoreEntryTransformList;
    
    private void Awake()
    {
        //entryContainer = transform.Find("LeaderboardEntriesContainer");
        //entryTemplate = entryContainer.Find("LeaderboardEntries");

        entryTemplate.gameObject.SetActive(false);

        //Add new entry
        //AddHighscoreEntry(8.82f, "JSG");

        //Load Save data
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        
        //Sort the times
        for(int i = 0; i < highscores._highscoreEntryList.Count; i++) {
            for(int j = i + 1; j < highscores._highscoreEntryList.Count; j++) {
                if(highscores._highscoreEntryList[j].time < highscores._highscoreEntryList[i].time) {
                    //Swap Places
                    HighscoreEntry temp = highscores._highscoreEntryList[i];
                    highscores._highscoreEntryList[i] = highscores._highscoreEntryList[j];
                    highscores._highscoreEntryList[j] = temp;

                }
            }
        }
        
        highscoreEntryTransformList = new List<Transform>();

        foreach(HighscoreEntry highscoreEntry in highscores._highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
;    }

    private void Update()
    {
        //Exit Leaderboard Screen
        if(Input.GetMouseButtonDown(0)) {
            Time.timeScale = 1f;
            Debug.Log("Opening Scene: " + sceneToLoad.name);
            SceneManager.LoadScene(sceneToLoad.handle);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 70f;

        Transform entryTransform = Transform.Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryRectTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch(rank) {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }
        entryTransform.Find("RANK entries").GetComponent<TextMeshProUGUI>().text = rankString;

        string name = highscoreEntry.name;
        entryTransform.Find("NAME entries").GetComponent<TextMeshProUGUI>().text = name;

        //May need to change variable datatype to Time
        float time = highscoreEntry.time;
        entryTransform.Find("TIME entries").GetComponent<TextMeshProUGUI>().text = time.ToString();

        transformList.Add(entryTransform);
    }

    /*
     * Add entries to Leaderboard list
     */ 
    private void AddHighscoreEntry(float _time, string _name)
    {
        //Creates Entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { time = _time, name = _name };

        //Load Save data
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add entry to list
        highscores._highscoreEntryList.Add(highscoreEntry);

        //Save updated data
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    /*
     * Stores Highscore list for saving/loading
     */ 
    private class Highscores
    {
        public List<HighscoreEntry> _highscoreEntryList;
    }

    /*
     * Represents single entry
     */
     [System.Serializable]
    private class HighscoreEntry
    {
        public float time;
        public string name;
    }
}
