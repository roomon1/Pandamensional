using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{
	Platform m_Platform;

	public void Awake()
	{
		m_Platform = (Platform)target;
	}

	public void OnSceneGUI()
	{
		Vector3 pos = m_Platform.transform.position;

		float width2  = m_Platform.Width / 2f;
		float height2 = m_Platform.Height / 2f;
		Vector3[] rect = {new Vector3(pos.x - width2, pos.y - height2, pos.z),
			new Vector3(pos.x + width2, pos.y - height2, pos.z),
			new Vector3(pos.x + width2, pos.y + height2, pos.z),
			new Vector3(pos.x - width2, pos.y + height2, pos.z)};
		Handles.DrawSolidRectangleWithOutline(rect, m_Platform.ActiveColor, Color.black);
	}
}
