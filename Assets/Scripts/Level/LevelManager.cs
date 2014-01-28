using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour {
	public GameObject LevelRespawn;
	PandaSpawner pandaSpawner;

	public float m_PlatformSnap = 0.5f;
	private float m_CurrentPlatformSnap = 0.5f;

	private Platform[] m_WhitePlatforms;
	private Platform[] m_RedPlatforms;
	private Platform[] m_BluePlatforms;
	private Platform[] m_YellowPlatforms;

	void Awake()
	{
		if (!Application.isPlaying)
			return;

		if (pandaSpawner == null)
			pandaSpawner = GameObject.FindGameObjectWithTag("Player").GetComponent<PandaSpawner>();
		pandaSpawner.SetLevelSpawn(LevelRespawn);
	}

	// Use this for initialization
	void Start ()
	{
		Platform[] plats = GetComponentsInChildren<Platform>();
		int white, red, blue, yellow;
		white = red = blue = yellow = 0;
		for (int i = 0; i < plats.Length; ++i)
		{
			plats[i].SetSnapResolution(m_CurrentPlatformSnap);
			switch (plats[i].m_Color)
			{
			case eColor.White:
				++white;
				break;
			case eColor.Red:
				++red;
				break;
			case eColor.Blue:
				++blue;
				break;
			case eColor.Yellow:
				++yellow;
				break;
			}
		}

		m_WhitePlatforms  = new Platform[white];
		m_RedPlatforms    = new Platform[red];
		m_BluePlatforms   = new Platform[blue];
		m_YellowPlatforms = new Platform[yellow];
		white = red = blue = yellow = 0;
		for (int i = 0; i < plats.Length; ++i)
		{
			switch (plats[i].m_Color)
			{
			case eColor.White:
				m_WhitePlatforms[white++] = plats[i];
				break;
			case eColor.Red:
				m_RedPlatforms[red++] = plats[i];
				break;
			case eColor.Blue:
				m_BluePlatforms[blue++] = plats[i];
				break;
			case eColor.Yellow:
				m_YellowPlatforms[yellow++] = plats[i];
				break;
			}
		}
	}

	void Update () {
		if (Input.GetButtonDown("Cancel"))
		{
			Application.LoadLevel("MainMenu");
		}

		if (!Application.isPlaying)
			return;

		if (m_PlatformSnap != m_CurrentPlatformSnap)
		{
			m_CurrentPlatformSnap = m_PlatformSnap;
			for (int i = 0; i < m_WhitePlatforms.Length; ++i)
				m_WhitePlatforms[i].SetSnapResolution(m_CurrentPlatformSnap);
			for (int i = 0; i < m_RedPlatforms.Length; ++i)
				m_RedPlatforms[i].SetSnapResolution(m_CurrentPlatformSnap);
			for (int i = 0; i < m_BluePlatforms.Length; ++i)
				m_BluePlatforms[i].SetSnapResolution(m_CurrentPlatformSnap);
			for (int i = 0; i < m_YellowPlatforms.Length; ++i)
				m_YellowPlatforms[i].SetSnapResolution(m_CurrentPlatformSnap);
		}
	}

	public void SetColor(eColor newColor, bool redUnlocked, bool blueUnlocked, bool yellowUnlocked)
	{
		bool anyUnlocked = redUnlocked || blueUnlocked || yellowUnlocked;
		for (int i = 0; i < m_WhitePlatforms.Length; ++i)
			m_WhitePlatforms[i].SetActiveColor(newColor, anyUnlocked);

		for (int i = 0; i < m_RedPlatforms.Length; ++i)
		{
			m_RedPlatforms[i].SetUnlocked(redUnlocked);
			m_RedPlatforms[i].SetActiveColor(newColor, anyUnlocked);
		}
		for (int i = 0; i < m_BluePlatforms.Length; ++i)
		{
			m_BluePlatforms[i].SetUnlocked(blueUnlocked);
			m_BluePlatforms[i].SetActiveColor(newColor, anyUnlocked);
		}
		for (int i = 0; i < m_YellowPlatforms.Length; ++i)
		{
			m_YellowPlatforms[i].SetUnlocked(yellowUnlocked);
			m_YellowPlatforms[i].SetActiveColor(newColor, anyUnlocked);
		}
	}

	public void Reset()
	{
		ColorManager colorMan = gameObject.GetComponent<ColorManager>();
		CollectibleManager collectibleMan = gameObject.GetComponent<CollectibleManager>();

		colorMan.Reset();
		collectibleMan.Reset();
	}
}
