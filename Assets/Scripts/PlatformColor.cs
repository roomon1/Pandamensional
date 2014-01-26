using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eColor
{
	White,
	Red,
	Blue,
	Yellow,
	Black
}

public class PlatformColor
{
	public static eColor[] eColorList = {eColor.White, eColor.Red, eColor.Blue, eColor.Yellow, eColor.Black};
	public static Color[] ColorList = {Color.white, Color.red, Color.blue, Color.yellow, Color.black};
	public static string[] NameList = {"White", "Red", "Blue", "Yellow", "Black"};
	
	public static UnityEngine.Color ToColor(eColor c)
	{
		for (int i = 0; i < eColorList.Length; ++i)
			if (eColorList[i] == c)
				return ColorList[i];
		
		return Color.gray;
	}

	public static string ToString(eColor c)
	{
		for (int i = 0; i < eColorList.Length; ++i)
			if (eColorList[i] == c)
				return NameList[i];
		
		return "White";
	}
}