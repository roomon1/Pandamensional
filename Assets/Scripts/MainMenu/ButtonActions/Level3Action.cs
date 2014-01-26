using UnityEngine;
using System.Collections;

public class Level3Action : ButtonAction {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void Activate ()
	{
		Application.LoadLevel("Level3");
	}
}
