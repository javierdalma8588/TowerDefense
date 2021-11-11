using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//Singleton Variable
	public static GameManager _instance;

	[Header("List of Points")]
	public MovePoint[] points;

	public bool gameOver;

	//Singleton initialization
	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

    private void Update()
    {
    	//if user has no lives left the game ends
        if(Stats.lives <=0 && !gameOver)
        {
			GameOver();
        }
    }

    //enable game over ui
    public void GameOver()
    {
		gameOver = true;
		UIManager.instance.gameOverPanel.SetActive(true);
    }

    //enable you win ui
	public void Win()
	{
		//gameOver = true;
		UIManager.instance.winPanel.SetActive(true);
	}
}
