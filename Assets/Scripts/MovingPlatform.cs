using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MovingPlatform : Platform
{
	public Vector3[] m_Points;

	public float m_Speed = 1f;

	private int m_NextPoint = -1;
	private Vector3 m_CurrentDirection;

	private float m_LastPointTime;

	public void Awake()
	{
		Debug.Log(m_Points.Length);
	}

	public void Start()
	{
		if (!Application.isPlaying)
			return;

		NextDirection();
	}

	public new void Update()
	{
		base.Update();
		if (!Application.isPlaying)
		{
			if (m_Points == null || m_Points.Length < 1)
				m_Points = new Vector3[1];
			if (m_Points[0] != transform.localPosition)
			{
				m_Points[0] = transform.localPosition;
#if UNITY_EDITOR
				UnityEditor.EditorUtility.SetDirty(this);
#endif
			}
			return;
		}

		if (m_Points.Length == 1)
			transform.localPosition = m_Points [0];
		else
		{
			float distanceTravelled = Time.deltaTime * m_Speed;
			float distanceRemaining = ToNextPoint().magnitude;

			while (distanceTravelled > distanceRemaining)
			{
				distanceTravelled -= distanceRemaining;
				transform.localPosition = m_Points[m_NextPoint];
				NextDirection ();
				distanceRemaining = ToNextPoint().magnitude;
			}

			transform.localPosition = Vector3.Lerp(transform.localPosition, m_Points[m_NextPoint], distanceTravelled / distanceRemaining);
		}
	}

	private void NextDirection()
	{
		m_NextPoint = (m_NextPoint + 1) % m_Points.Length;
		m_CurrentDirection = ToNextPoint().normalized;
	}
	
	private Vector3 ToNextPoint()
	{
		return m_Points[m_NextPoint] - transform.localPosition;
	}
}
