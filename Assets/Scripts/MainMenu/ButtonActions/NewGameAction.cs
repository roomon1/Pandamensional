using UnityEngine;
using System.Collections;

public class NewGameAction : ButtonAction{
	public GameObject mainMenuButtons;
	public GameObject levelButtons;

	public override void Activate ()
	{
		levelButtons.SetActive(true);
		mainMenuButtons.SetActive(false);
	}
}
