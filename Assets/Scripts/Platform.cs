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
	public Transform m_SnappedTransform;

	public bool m_PassThrough = false;

	public ColoredPlatform m_Platform;
	private ColoredPlatform[] m_WhitePlatforms;
	
	public bool m_ForceRestart = false;
	
	private float m_CurrentWidth  = -1f;
	private float m_CurrentHeight = -1f;
	private float m_SnapResolution = 0.5f;
	
	private BoxCollider2D m_Collider;

	private bool m_PassingThrough = false;
	private bool m_Unlocked = false;
	private bool m_IsColorActive = false;
	
	void Awake()
	{
		if (m_Platform == null)
		{
			Debug.LogError(string.Format("Platform prefab is not set for platform at {0}", transform.position));
			return;
		}

		if (m_SnappedTransform == null)
		{
			Debug.LogError(string.Format("Snap-transform is not set for platform at {0}", transform.position));
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
		    m_CurrentWidth  != Snap(m_Width) ||
		    m_CurrentHeight != Snap(m_Height) ||
		    transform.position.x - m_SnappedTransform.localPosition.x != Snap(transform.position.x) ||
		    transform.position.y - m_SnappedTransform.localPosition.y != Snap(transform.position.y))
		{
			m_Width  = Mathf.Max(m_Width,  MIN_DIMENSION);
			m_Height = Mathf.Max(m_Height, MIN_DIMENSION);
			Restart();
			m_ForceRestart = false;
		}
	}
	
	private void Restart()
	{
		m_CurrentWidth = Snap(m_Width);
		m_CurrentHeight = Snap(m_Height);
		
		Vector2 newPos = Vector2.zero;
		newPos.x = -(Snap(transform.position.x) - transform.position.x);
		newPos.y = -(Snap(transform.position.y) - transform.position.y);
		m_SnappedTransform.localPosition = new Vector3(newPos.x, newPos.y);

		if (m_ForceRestart)
			m_Platform.ForceRestart();
		for (int i = 0; i < m_WhitePlatforms.Length; ++i)
			m_WhitePlatforms[i].SetSize(m_CurrentHeight, m_CurrentWidth);
		
		m_Collider.size = new Vector2(m_CurrentWidth, m_CurrentHeight);
		m_Collider.center = new Vector2(m_CurrentWidth / 2f, -m_CurrentHeight / 2f) + newPos;
		
		SetActiveColor(m_Color);
	}

	public void PassThrough()
	{
		m_PassingThrough = true;
		UpdateCollider();
	}

	public void PassThroughDone()
	{
		m_PassingThrough = false;
		UpdateCollider();
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
		m_IsColorActive = m_Color == eColor.White || m_Color == newColor;
		UpdateCollider();
	}

	private void UpdateCollider()
	{
		m_Collider.enabled = !m_PassingThrough && m_Unlocked && m_IsColorActive;
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

	public void SetSnapResolution(float newResolution)
	{
		if (newResolution > 0f)
			m_SnapResolution = newResolution;
	}

	private float Snap(float input)
	{
		return m_SnapResolution * Mathf.Round(input / m_SnapResolution);
	}
}
