using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Platform : MonoBehaviour
{
	private static float MIN_DIMENSION = 1f;

	public PlatformColor.eColor m_Color = PlatformColor.eColor.White;

	public float m_Width  = MIN_DIMENSION;
	public float m_Height = MIN_DIMENSION;

	public Transform m_RedPlatform;
	public Transform m_YellowPlatform;
	public Transform m_BluePlatform;
	public Transform m_WhitePlatform;
	public Transform m_BlackPlatform;

	public bool m_ForceRestart = false;

	private ColoredPlatform[] m_Platforms;

	private float m_CurrentWidth  = -1f;
	private float m_CurrentHeight = -1f;

	private BoxCollider2D m_Collider;

	void Awake()
	{
		Transform[] wallPrefabs = {m_RedPlatform, m_YellowPlatform, m_BluePlatform, m_WhitePlatform, m_BlackPlatform};
		string[] wallColors = {"Red", "Yellow", "Blue", "White", "Black"};
		for (int i = 0; i < wallPrefabs.Length; ++i)
		{
			if (wallPrefabs[i] == null)
			{
				Debug.LogError(string.Format("{0} Platform prefab is not set for platform at {1}", wallColors[i], transform.position));
				return;
			}
		}

		m_Platforms = GetComponentsInChildren<ColoredPlatform>();

		if (m_Platforms.Length < wallPrefabs.Length)
		{
			Debug.LogError(string.Format("Platform at {0} only has {1} platforms; {2} are requried.", transform.position, m_Platforms.Length, wallPrefabs.Length));
			return;
		}

		for (int i = 0; i < m_Platforms.Length; ++i)
			m_Platforms[i].SetSize(m_CurrentHeight, m_CurrentWidth);

		m_Collider = GetComponent<BoxCollider2D>();
	}

	void Start()
	{
		Color[] colors = PlatformColor.AllColors();
		for (int i = 0; i < m_Platforms.Length; ++i)
		{
			m_Platforms[i].SetColor(colors [i]);
		}
		
		Restart();
	}

	public void Update()
	{
		if (m_ForceRestart ||
		    m_Width != m_CurrentWidth ||
			m_Height != m_CurrentHeight)
		{
			m_Width  = Mathf.Max(m_Width,  MIN_DIMENSION);
			m_Height = Mathf.Max(m_Height, MIN_DIMENSION);
			Restart();
			m_ForceRestart = false;
		}
	}

	public void Restart()
	{
		m_CurrentWidth = m_Width;
		m_CurrentHeight = m_Height;
		for (int i = 0; i < m_Platforms.Length; ++i)
		{
			if (m_ForceRestart)
				m_Platforms[i].ForceRestart();
			m_Platforms[i].SetSize(m_CurrentHeight, m_CurrentWidth);
		}

		m_Collider.size = new Vector2(m_CurrentWidth, m_CurrentHeight);
		m_Collider.center = new Vector2(m_CurrentWidth / 2f, -m_CurrentHeight / 2f);
	}
}
