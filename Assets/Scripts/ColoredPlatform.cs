using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ColoredPlatform : MonoBehaviour
{
	private static int SPRITES_FOR_SIDE = 3;
	private static int SPRITES_FOR_PLATFORM = SPRITES_FOR_SIDE * SPRITES_FOR_SIDE;

	public float m_Height;
	public float m_Width;

	public Transform[] m_Sprites;

	public bool m_ForceRestart = false;

	private float m_CurrentHeight = -1f;
	private float m_CurrentWidth = -1f;

	private GameObject[,] m_ActiveSprites;

	private BoxCollider2D m_Collider;

	void Start ()
	{
		if (m_Sprites.Length < SPRITES_FOR_PLATFORM)
		{
			Debug.LogError(string.Format("Colored Platform at ({0}) does not have enough sprites.  {1} are required.", gameObject.transform.ToString(), SPRITES_FOR_PLATFORM));
			return;
		}

		for (int i = 0; i < SPRITES_FOR_PLATFORM; ++i)
		{
			if (m_Sprites[i] == null)
			{
				Debug.LogError(string.Format("Colored Platform at ({0}) is missing sprite number {1}.", gameObject.transform.ToString(), i));
				return;
			}
		}

		Restart();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_ForceRestart ||
		    m_CurrentWidth != m_Width ||
		    m_CurrentHeight != m_Height)
		{
			Restart();
			m_ForceRestart = false;
		}
	}

	void Restart()
	{
		m_Collider = GetComponent<BoxCollider2D>();

		if (m_ActiveSprites == null)
			m_ActiveSprites = new GameObject[SPRITES_FOR_SIDE, SPRITES_FOR_SIDE];

		int r, c;
		if (m_ForceRestart)
		{
			for (r = 0; r < SPRITES_FOR_SIDE; ++r)
			{
				for (c = 0; c < SPRITES_FOR_SIDE; ++c)
				{
					if (Application.isEditor)
						GameObject.DestroyImmediate(m_ActiveSprites[r, c]);
					else if (Application.isPlaying)
						GameObject.Destroy(m_ActiveSprites[r, c]);
					else
						Debug.LogWarning(string.Format("Unhandled application state during restart: E:{0} P:{1} W:{2} L:{3}", Application.isEditor, Application.isPlaying, Application.isWebPlayer, Application.isLoadingLevel));
					m_ActiveSprites[r, c] = null;
				}
			}
		}

		r = 0;
		c = 0;
		for (int i = 0; i < SPRITES_FOR_PLATFORM; ++i, ++c)
		{
			if (c >= SPRITES_FOR_SIDE)
			{
				++r;
				c -= SPRITES_FOR_SIDE;
			}
			if (m_ActiveSprites[r, c] == null)
			{
				m_ActiveSprites[r,c] = ((Transform)GameObject.Instantiate(m_Sprites[i])).gameObject;
				m_ActiveSprites[r,c].transform.parent = gameObject.transform;
			}
		}
		
		ArrangeSprites();

		m_Collider.size = new Vector2(m_Width, m_Height);
		m_CurrentHeight = m_Height;
		m_CurrentWidth = m_Width;
	}

	void ArrangeSprites()
	{
		for (int r = 0; r < SPRITES_FOR_SIDE; ++r)
		{
			float yPos = YForRow(r);
			for (int c = 0; c < SPRITES_FOR_SIDE; ++c)
			{
				float xPos = XForCol(c);
				Vector3 spritePos = new Vector3(xPos, yPos);
				m_ActiveSprites[r, c].transform.localPosition = spritePos;
				Vector3 scale = m_ActiveSprites[r, c].transform.localScale;
				scale.x = SpriteSizeToScale(SpriteWidth(c));
				scale.y = SpriteSizeToScale(SpriteHeight(r));
				m_ActiveSprites[r, c].transform.localScale = scale;
			}
		}
	}

	private float YForRow(int row)
	{
		return -ItemPos(row, m_Height);
	}

	private float XForCol(int col)
	{
		return ItemPos(col, m_Width);
	}

	private float ItemPos(int ind, float dimSize)
	{
		float pos = -dimSize / 2f;
		for (int r = 0; r < ind; ++r)
			pos += SpriteSize(r, dimSize);
		pos += SpriteSize(ind, dimSize) / 2f;
		return pos;
	}

	private float SpriteHeight(int row)
	{
		return SpriteSize(row, m_Height);
	}

	private float SpriteWidth(int col)
	{
		return SpriteSize(col, m_Width);
	}

	private float SpriteSize(int ind, float dimSize)
	{
		if (ind == SPRITES_FOR_SIDE / 2)
			return dimSize - ((SPRITES_FOR_SIDE - 1) / (float)SPRITES_FOR_SIDE);
		return 1f / SPRITES_FOR_SIDE;
	}

	private float SpriteSizeToScale(float size)
	{
		return size * SPRITES_FOR_SIDE;
	}
}
