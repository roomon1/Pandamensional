using UnityEditor;
using UnityEngine;
using System.Collections;

//[CustomEditor(typeof(ColoredPlatform))]
public class ColoredPlatformEditor : Editor
{
	ColoredPlatform m_Cp;

	void Awake()
	{
		m_Cp = (ColoredPlatform)target;
	}

	//void OnInspectorGUI()
	//{
	//}

/*	void OnSceneGUI ()
	{
		Vector3 pos = m_Cp.transform.position;
		Handles.DrawLine(pos, pos + Vector3.one);
		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}*/
}
