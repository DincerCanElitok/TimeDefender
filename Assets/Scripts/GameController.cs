using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Spawner spawner1;
    [SerializeField] private Spawner spawner2;
    [SerializeField] private Spawner spawner3;

    [SerializeField] private TextMeshProUGUI timeText;


    [SerializeField] private float timeForNextUpdate;
    [SerializeField] private float timeForGameOver;
    public float currentTime;
    private bool isTimeStopped;


    private void Start()
    {
        currentTime = timeForNextUpdate;
        isTimeStopped = false;
    }
    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeTime();
             
        }
            

        currentTime -= Time.deltaTime;
        timeText.text = currentTime.ToString("F1");

        //show upgrade panel
        if (currentTime < 0.1f && !isTimeStopped)
        {
            ChangeTime();      
        }

        //show gameover panel
        if(currentTime > timeForGameOver)
        {
            ChangeTime();
        }
            
    }
    public void ChangeTime()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            isTimeStopped = false;
            currentTime = timeForNextUpdate;
        }         
        else
        {
            Time.timeScale = 0f;
            isTimeStopped = true;
        }           
    }

}
