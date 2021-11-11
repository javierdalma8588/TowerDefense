using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    //if we enable this element we just stop everything by setting the time scale to 0
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    //when we disable this object the game is resumed by setting the timescale to 1
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    //function just to load the menu scene it thte player quits from the pause menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
