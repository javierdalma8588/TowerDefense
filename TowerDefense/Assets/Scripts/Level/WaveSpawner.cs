using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;

	public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	public GameManager gameManager;

	private int waveIndex = 0;

    private void Start()
    {
    	//set the enemies alive to 0 again otherwise if tha player restarts the level the wave spawner will still have the number
		EnemiesAlive = 0;
    }

    void Update()
	{
		//if we have enemies alive we dont do anything
		if (EnemiesAlive > 0)
		{
			return;
		}

		//if the amount of waves we spawn is the same as the amount of waves we have on the array
		if (waveIndex == waves.Length)
		{
			//and we still have lives
			if(Stats.lives > 0)
            {
            	//we win
				gameManager.Win();
			}else
            {
            	//we check this because the last enemy might be the one removing our last life
				gameManager.GameOver();
            }
            //disbale this gameobject
			this.enabled = false;
		}

		if (countdown <= 0f)
		{
			//call the coriutine to spawn a wave
			StartCoroutine(SpawnWave());
			//set the time between 1 wave and the other
			countdown = timeBetweenWaves;
			return;
		}

		//reduce countdown by delta time
		countdown -= Time.deltaTime;

		//clamp countodown between countodwn and cero
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

	}

	IEnumerator SpawnWave()
	{
		//set the wave according to the wave index
		Wave wave = waves[waveIndex];

		//set enemies alive to the amount of enemies on the current wave
		EnemiesAlive = wave.count;

		//spawn the amoun of enemies depending on the wave enemy count
		for (int i = 0; i < wave.count; i++)
		{
			//spawn enemy
			SpawnEnemy(wave.enemy);
			//wait between the spawned enemy and the newt enemy according to the wave enemy rate
			yield return new WaitForSeconds(1f / wave.rate);
		}

		//set the index to the next wave
		waveIndex++;
	}

	void SpawnEnemy(GameObject enemy)
	{
		//intsnatiate the enemy
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
}
