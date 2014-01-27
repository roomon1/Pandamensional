using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public List<Button> buttons;
	public int selectedIndex;

	public GameObject MenuButtons;

	bool verticalPressed;
	//public float horizontalAxis;

	// Use this for initialization
	void Start () {
		selectedIndex = 0;
		verticalPressed = false;
	}

	// Update is called once per frame
	void Update () {

		handleMovement();
		handleButtons();
	}

	void handleMovement()
	{
		if (!verticalPressed &&
		    Input.GetAxis("Vertical") < 0.0f)
		{
			buttons[selectedIndex].IsSelected = false;
			selectedIndex++;
			if (selectedIndex > buttons.Count - 1)
				selectedIndex = 0;
			buttons[selectedIndex].IsSelected = true;
			verticalPressed = true;
		}
		
		if (!verticalPressed &&
		    Input.GetAxis("Vertical") > 0.0f)
		{
			buttons[selectedIndex].IsSelected = false;
			selectedIndex--;
			if (selectedIndex < 0)
				selectedIndex = buttons.Count - 1;
			buttons[selectedIndex].IsSelected = true;
			verticalPressed = true;
		}
		
		if (verticalPressed &&
		    Input.GetAxis("Vertical") == 0)
			verticalPressed = false;
	}

	void handleButtons()
	{
		if (Input.GetButtonDown("Confirm"))
		{
			buttons[selectedIndex].Activate();
		}

		if (Input.GetButtonDown("Cancel") &&
		    !gameObject.tag.Equals("MainMenu"))
		{
			MenuButtons.SetActive(true);		
			gameObject.SetActive(false);
		}
	}
}
