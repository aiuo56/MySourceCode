using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static int hour;
    public  static int minute;
    public static float seconds;
    public static  float oldSeconds;
    public static Text timerText;

    public Text clearTime;
     
    // Start is called before the first frame update
    void Start()
    {
        hour = 0;
        minute = 0;
        seconds = 0;
        oldSeconds = 0;
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if(seconds >= 60f)
        {
            minute++;
            seconds = seconds -60;
        }

        if(minute >= 60f)
        {
            hour++;
            minute = minute -60;
        }


        if((int)seconds != (int)oldSeconds)
        {
            timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }


        if(GameState.GameOver)
        {
            hour += 0;
            minute += 0;
            seconds += 0;
            clearTime.text =  hour.ToString("00") + ":" + minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            clearTime.enabled = true;
        }
    }
}
