using UnityEngine;
using System.Collections;
using System;

public class PandaColor : MonoBehaviour {
	
	public void ColorShift(eColor newColor)
	{
		
		//TODO: When sprites are added, change this region to use sprites
		#region change player color
		
		renderer.material.color = PlatformColor.ToColor(newColor);
		
		#endregion
	}
}
