using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	//function just to change the scene on the main menu
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level"+level);
    }
}
