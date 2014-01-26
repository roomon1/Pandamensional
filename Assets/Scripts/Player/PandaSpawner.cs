using UnityEngine;
using System.Collections;

public class PandaSpawner : MonoBehaviour {

	const int MAX_LIVES = 3;
	public int lives;

	Vector3 levelRespawn;
	Vector3 lastCheckpoint;

	public void Start()
	{
		lastCheckpoint = levelRespawn;
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
			lastCheckpoint = levelRespawn;
			transform.position = lastCheckpoint;
			lives = MAX_LIVES;
			PandaColor color = gameObject.GetComponent<PandaColor>();
			color.Reset();
		}
	}

	public void SetLevelSpawn(GameObject newLevelSpawn)
	{
		levelRespawn = newLevelSpawn.transform.position;
	}
}
