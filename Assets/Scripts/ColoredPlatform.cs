using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ColoredPlatform : MonoBehaviour
{
	private static int SPRITES_FOR_SIDE = 3;
	private static int SPRITES_FOR_PLATFORM = SPRITES_FOR_SIDE * SPRITES_FOR_SIDE;

	public Transform[] m_UsedSprites;

	private float m_CurrentHeight = -1f;
	private float m_CurrentWidth = -1f;
	
	private bool m_ForceRestart = false;

	private GameObject[,] m_ActiveSprites;

	private Color m_Color = Color.white;

	void Start ()
	{
		if (m_UsedSprites.Length < SPRITES_FOR_PLATFORM)
		{
			Debug.LogError(string.Format("Colored Platform at ({0}) does not have enough sprites.  {1} are required.", gameObject.transform.ToString(), SPRITES_FOR_PLATFORM));
			return;
		}

		Restart();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_ForceRestart)
		{
			Restart();
			m_ForceRestart = false;
		}
	}

	public void SetSize(float height, float width)
	{
		m_CurrentHeight = height;
		m_CurrentWidth = width;
		if (m_ActiveSprites != null)
			ArrangeSprites();
	}

	public void SetColor(Color newColor)
	{
		m_Color = newColor;
		if (m_ActiveSprites != null)
		{
			for (int r = 0; r < SPRITES_FOR_SIDE; ++r)
			{
				for (int c = 0; c < SPRITES_FOR_SIDE; ++c)
				{
					m_ActiveSprites[r, c].GetComponentInChildren<SpriteRenderer>().color = m_Color;
				}
			}
		}
	}

	public void ForceRestart()
	{
		m_ForceRestart = true;
	}

	private void Restart()
	{
		if (m_ActiveSprites == null)
			m_ActiveSprites = new GameObject[SPRITES_FOR_SIDE, SPRITES_FOR_SIDE];

		int r = 0;
		int c = 0;
		for (int i = 0; i < SPRITES_FOR_PLATFORM; ++i, ++c)
		{
			if (c >= SPRITES_FOR_SIDE)
			{
				++r;
				c -= SPRITES_FOR_SIDE;
			}
			m_ActiveSprites[r, c] = m_UsedSprites[i].gameObject;
			m_ActiveSprites[r, c].transform.parent = gameObject.transform;
		}

		SetColor(m_Color);
		ArrangeSprites();
	}

	private void ArrangeSprites()
	{
		for (int r = 0; r < SPRITES_FOR_SIDE; ++r)
		{
			float yPos = YForRow(r);
			for (int c = 0; c < SPRITES_FOR_SIDE; ++c)
			{
				if (m_ActiveSprites[r, c] == null)
					continue;
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
		return -ItemPos(row, m_CurrentHeight);
	}

	private float XForCol(int col)
	{
		return ItemPos(col, m_CurrentWidth);
	}

	private float ItemPos(int ind, float dimSize)
	{
		float pos = 0f;
		for (int r = 0; r < ind; ++r)
			pos += SpriteSize(r, dimSize);
		pos += SpriteSize(ind, dimSize) / 2f;
		return pos;
	}

	private float SpriteHeight(int row)
	{
		return SpriteSize(row, m_CurrentHeight);
	}

	private float SpriteWidth(int col)
	{
		return SpriteSize(col, m_CurrentWidth);
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

	private int SpriteNum(Transform sprite)
	{
		string name = sprite.GetComponent<SpriteRenderer>().sprite.name;
		int ind = name.LastIndexOf("_");
		string num = name.Substring(ind + 1);
		return System.Convert.ToInt32(num);
	}
}
