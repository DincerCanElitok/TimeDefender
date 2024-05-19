using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public List<Downgrade> downgrades = new List<Downgrade>();
    public List<Downgrade> currentDowngrades = new List<Downgrade>();
    [SerializeField] private TextMeshProUGUI downgradeNameText1;
    [SerializeField] private TextMeshProUGUI downgradeDescriptionText1;
    [SerializeField] private GameObject downgradeImage1;
    [SerializeField] private Button downgradeButton1;

    [SerializeField] private TextMeshProUGUI downgradeNameText2;
    [SerializeField] private TextMeshProUGUI downgradeDescriptionText2;
    [SerializeField] private GameObject downgradeImage2;
    [SerializeField] private Button downgradeButton2;

    [SerializeField] private TextMeshProUGUI downgradeNameText3;
    [SerializeField] private TextMeshProUGUI downgradeDescriptionText3;
    [SerializeField] private GameObject downgradeImage3;
    [SerializeField] private Button downgradeButton3;

    private void Start()
    {
        currentTime = timeForNextUpdate;
        isTimeStopped = false;

        downgradeButton1.onClick.AddListener(() => ApplyDowngrade(0));
        downgradeButton2.onClick.AddListener(() => ApplyDowngrade(1));
        downgradeButton3.onClick.AddListener(() => ApplyDowngrade(2));

        
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
        ShowRandomDowngrades();
        upgradePanel.SetActive(true);
    }
    public void ShowGameOverPanel()
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlaySFX("Over");
        roundTextGameOver.text = "Reached Round :  " + roundCount.ToString();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void LoadMainMenu()
    {
        AudioManager.instance.PlayMusic("Theme");
        SceneManager.LoadScene(0);
    }
    public void ShowRandomDowngrades()
    {
        currentDowngrades.Clear();
        for (int i = 0; i < 3; i++)
        {
            int random;
            do
            {
                random = Random.Range(0, downgrades.Count);
            } while (currentDowngrades.Contains(downgrades[random]));

            currentDowngrades.Add(downgrades[random]);
        }
        downgradeNameText1.text = currentDowngrades[0].downgradeName;
        downgradeDescriptionText1.text = currentDowngrades[0].description;
        if(currentDowngrades[0].image != null)
            downgradeImage1.GetComponent<Image>().sprite = currentDowngrades[0].image;

        downgradeNameText2.text = currentDowngrades[1].downgradeName;
        downgradeDescriptionText2.text = currentDowngrades[1].description;
        if (currentDowngrades[1].image != null)
            downgradeImage2.GetComponent<Image>().sprite = currentDowngrades[1].image;

        downgradeNameText3.text = currentDowngrades[2].downgradeName;
        downgradeDescriptionText3.text = currentDowngrades[2].description;
        if (currentDowngrades[2].image != null)
            downgradeImage3.GetComponent<Image>().sprite = currentDowngrades[2].image;
    }
    public void ApplyDowngrade(int index)
    {
        Downgrade selectedDowngrade = currentDowngrades[index];
        Debug.Log("Seçilen azaltma " + selectedDowngrade.downgradeName);

        if (selectedDowngrade.downgradeName == "LeftSpeedReduction")
        {
            spawner1.enemySpeedAmount *= 2f / 3f;
            Debug.Log("soldan hız kesildi");
        }
        else if (selectedDowngrade.downgradeName == "MiddleSpeedReduction")
        {
            spawner2.enemySpeedAmount *= 2f / 3f;
            Debug.Log("ortadan hız kesildi");
        }
        else if (selectedDowngrade.downgradeName == "RightSpeedReduction")
        {
            spawner3.enemySpeedAmount *= 2f / 3f;
            Debug.Log("sağdan hız kesildi");
        }
        else if (selectedDowngrade.downgradeName == "LeftWeakness")
        {
            spawner1.enemyTakeDamageAmount *= 1.25f;
            Debug.Log("soldakiler zayıfladı");
        }
        else if (selectedDowngrade.downgradeName == "MiddleWeakness")
        {
            spawner2.enemyTakeDamageAmount *= 1.25f;
            Debug.Log("ortadakiler zayıfladı");
        }
        else if (selectedDowngrade.downgradeName == "RightWeakness")
        {
            spawner3.enemyTakeDamageAmount *= 1.25f;
            Debug.Log("sağdakiler zayıfladı");
        }

        upgradePanel.SetActive(false);
        ChangeTime();
    }
    [System.Serializable]
    public class Downgrade
    {
        public string downgradeName;
        public string description;
        public Sprite image;
    }

}
