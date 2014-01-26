using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {
	public bool IsBlueUnlocked;
	public bool IsRedUnlocked;
	public bool IsYellowUnlocked;
	
	public eColor currentColor;

	private LevelManager m_Manager;
	private CameraBehaviour cameras;
	private PandaColor pandaColor;

	private ColorItem[] colors;
	
	// Use this for initialization
	void Start () {
		colors = gameObject.GetComponentsInChildren<ColorItem>();
		
		foreach (ColorItem curColor in colors)
		{
			curColor.SetManager(this);
		}
		
		m_Manager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
		cameras = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour> ();
		pandaColor = GameObject.FindGameObjectWithTag("Player").GetComponent<PandaColor> ();
	}

	void Update () {
		if (Input.GetButtonDown("BlueShift") &&
		    IsBlueUnlocked)
		{
			ColorShift (eColor.Blue);
		}
		
		if (Input.GetButtonDown("RedShift") &&
		    IsRedUnlocked)
		{
			ColorShift (eColor.Red);
		}
		
		if (Input.GetButtonDown("YellowShift") &&
		    IsYellowUnlocked)
		{
			ColorShift (eColor.Yellow);
		}
		
		if (Input.GetButtonDown("CycleColor"))
		{
			ColorShift (nextColor(currentColor));
		}
	}

	public void Reset()
	{
		IsBlueUnlocked = false;
		IsRedUnlocked = false;
		IsYellowUnlocked = false;

		ColorShift (eColor.White);

		foreach (ColorItem curColor in colors)
		{
			curColor.gameObject.SetActive(true);
		}
	}

	public void ColorObtained(eColor newColor)
	{
		switch (newColor)
		{
		case eColor.Blue:
			IsBlueUnlocked = true;
			ColorShift (newColor);
			break;
		case eColor.Red:
			IsRedUnlocked = true;
			ColorShift (newColor);
			break;
		case eColor.Yellow:
			IsYellowUnlocked = true;
			ColorShift (newColor);
			break;
		default:
			Debug.LogError("Color " + newColor.ToString() + " should not be collectible");
			break;
		}
	}

	public void ColorShift(eColor newColor)
	{
		if (currentColor == newColor)
			return;
		
		currentColor = newColor;
		
		m_Manager.SetColor(currentColor, IsRedUnlocked, IsBlueUnlocked, IsYellowUnlocked);
		eColor myNextColor = nextColor(currentColor);
		cameras.SetNewActiveColor (currentColor, myNextColor, nextColor(myNextColor));
		
		//TODO: When sprites are added, change this region to use sprites
		#region change player color
		
		pandaColor.ColorShift(currentColor);
		
		#endregion
	}

	eColor nextColor(eColor curColor)
	{
		if (!IsRedUnlocked
		    && !IsBlueUnlocked
		    && !IsYellowUnlocked)
		{
			return eColor.White;
		}
		
		switch(curColor)
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
}
