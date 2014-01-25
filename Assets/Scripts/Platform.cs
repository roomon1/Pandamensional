using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	public string m_TextureName;

	public PlatformColor.Colors m_Color;

	public float m_Height = 1.0f;
	public float m_Width  = 1.0f;

	private Sprite[] m_PlatformSprites;

	public Color ActiveColor { get { return PlatformColor.ToColor(m_Color); } }

	public float Height { get { return m_Height; } }
	public float Width  { get { return m_Width;  } }

	// Use this for initialization
	void Start ()
	{
		if (m_TextureName == null)
		{
			Debug.LogError ("No texture for platform at " + gameObject.transform.position);
			return;
		}
		m_PlatformSprites = Resources.LoadAll<Sprite> (m_TextureName);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
