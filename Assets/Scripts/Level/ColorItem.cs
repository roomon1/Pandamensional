using UnityEngine;
using System.Collections;

public class ColorItem : MonoBehaviour {
	ColorManager manager;
	public eColor color;
	
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
			manager.ColorObtained(color);
		}
		gameObject.SetActive(false);
	}
	
	public void SetManager(ColorManager newManager)
	{
		manager = newManager;
	}
}
