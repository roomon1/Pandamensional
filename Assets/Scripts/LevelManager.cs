using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject LevelRespawn;
	public PandaSpawner pandaSpawner;

	void Awake()
	{
		pandaSpawner.SetLevelSpawn(LevelRespawn);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
