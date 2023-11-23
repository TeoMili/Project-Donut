using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 300;
    public bool timerRunning = false;

    public float minutesRemaining;
    public float secondsRemaining;

    public TMP_Text timerText;

    void Start()
    {
        if(ManageScenes.Instance.difficulty == 2)
            timerRunning = true;
    }

    void Update()
    {

        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }

            else
            {
                Debug.Log("Game Over!");
                timeRemaining = 0;
                timerRunning = false;
            }

            DisplayTimer(timeRemaining);

        }
    }

    void DisplayTimer(float time)
    {
        time+=1;

        //calculate the minutes and seconds remaining

        minutesRemaining = Mathf.FloorToInt(time / 60);

        secondsRemaining = Mathf.FloorToInt(time % 60);

        //display the remaining time 
        timerText.text = string.Format("{0:00}:{1:00}", minutesRemaining, secondsRemaining);

        Debug.Log(minutesRemaining);
    }
}
