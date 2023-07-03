using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Saha : MonoBehaviour
{
    public static Saha instance;


    //public TMPro.TextMeshProUGUI evSahibiGol_text;
    //public TMPro.TextMeshProUGUI deplasmanGol_text;
    public TMPro.TextMeshProUGUI time_text;
    public TMPro.TextMeshProUGUI score_text;
    public Image goalBar;
    public GameObject goalPanel;
    public GameObject failPanel;
    public GameObject winPanel;


    public int buildIndex;



    //public int evSahibiGol;
    //public int deplasmanGol;
    public int goalXP;
    //public int topXP;
    public float time;
    public int score;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        time_text.text = time.ToString();
    }


    private void Update()
    {

        goalBar.fillAmount = (float)score / (float)goalXP;

        if (score>=goalXP)
        {
            enemyDead();

        }

        time -= Time.deltaTime;
        time_text.text = Mathf.Round(time).ToString();

        if (time <= 0)
        {
            playerDead();
            Debug.Log("kaybettin");
        }

        //if (evSahibiGol>deplasmanGol)
        //{
        //    enemyDead();
        //}
    }

    public void ScoreArttr(int gelenScore)
    {
        score+=gelenScore;
        score_text.text = score.ToString();
    }

    public IEnumerator goalPaneli()
    {
                goalPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        goalPanel.SetActive(false);
    }

    public void playerDead()
    {
        Time.timeScale = 0;
        failPanel.SetActive(true);
    }

    public void enemyDead()
    {
        int saveIndex = PlayerPrefs.GetInt("saveIndex");

        if (buildIndex > saveIndex)
        {
            PlayerPrefs.SetInt("saveIndex", buildIndex);
        }
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }
    public void menuButton()
    {
        int saveIndex = PlayerPrefs.GetInt("saveIndex");

        if (buildIndex > saveIndex)
        {
            PlayerPrefs.SetInt("saveIndex", buildIndex + 1);
        }

        SceneManager.LoadScene(0);
    }


}
