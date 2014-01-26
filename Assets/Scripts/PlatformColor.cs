using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eColor
{
	White,
	Red,
	Yellow,
	Blue,
	Black
}

public class PlatformColor
{

	public static Dictionary<eColor, Color> ColorDict;

	public static Color[] AllColors()
	{
		InitColorDict();
		Color[] colors = new Color[ColorDict.Values.Count];
		int i = 0;
		foreach (Color c in ColorDict.Values)
			colors[i++] = c;
		return colors;
	}

	public static UnityEngine.Color ToColor(eColor c)
	{
		InitColorDict();

		Color ret;
		if (!ColorDict.TryGetValue (c, out ret))
			return Color.gray;

		return ret;
	}

	public static void InitColorDict()
	{
		if (ColorDict != null)
			return;

		ColorDict = new Dictionary<eColor, Color>();
		ColorDict.Add(eColor.White,  Color.white);
		ColorDict.Add(eColor.Red,    Color.red);
		ColorDict.Add(eColor.Yellow, Color.yellow);
		ColorDict.Add(eColor.Blue,   Color.blue);
		ColorDict.Add(eColor.Black,  Color.black);
	}
}