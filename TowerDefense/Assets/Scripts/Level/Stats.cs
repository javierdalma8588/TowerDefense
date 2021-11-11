using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //stats for each level

    //how much cash the user will start the level
    public static int cash;
    public int startCash = 400;

    //how many lives will the player have on this level
    public static int lives;
    public int startlives = 10;

    private void Start()
    {
        //as the cash and lives are static we set them to the proper amount otherwise they will get carried to the next level
        cash = startCash;
        lives = startlives;

        //set the ui elements
        UIManager.instance.cashText.text = "$" +cash.ToString();
        UIManager.instance.livesText.text = "Lives: " + lives.ToString();
    }
}
