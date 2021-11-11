using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    public float speed = 5;
    public float startHealth = 100;
    private float health;

    public int cashPrice = 50;

    public Image healthBar;

    [HideInInspector]
    public Transform target;
    public int currentMovepoint = 0;

    private void Start()
    {
        health = startHealth;
        target = GameManager._instance.points[0].transform;
    }

    public void TakeDamage(float damage)
    {
    	//reduce life by damage amount
        startHealth -= damage;

        healthBar.fillAmount = startHealth/health;

       	//if the enemy has no more health left we...
        if(startHealth <=0)
        {
        	//Give the amount of cash to the user
            Stats.cash += cashPrice;
            //Update the amount of cash ui
            UIManager.instance.cashText.text = "$" + Stats.cash.ToString();
            //reduce the enemies alive from the wave spawner
            WaveSpawner.EnemiesAlive--;
            //destroy this object
            Destroy(gameObject);
        }


    }

    private void Update()
    {
        if (!GameManager._instance.gameOver)
        {
            //get the direction point by substracting the current position to the target position
            Vector3 direction = target.transform.position - transform.position;
            //move to the resulting direction
            //we normalized the vector to make sure this will always have the same fixed speed and the only variable controlling the speed will be the speed variable
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            //check if the distance is less than 0.2 then we reach our point and we set the next one
            if (Vector3.Distance(transform.position, target.position) <= 0.4)
            {
                if (currentMovepoint >= GameManager._instance.points.Length - 1)
                {
                    Stats.lives--;
                    WaveSpawner.EnemiesAlive--;
                    UIManager.instance.livesText.text = "Lives: " + Stats.lives.ToString();
                    Destroy(this.gameObject);
                    return;
                }

                currentMovepoint++;
                target = GameManager._instance.points[currentMovepoint].transform;
            }
        }
    }
}
