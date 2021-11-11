using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	[Header("Level Tiles")]
	public GameObject[] objectPrefabs;

	//Each number represents a prefab that will be instantiated 
	public int[,] grid = new int[,] {
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 5, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1},
		{ 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
		{ 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
		{ 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
		{ 1, 1, 1, 1, 1, 5, 0, 0, 0, 0, 5, 1, 1, 5, 3, 5},
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 2, 0, 0, 0, 5, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 5, 0, 0, 5, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 1, 1, 5, 0, 0, 5, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
	};

	void Awake()
	{
		BuildLevel();
	}

	void BuildLevel()
	{
		//We get the lenght of the array in two dimensions
		int gridSizeX = grid.GetLength(0);
		int gridSizeY = grid.GetLength(1);

		for (int i = 0; i < gridSizeY; i++)
		{

			for (int j = 0; j < gridSizeX; j++)
			{

				int objectType = grid[j, i];

				//We use the objectType as an index for our prefabs array.
				//If the objectType =0, we get the 0 element of the array (=emptySpace)
				//If the objectType =1, we get the 1 element of the array (=walls)
				GameObject objectPrefab = objectPrefabs[objectType];

				//We create a wall object, based on the variable Wallpreefab. 
				//yield return new WaitForSeconds(0.5f); we can enable this line to see a cool effect of the level building only set the function to be a Coroutine
				GameObject objectClone = Instantiate(objectPrefab, this.gameObject.transform);

				objectClone.transform.position = new Vector3(i, 0, j);
			}
		}
		SaveLevel();
	}

	//Function to save the level as a prefab
	void SaveLevel()
    {
		//Set the path to save the prefab
		string localPath = "Assets/Prefab/Levels/" + "Level.prefab";

		localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
		//Generate a prefab on the certain location and after a certain name
		PrefabUtility.SaveAsPrefabAssetAndConnect(this.gameObject, localPath, InteractionMode.UserAction);
    }
}

