using UnityEngine;
using System.Collections;

public class PandaSpawner : MonoBehaviour {

	const int MAX_LIVES = 3;
	public int lives;

	GameObject levelRespawn = null;
	Vector3 lastCheckpoint;

	LevelManager levelManager;

	public void Start()
	{
		if (levelRespawn == null)
		{
			Transform[] levelkids = GameObject.FindGameObjectWithTag("Level").GetComponentsInChildren<Transform>();
			for (int i = 0; i < levelkids.Length; ++i)
				if (levelkids[i].name == "LevelSpawner")
				{
					levelRespawn = levelkids[i].gameObject;
					break;
				}
		}
		lastCheckpoint = levelRespawn.transform.position;
		transform.position = lastCheckpoint;
		lives = MAX_LIVES;

		levelManager = GameObject.FindGameObjectWithTag ("Level").GetComponent<LevelManager> ();
	}
	
	public void SetRespawn(Vector3 respawn) {
		lastCheckpoint = respawn;
	}

	public void Respawn()
	{
		if (lives > 1)
		{
			transform.position = lastCheckpoint;
			lives--;
		}
		else
		{
			lastCheckpoint = levelRespawn.transform.position;
			transform.position = lastCheckpoint;
			lives = MAX_LIVES;
			levelManager.Reset();
		}
	}

	public void SetLevelSpawn(GameObject newLevelSpawn)
	{
		levelRespawn = newLevelSpawn;
	}
}
