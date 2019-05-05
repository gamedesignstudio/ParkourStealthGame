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
    private HighscoreList highscores;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        //Debug.Log("Add Entry? " + PlayerPrefs.GetInt("AddEntry"));

        if(!PlayerPrefs.HasKey("LeaderboardEntries")) {
            //Convert highscoreList to Json file
            highscores = new HighscoreList { entries = new List<HighscoreEntry>() };
            string saveJsonEntryList = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("LeaderboardEntries", saveJsonEntryList);
            PlayerPrefs.Save();
            //Debug.Log("If statement read.");

        }
        else {
            //Load Save file
            string loadJsonEntryList = PlayerPrefs.GetString("LeaderboardEntries");
            highscores = JsonUtility.FromJson<HighscoreList>(loadJsonEntryList);
            //Debug.Log("Else Statement read");
        }

        //Add new entry

        if(PlayerPrefs.GetInt("AddEntry") == 1) {
            AddHighscoreEntry(PlayerPrefs.GetFloat("PlayerTimeFloat"),
                PlayerPrefs.GetString("PlayerTimeString"),
                PlayerPrefs.GetString("PlayerName"));
        }


        TimeSortedLeaderboard();
    }

    private void DestroyOldHighscoreEntryTransform(Transform container)
    {
        //Disable
        for(int i = 0; i < container.childCount; ++i) {
            container.GetChild(i).gameObject.SetActive(false);
        }

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 70f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryRectTransform.gameObject.SetActive(true);

        //Values for Rank
        int rank = transformList.Count + 1;
        string rankString;
        switch(rank) {
            case 1:
                rankString = rank + "ST";
                break;
            case 2:
                rankString = rank + "ND";
                break;
            case 3:
                rankString = rank + "RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }
        //Display in Leaderboard
        entryTransform.Find("RANK entries").GetComponent<TextMeshProUGUI>().text = rankString;

        //Values for Time
        float timeFloat = highscoreEntry.timeFloat;
        string timeString = highscoreEntry.timeString;
        //Display in Leaderboard
        entryTransform.Find("TIME entries").GetComponent<TextMeshProUGUI>().text = timeString;

        //Values for Name
        string playerName = highscoreEntry.playerName;
        entryTransform.Find("NAME entries").GetComponent<TextMeshProUGUI>().text = playerName;

        //Add to entry list
        transformList.Add(entryTransform);
    }

    //Used for json
    public class HighscoreList
    {
        public List<HighscoreEntry> entries;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public float timeFloat;
        public string timeString;
        public string playerName;
    }

    private void AddHighscoreEntry(float timeFloat, string timeString, string playerName)
    {
        //Setup Entry Values
        HighscoreEntry highscoreEntry = new HighscoreEntry {
            timeFloat = timeFloat,
            timeString = timeString,
            playerName = playerName
        };

        //Load Save file
        string loadJsonEntryList = PlayerPrefs.GetString("LeaderboardEntries");
        HighscoreList highscores = JsonUtility.FromJson<HighscoreList>(loadJsonEntryList);

        //Add new entry to list
        highscores.entries.Add(highscoreEntry);

        //Convert highscoreList to Json file
        string saveJsonEntryList = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("LeaderboardEntries", saveJsonEntryList);
        PlayerPrefs.Save();
    }

    private void TimeSortedLeaderboard()
    {
        //Load Save file
        string json = PlayerPrefs.GetString("LeaderboardEntries");
        highscores = JsonUtility.FromJson<HighscoreList>(json);

        //Sort highscoreEntryList...
        for(int i = 0; i < highscores.entries.Count; i++) {
            for(int j = i + 1; j < highscores.entries.Count; j++) {
                //...by timeFloat
                if(highscores.entries[j].timeFloat < highscores.entries[i].timeFloat) {
                    //Swap
                    HighscoreEntry temp = highscores.entries[i];
                    highscores.entries[i] = highscores.entries[j];
                    highscores.entries[j] = temp;

                }
            }
        }
        Debug.Log(PlayerPrefs.GetString("LeaderboardEntries"));
        //Destroy Old Leaderboard Entries
        for(int i = 0; i < 10; i++) {
            DestroyOldHighscoreEntryTransform(entryContainer);
        }

        //Display 10 highscoreEntries from HighscoreEntryList ^^^
        highscoreEntryTransformList = new List<Transform>();
        int length = 10;

        if(highscores.entries.Count <= 10) {
            length = highscores.entries.Count;
        }

        for(int i = 0; i < length; i++) {
            CreateHighscoreEntryTransform(highscores.entries[i], entryContainer, highscoreEntryTransformList);
        }
    }

    //Return to Main Menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Opening Scene: " + sceneToLoad.name);
        SceneManager.LoadScene(sceneToLoad.handle);
    }
}
