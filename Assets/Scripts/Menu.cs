using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public List<Button> buttons;
	int selectedIndex;
	public bool horizontalPressed;

	// Use this for initialization
	void Start () {
		buttons = new List<Button>();
		selectedIndex = 0;
		horizontalPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!horizontalPressed &&
		    Input.GetAxis("Horizontal") > 1)
		{
			selectedIndex++;
			horizontalPressed = true;
		}

		if (!horizontalPressed &&
		    Input.GetAxis("Horizontal") < 1)
		{
			selectedIndex--;
			horizontalPressed = true;
		}

		if (horizontalPressed &&
		    Input.GetAxis("Horizontal") == 0)
			horizontalPressed = false;
	}
}
