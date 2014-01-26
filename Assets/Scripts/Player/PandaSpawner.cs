using UnityEngine;
using System.Collections;

public class PandaSpawner : MonoBehaviour {
	const int MAX_LIVES = 3;
	public int lives;

	GameObject levelRespawn;
	Vector3 lastCheckpoint;

	public void Start()
	{
		lastCheckpoint = levelRespawn.transform.position;
		transform.position = lastCheckpoint;
		lives = MAX_LIVES;
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
		}
	}

	public void SetLevelSpawn(GameObject newLevelSpawn)
	{
		levelRespawn = newLevelSpawn;
	}
}
