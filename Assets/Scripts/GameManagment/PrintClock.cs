using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintClock : MonoBehaviour
{
    public Text clockText;
    public int sec;
    public int min;
    public int milSec;
    private void Start()
    {
        min = 0;
        sec = 01;
        milSec = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        milSec += 1;
        if (sec < 10)
        {
            clockText.text = min + ":0" + sec;
        }
        else
        {
            clockText.text = min + ":" + sec;
        }
        if (milSec == 60)
        {
            sec++;
            milSec = 0;
        }
        if (sec == 60)
        {
            min++;
            sec = 0;
        }

    }
}
