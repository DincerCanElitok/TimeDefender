using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI roundTextGameOver;
    private int roundCount = 0;
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
            ShowUpgradePanel();
        }

        //show gameover panel
        if(currentTime > timeForGameOver)
        {
            ChangeTime();
            ShowGameOverPanel();
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
    public void ShowUpgradePanel()
    {
        roundCount += 1;
        roundText.text = "Round :  " + roundCount.ToString();
        upgradePanel.SetActive(true);
    }
    public void ShowGameOverPanel()
    {
        roundTextGameOver.text = "Reached Round :  " + roundCount.ToString();
        gameOverPanel.SetActive(true);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
