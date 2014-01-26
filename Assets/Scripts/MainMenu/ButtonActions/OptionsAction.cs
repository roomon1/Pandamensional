using UnityEngine;
using System.Collections;

public class OptionsAction : ButtonAction {
	public GameObject mainMenuButtons;
	public GameObject options;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void Activate ()
	{
		options.SetActive(true);
		mainMenuButtons.SetActive(false);
	}
}
