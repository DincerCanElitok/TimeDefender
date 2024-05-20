using DG.Tweening;
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

    [SerializeField] private GameObject defender2;
    [SerializeField] private GameObject defender3;
    [SerializeField] private TextMeshProUGUI infoText1;
    [SerializeField] private TextMeshProUGUI infoText2;
    private void Start()
    {
        currentTime = timeForNextUpdate;
        isTimeStopped = false;

        downgradeButton1.onClick.AddListener(() => ApplyDowngrade(0));
        downgradeButton2.onClick.AddListener(() => ApplyDowngrade(1));
        downgradeButton3.onClick.AddListener(() => ApplyDowngrade(2));

        infoText1.DOFade(1, 5).OnComplete(() =>
        {
            infoText1.DOFade(0, 5).OnComplete(() => { infoText1.gameObject.SetActive(false); });
        });
    }
    private void Update()
    {
            
        currentTime -= Time.deltaTime;
        timeText.text = currentTime.ToString("F1");

        if (currentTime < 0.1f && !isTimeStopped)
        {
            ChangeTime();
            ShowUpgradePanel();
        }

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

        //for push the player to use the drag mechanic
        if (roundCount == 1)
        {
            defender2.SetActive(true);
            infoText2.DOFade(1, 5).OnComplete(() =>
            {
                infoText2.DOFade(0, 5).OnComplete(() => { infoText2.gameObject.SetActive(false); });
            });
        }
        else if (roundCount == 2)
        {
            defender3.SetActive(true);

            //reducing the average spawn time
            spawner1.spawnTimeMax = 7f;
            spawner1.spawnTimeMin = 3f;

            spawner2.spawnTimeMax = 7f;
            spawner2.spawnTimeMin = 3f;

            spawner3.spawnTimeMax = 7f;
            spawner3.spawnTimeMin = 3f;
        }
        //Difficulty
        else if (roundCount > 2 && roundCount < 5)
        {        
            spawner1.enemyTakeDamageAmount -= 5f;
            spawner2.enemyTakeDamageAmount -= 5f;
            spawner3.enemyTakeDamageAmount -= 5f;

            spawner1.enemySpeedAmount += 0.25f;
            spawner2.enemySpeedAmount += 0.25f;
            spawner3.enemySpeedAmount += 0.25f;
        }
        else if (roundCount >= 5 && roundCount < 8)
        {
            spawner1.enemyTakeDamageAmount -= 10f;
            spawner2.enemyTakeDamageAmount -= 10f;
            spawner3.enemyTakeDamageAmount -= 10f;

            spawner1.enemySpeedAmount += 1f;
            spawner2.enemySpeedAmount += 1f;
            spawner3.enemySpeedAmount += 1f;

            if(roundCount == 5)
            {
                spawner1.spawnTimeMax = 5f;
                spawner1.spawnTimeMin = 3f;

                spawner2.spawnTimeMax = 5f;
                spawner2.spawnTimeMin = 3f;

                spawner3.spawnTimeMax = 5f;
                spawner3.spawnTimeMin = 3f;
            }
        }
        else if (roundCount >= 8)
        {
            spawner1.enemyTakeDamageAmount -= 20f;
            spawner2.enemyTakeDamageAmount -= 20f;
            spawner3.enemyTakeDamageAmount -= 20f;

            spawner1.enemySpeedAmount += 1.5f;
            spawner2.enemySpeedAmount += 1.5f;
            spawner3.enemySpeedAmount += 1.5f;
        }
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
        //Debug.Log("Seçilen azaltma " + selectedDowngrade.downgradeName);

        if (selectedDowngrade.downgradeName == "LeftSpeedReduction")
        {
            spawner1.enemySpeedAmount -=0.5f;
            //Debug.Log("soldan hız kesildi");    
            if (spawner1.enemySpeedAmount < 0.5f)
                spawner1.enemySpeedAmount = 0.25f;
        }
        else if (selectedDowngrade.downgradeName == "MiddleSpeedReduction")
        {
            spawner2.enemySpeedAmount -= 0.5f;
            //Debug.Log("ortadan hız kesildi");
            if (spawner2.enemySpeedAmount < 0.5f)
                spawner2.enemySpeedAmount = 0.25f;
        }
        else if (selectedDowngrade.downgradeName == "RightSpeedReduction")
        {
            spawner3.enemySpeedAmount -= 0.5f;
            //Debug.Log("sağdan hız kesildi");
            if (spawner3.enemySpeedAmount < 0.5f)
                spawner3.enemySpeedAmount = 0.25f;
        }
        else if (selectedDowngrade.downgradeName == "LeftWeakness")
        {
            spawner1.enemyTakeDamageAmount += 10f;
            //Debug.Log("soldakiler zayıfladı");
        }
        else if (selectedDowngrade.downgradeName == "MiddleWeakness")
        {
            spawner2.enemyTakeDamageAmount += 10f;
            //Debug.Log("ortadakiler zayıfladı");
        }
        else if (selectedDowngrade.downgradeName == "RightWeakness")
        {
            spawner3.enemyTakeDamageAmount += 10f;
            //Debug.Log("sağdakiler zayıfladı");
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
