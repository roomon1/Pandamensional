using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Platform : MonoBehaviour
{
	private static float MIN_DIMENSION = 1f;
	
	public eColor m_Color = eColor.White;
	
	public float m_Width  = MIN_DIMENSION;
	public float m_Height = MIN_DIMENSION;

	public bool m_PassThrough = false;

	public ColoredPlatform m_Platform;
	private ColoredPlatform[] m_WhitePlatforms;
	
	public bool m_ForceRestart = false;
	
	private float m_CurrentWidth  = -1f;
	private float m_CurrentHeight = -1f;
	
	private BoxCollider2D m_Collider;

	private bool m_Unlocked = false;
	
	void Awake()
	{
		if (m_Platform == null)
		{
			Debug.LogError(string.Format("Platform prefab is not set for platform at {0}", transform.position));
			return;
		}
		
		m_Platform.SetSize(m_CurrentHeight, m_CurrentWidth);
		
		m_Collider = GetComponent<BoxCollider2D>();

		SetUnlocked(m_Unlocked || m_Color == eColor.White);
		SetUnlocked(m_Unlocked || !Application.isPlaying);
	}
	
	void Start()
	{
		if (m_Color == eColor.White && Application.isPlaying)
		{
			m_WhitePlatforms = new ColoredPlatform[3];
			m_WhitePlatforms[0] = m_Platform;
			for (int  i = 1; i < m_WhitePlatforms.Length; ++i)
			{
				m_WhitePlatforms[i] = (ColoredPlatform)Instantiate(m_Platform);
				m_WhitePlatforms[i].transform.parent = m_Platform.transform.parent;
				m_WhitePlatforms[i].transform.localPosition = m_Platform.transform.localPosition;
			}
		}
		else
		{
			m_WhitePlatforms = new ColoredPlatform[1];
			m_WhitePlatforms[0] = m_Platform;
		}
		SetActiveColor(eColor.White);
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
	
	private void Restart()
	{
		m_CurrentWidth = m_Width;
		m_CurrentHeight = m_Height;
		
		if (m_ForceRestart)
			m_Platform.ForceRestart();
		for (int i = 0; i < m_WhitePlatforms.Length; ++i)
			m_WhitePlatforms[i].SetSize(m_CurrentHeight, m_CurrentWidth);
		
		m_Collider.size = new Vector2(m_CurrentWidth, m_CurrentHeight);
		m_Collider.center = new Vector2(m_CurrentWidth / 2f, -m_CurrentHeight / 2f);
		
		SetActiveColor(m_Color);
	}

	public void SetUnlocked(bool unlocked)
	{
		m_Unlocked = unlocked;
	}

	public void SetActiveColor(eColor newColor, bool anyColorUnlocked = false)
	{
		if (m_Color == eColor.White && Application.isPlaying)
		{
			if (anyColorUnlocked)
				SetColor(eColor.Red,    m_WhitePlatforms[0]);
			else
				SetColor(eColor.White,  m_WhitePlatforms[0]);
			SetColor(eColor.Blue,   m_WhitePlatforms[1]);
			SetColor(eColor.Yellow, m_WhitePlatforms[2]);
		}
		else if (m_Color == newColor)
			SetColor(newColor, m_Platform);
		m_Collider.enabled = m_Unlocked && (m_Color == eColor.White || m_Color == newColor);
	}

	private void SetColor(eColor newColor, ColoredPlatform platform)
	{
		if (!m_Unlocked)
			newColor = eColor.Black;

		int ind = 0;
		for (ind = 0; ind < PlatformColor.eColorList.Length && PlatformColor.eColorList[ind] != newColor; ++ind);
		int mind = 0;
		for (mind = 0; mind < PlatformColor.eColorList.Length && PlatformColor.eColorList[mind] != m_Color; ++mind);

		int mask = LayerMask.NameToLayer(PlatformColor.NameList[ind]);

		platform.SetColor (PlatformColor.ColorList[ind]);
		platform.transform.name = PlatformColor.NameList[ind];

		Queue<Transform> children = new Queue<Transform>();
		children.Enqueue(platform.transform);

		while (children.Count > 0)
		{
			Transform t = children.Dequeue ();
			t.gameObject.layer = mask;
			foreach (Transform child in t)
				children.Enqueue (child);
		}
	}
}
