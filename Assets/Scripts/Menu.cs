using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public List<Button> buttons;
	public int selectedIndex;
	bool horizontalPressed;
	//public float horizontalAxis;

	// Use this for initialization
	void Start () {
		selectedIndex = 0;
		horizontalPressed = false;
	}

	// Update is called once per frame
	void Update () {

		handleMovement();
		handleButtons();
	}

	void handleMovement()
	{
		if (!horizontalPressed &&
		    Input.GetAxis("Horizontal") > 0.0f)
		{
			buttons[selectedIndex].IsSelected = false;
			selectedIndex++;
			if (selectedIndex > buttons.Count - 1)
				selectedIndex = 0;
			buttons[selectedIndex].IsSelected = true;
			horizontalPressed = true;
		}
		
		if (!horizontalPressed &&
		    Input.GetAxis("Horizontal") < 0.0f)
		{
			buttons[selectedIndex].IsSelected = false;
			selectedIndex--;
			if (selectedIndex < 0)
				selectedIndex = buttons.Count - 1;
			buttons[selectedIndex].IsSelected = true;
			horizontalPressed = true;
		}
		
		if (horizontalPressed &&
		    Input.GetAxis("Horizontal") == 0)
			horizontalPressed = false;
	}

	void handleButtons()
	{
	}
}
