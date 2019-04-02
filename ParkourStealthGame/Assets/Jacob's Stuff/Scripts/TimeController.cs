using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private int minutes;
    private int seconds;
    private int frames;

    // Start is called before the first frame update
    void Start()
    {
        minutes = 0;
        seconds = 0;
        frames = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountFrames();
        Debug.Log("Timer  " + minutes + ":" + seconds);
    }

    private void CountFrames()
    {
        if(frames == 50)
        {
            CountSeconds();
            frames = 0;
        }
        else
        {
            frames += 1;
        }
    }

    //count seconds
    private void CountSeconds()
    {
        //if 59 seconds, add 1 minute and reset seconds else continue counting
        if(seconds == 59)
        {
            CountMinute();
            seconds = 0;
        }
        else
        {
            seconds += 1;
        }
    }

    //count minutes
    private void CountMinute()
    {
        //if 1hr, end game, else continue counting
        if(minutes > 60)
        {
            //end game
            Debug.Log("Time over 1hr. end game");
        }
        else
        {
            minutes += 1;
        }

    }
}
