using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardController : MonoBehaviour
{

    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    
    private void Awake()
    {
        //entryContainer = transform.Find("LeaderboardEntriesContainer");
        //entryTemplate = entryContainer.Find("LeaderboardEntries");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighscoreEntry>() {
            new HighscoreEntry{time = "00:12.24", name = "ABC"},
            new HighscoreEntry{time = "01:34.20", name = "ABC"},
            new HighscoreEntry{time = "00:12.24", name = "BOI"},
            new HighscoreEntry{time = "00:12.24", name = "ABC"},
            new HighscoreEntry{time = "08:88.88", name = "ABC"},
            new HighscoreEntry{time = "00:12.24", name = "ABC"},
            new HighscoreEntry{time = "00:12.24", name = "YOU"}
        };

        highscoreEntryTransformList = new List<Transform>();

        foreach(HighscoreEntry highscoreEntry in highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
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
        string time = highscoreEntry.time;
        entryTransform.Find("TIME entries").GetComponent<TextMeshProUGUI>().text = time.ToString();

        transformList.Add(entryTransform);
    }




    /*
     * Represents single entry
     */
    private class HighscoreEntry
    {
        public string time;
        public string name;
    }
}
