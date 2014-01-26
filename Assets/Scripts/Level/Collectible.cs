using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	CollectibleManager manager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			manager.CollectibleObtained();
		}
		gameObject.SetActive(false);
	}

	public void SetManager(CollectibleManager newManager)
	{
		manager = newManager;
	}
}
