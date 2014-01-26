using UnityEngine;
using System.Collections;
using System;

public class PandaColor : MonoBehaviour {
	public bool IsBlueUnlocked;
	public bool IsRedUnlocked;
	public bool IsYellowUnlocked;

	public eColor currentColor;

	private LevelManager m_Manager;

	//TODO: Put in public LevelColor here

	// Use this for initialization
	void Start () {
		currentColor = eColor.White;
		IsBlueUnlocked = false;
		IsYellowUnlocked = false;
		IsRedUnlocked = false;

		m_Manager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("BlueShift") 
		    && IsBlueUnlocked)
		{
			ColorShift (eColor.Blue);
		}

		if (Input.GetButtonDown("RedShift") 
		    && IsRedUnlocked)
		{
			ColorShift (eColor.Red);
		}

		if (Input.GetButtonDown("YellowShift") 
		    && IsYellowUnlocked)
		{
			ColorShift (eColor.Yellow);
		}

		if (Input.GetButtonDown("CycleColor"))
		{
			ColorShift (nextColor());
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;
		if (tag.Equals("RedTrigger"))
		{
			unlockColor (eColor.Red);
			other.gameObject.SetActive(false);
		}
		if (tag.Equals("BlueTrigger"))
		{
			unlockColor (eColor.Blue);
			other.gameObject.SetActive(false);
		}
		if (tag.Equals("YellowTrigger"))
		{
			unlockColor (eColor.Yellow);
			other.gameObject.SetActive(false);
		}
	}

	eColor nextColor()
	{
		if (!IsRedUnlocked
		    && !IsBlueUnlocked
		    && !IsYellowUnlocked)
		{
			return eColor.White;
		}

		switch(currentColor)
		{
		case eColor.Red:
			if (IsBlueUnlocked)
				return eColor.Blue;
			if (IsYellowUnlocked)
				return eColor.Yellow;
			return eColor.Red;
		case eColor.Blue:
			if (IsYellowUnlocked)
				return eColor.Yellow;
			if (IsRedUnlocked)
				return eColor.Red;
			return eColor.Blue;
		case eColor.Yellow:
			if (IsRedUnlocked)
				return eColor.Red;
			if (IsBlueUnlocked)
				return eColor.Blue;
			return eColor.Yellow;
		}

		return eColor.Black;
	}

	void unlockColor(eColor newColor)
	{
		switch (newColor)
		{
		case eColor.Blue:
			if (IsBlueUnlocked)
				Debug.Log("Unlocked blue when it was already unlocked");
			else
				IsBlueUnlocked = true;
			break;
		case eColor.Red:
			if (IsRedUnlocked)
				Debug.Log("Unlocked red when it was already unlocked");
			else
				IsRedUnlocked = true;
			break;
		case eColor.Yellow:
			if (IsYellowUnlocked)
				Debug.Log("Unlocked yellow when it was already unlocked");
			else
				IsYellowUnlocked = true;
			break;
		default:
			throw new Exception("Color " + newColor.ToString() + " is invalid");
		}
		
		ColorShift(newColor);
	}

	public void ColorShift(eColor newColor)
	{
		if (currentColor == newColor)
			return;

		currentColor = newColor;
		
		m_Manager.SetColor(newColor, IsRedUnlocked, IsBlueUnlocked, IsYellowUnlocked);
		
		//TODO: When sprites are added, change this region to use sprites
		#region change player color
		
		renderer.material.color = PlatformColor.ToColor(currentColor);
		
		#endregion
	}
}
