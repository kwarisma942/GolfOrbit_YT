using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldElement : MonoBehaviour
{
	private static List<WorldElement> deactivatedElements = new List<WorldElement>();
	public enum WorldElementType
	{
		Fair,
		Bunker,
		Water,
		Obstacle,
		Green,
		Hole,
		Bird
	}

	[SerializeField] private GameObject m_destructionFxPrefab;
	[SerializeField] private WorldElementType m_worldElementType;
	public WorldElementType worldElementType { get { return m_worldElementType; } }
	public bool isDestroyed { get; private set; }

	private void Start ()
	{
		isDestroyed = false;
	}

	public float GetStartAngle ()
	{
		switch (m_worldElementType)
		{
			case WorldElementType.Fair:
			case WorldElementType.Bunker:
			case WorldElementType.Green:
				return -GetComponent<GreenMesh>().startAngle - 90f;
			case WorldElementType.Water:
				return -GetComponent<WaterMesh>().startAngle - 90f;
		}
		return 0f;
	}
	public float GetEndAngle ()
	{
		switch (m_worldElementType)
		{
			case WorldElementType.Fair:
			case WorldElementType.Bunker:
			case WorldElementType.Green:
				return -GetComponent<GreenMesh>().startAngle - 90f - GetComponent<GreenMesh>().angle;
			case WorldElementType.Water:
				return -GetComponent<WaterMesh>().startAngle - 90f - GetComponent<WaterMesh>().angle;
		}
		return 0f;
	}

	public float GetHeightAtAngle (float angle)
	{
		switch (m_worldElementType)
		{
			case WorldElementType.Fair:
			case WorldElementType.Bunker:
			case WorldElementType.Green:
				return GetComponent<GreenMesh>().GetHeight(-90f - angle);
		}
		return 0f;
	}

	public void Deactivate(Vector2 ballSpeed)
	{
		isDestroyed = true;
		if (m_destructionFxPrefab != null)
		{
			GameObject part = Instantiate(m_destructionFxPrefab, transform.position, transform.rotation);
			part.transform.position += new Vector3(0f, 0f, 0.5f);
			if(Vector2.Dot(ballSpeed, transform.right) < 0)
			{
				part.transform.localScale = new Vector3(-1f, 1f, 1f);
			}
		}
			

		gameObject.SetActive(false);
		deactivatedElements.Add(this);
;	}

	public void Reactivate()
	{
		gameObject.SetActive(true);
		isDestroyed = false;
	}

	public static void ReactivateALL()
	{
		for (int i = 0; i < deactivatedElements.Count; i++)
		{
			if (deactivatedElements[i] != null)
				deactivatedElements[i].Reactivate();
		}
		deactivatedElements.Clear();
	}
	
}
