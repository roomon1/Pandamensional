using UnityEngine;
using System.Collections;

public class NewGameAction : MonoBehaviour, IButtonAction{
	GameObject mainMenuButtons;
	GameObject levelButtons;

	// Use this for initialization
	void Start () {
		mainMenuButtons = GameObject.Find("MainMenuButtons");
		levelButtons = GameObject.Find ("LevelButtons");

	}

	public void Activate ()
	{
		levelButtons.SetActive(true);
		mainMenuButtons.SetActive(false);
	}
}
