using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformColor
{
	public enum eColor
	{
		White,
		Red,
		Yellow,
		Blue,
		Black
	}

	public static eColor[] eColorList = {eColor.White, eColor.Red, eColor.Yellow, eColor.Blue, eColor.Black};
	public static Color[] ColorList = {Color.white, Color.red, Color.yellow, Color.blue, Color.black};
	public static string[] NameList = {"White", "Red", "Yellow", "Blue", "Black"};

	public static UnityEngine.Color ToColor(eColor c)
	{
		for (int i = 0; i < eColorList.Length; ++i)
			if (eColorList[i] == c)
				return ColorList[i];

		return Color.gray;
	}
}