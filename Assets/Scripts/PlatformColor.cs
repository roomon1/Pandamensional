using UnityEngine;
using System.Collections;

public class PlatformColor
{
	public enum Colors
	{
		White,
		Red,
		Yellow,
		Blue,
		Black
	}

	public static Color ToColor(Colors c)
	{
		switch (c)
		{
		case Colors.White:
			return Color.white;
		case Colors.Red:
			return Color.red;
		case Colors.Yellow:
			return Color.yellow;
		case Colors.Blue:
			return Color.blue;
		case Colors.Black:
			return Color.black;
			
		default:
			return Color.gray;
		}
	}
}
