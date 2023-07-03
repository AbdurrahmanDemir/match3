using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public void Retrybutton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevelButton()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //public void menuButton()
    //{
    //    int saveIndex = PlayerPrefs.GetInt("saveIndex");

    //    if (buildIndex > saveIndex)
    //    {
    //        PlayerPrefs.SetInt("saveIndex", buildIndex + 1);
    //    }

    //    SceneManager.LoadScene(0);
    //}
    public void level1()
    {
        SceneManager.LoadScene(1);
    }

    public void level2()
    {
        SceneManager.LoadScene(2);
    }

    public void level3()
    {
        SceneManager.LoadScene(3);
    }

    public void level4()
    {
        SceneManager.LoadScene(4);
    }

    public void level5()
    {
        SceneManager.LoadScene(5);
    }

    public void level6()
    {
        SceneManager.LoadScene(6);
    }
    public void level7()
    {
        SceneManager.LoadScene(7);
    }
    public void level8()
    {
        SceneManager.LoadScene(8);


    }

    public void level9()
    {
        SceneManager.LoadScene(9);
    }
    public void level10()
    {
        SceneManager.LoadScene(10);
    }
    public void level11()
    {
        SceneManager.LoadScene(11);
    }
    public void level12()
    {
        SceneManager.LoadScene(12);
    }

    public void level13()
    {
        SceneManager.LoadScene(13);
    }
    public void level14()
    {
        SceneManager.LoadScene(14);
    }
    public void level15()
    {
        SceneManager.LoadScene(15);
    }
    public void level16()
    {
        SceneManager.LoadScene(16);
    }
    public void level17()
    {
        SceneManager.LoadScene(17);
    }
    public void level18()
    {
        SceneManager.LoadScene(18);
    }

    public void level19()
    {
        SceneManager.LoadScene(19);
    }
    public void level20()
    {
        SceneManager.LoadScene(20);
    }
}
