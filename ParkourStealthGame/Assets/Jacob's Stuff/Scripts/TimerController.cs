using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(finished) {
            return;
        }

        float timeBuffer = Time.time - startTime;
        PlayerPrefs.SetFloat("PlayerTimeFloat", timeBuffer);
        TimeFormatter();
    }

    public void TimeFormatter()
    {
        float t = PlayerPrefs.GetFloat("PlayerTimeFloat");

        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00.00");

        timerText.text = minutes + ":" + seconds;
        PlayerPrefs.SetString("PlayerTimeString", timerText.text);
    }

    public void Finish()
    {
        finished = true;
    }
}
