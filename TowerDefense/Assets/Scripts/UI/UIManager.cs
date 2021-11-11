using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Singleton
    public static UIManager instance;

    public Text cashText;
    public Text livesText;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    void Awake()
    {
        //Singleton initialization
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //reload the currnet scene
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //load the menu scene
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
