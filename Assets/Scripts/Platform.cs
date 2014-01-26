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
	
	public ColoredPlatform m_Platform;
	
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
		m_Platform.SetSize(m_CurrentHeight, m_CurrentWidth);
		
		m_Collider.size = new Vector2(m_CurrentWidth, m_CurrentHeight);
		m_Collider.center = new Vector2(m_CurrentWidth / 2f, -m_CurrentHeight / 2f);
		
		SetColor(m_Color);
	}

	public void SetUnlocked(bool unlocked)
	{
		m_Unlocked = unlocked;
	}

	public void SetActiveColor(eColor newColor)
	{
		if (m_Color == eColor.White ||
		    !m_Unlocked ||
		    m_Color == newColor)
			SetColor(m_Color);
		m_Collider.enabled = m_Unlocked && (m_Color == eColor.White || m_Color == newColor);
	}

	public void SetColor(eColor color)
	{
		if (!m_Unlocked)
			color = eColor.Black;

		int ind = 0;
		for (ind = 0; ind < PlatformColor.eColorList.Length && PlatformColor.eColorList[ind] != color; ++ind);
		
		int mask = LayerMask.NameToLayer(PlatformColor.NameList[ind]);

		m_Platform.SetColor(PlatformColor.ColorList[ind]);
		m_Platform.transform.name = PlatformColor.NameList[ind];
		
		Queue<Transform> children = new Queue<Transform>();
		children.Enqueue(m_Platform.transform);
		
		while (children.Count > 0)
		{
			Transform t = children.Dequeue();
			t.gameObject.layer = mask;
			foreach (Transform child in t)
				children.Enqueue(child);
		}
	}
}
