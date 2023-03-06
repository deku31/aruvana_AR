using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeConvress : MonoBehaviour
{
    public TMP_InputField timeText;
    public TMP_InputField timeTextOutput;
    DateTime currentTime;
    private void Start()
    {
        InvokeRepeating("UpdateTime", 0f, 1f);
        currentTime = DateTime.Now;
        timeText.text = currentTime.ToString("hh:mm:ss tt");
    }

    private void UpdateTime()
    {
        
        string timeString = TimeConversion(timeText.text);
        timeTextOutput.text = timeString;
    }

    private string TimeConversion(string s)
    {
        string time = s.Substring(0, 8);
        string period = s.Substring(9, 2);
        string[] timeArray = time.Split(':');

        if (period == "AM")
        {
            if (timeArray[0] == "12")
            {
                timeArray[0] = "00";
            }
        }
        else if (period == "PM")
        {
            if (timeArray[0] != "12")
            {
                int hour = int.Parse(timeArray[0]);
                hour += 12;
                timeArray[0] = hour.ToString();
            }
        }

        return string.Join(":", timeArray);
    }
}
