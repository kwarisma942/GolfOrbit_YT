using Pinpin;
using Pinpin.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GreenMesh : MonoBehaviour
{

	[Min(3)] public int precision = 60;
	public float startAngle = 0f;
	public float angleDiff = 0f;
	[Min(0.1f)] public float angle = 0.1f;
	public float startRadius = 0f;
	public float endRadius = 1000f;
	public float holeRadius = 0.2f;
	[Range(0, 1f)] public float holePos = 0.1f;
	[SerializeField] private MeshFilter m_meshFilter;
	[SerializeField] private PolygonCollider2D m_collider;
	[SerializeField] private AnimationCurve m_wave;
	[SerializeField] private Transform m_hole;

	public void Configure ( float startAngle, float angle, float endRadius, float amplitude, int waveCount, int holeWavePosition, bool haveStart = false )
	{
		this.startAngle = startAngle;
		this.angle = angle;
		precision = (int)(angle * 100);
		this.endRadius = endRadius;
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		m_meshFilter.sharedMesh = mesh;
		float height = 0.2f;
		if (haveStart)
		{
			m_wave.MoveKey(0, new Keyframe(0f, amplitude));
			m_wave.MoveKey(1, new Keyframe(1f, amplitude));
		}
		for (int i = 0; i < waveCount; i++)
		{
			height = UnityEngine.Random.Range(0.4f, 0.5f) * amplitude;
			float t = 0.028f + (i * 2 + UnityEngine.Random.Range(0.4f, 0.6f)) / (waveCount * 2);
			m_wave.AddKey(new Keyframe(t, height));
			if (i == holeWavePosition)
				this.holePos = t;

			if (i < waveCount - 1)
			{
				height = UnityEngine.Random.Range(height, amplitude) * 1.1f;
				m_wave.AddKey(new Keyframe(0.028f + (i * 2 + UnityEngine.Random.Range(1.4f, 1.6f)) / (waveCount * 2), height));
			}
		}
		PlaceHole();
		GenerateMesh();
	}

	public void ConfigureBorder ( float startAngle, float endRadius, float angle, float amplitude, bool right )
	{
		this.startAngle = startAngle;
		this.endRadius = endRadius;
		this.angle = angle;
		if (right)
		{
			m_wave.MoveKey(0, new Keyframe(0f, amplitude));
			m_wave.MoveKey(1, new Keyframe(1f, 0f));
		}
		else
		{
			m_wave.MoveKey(0, new Keyframe(0f, 0f));
			m_wave.MoveKey(1, new Keyframe(1f, amplitude));
		}
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		m_meshFilter.sharedMesh = mesh;
		GenerateMesh();
	}

	private void Reset ()
	{
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		m_meshFilter.sharedMesh = mesh;
	}

#if UNITY_EDITOR && !UNITY_CLOUD_BUILD
	private void OnValidate ()
	{
		if (EditorHelper.IsPrefabAsset(gameObject))
			return;
		PlaceHole();
		GenerateMesh();
	}
#endif

	public void CleanHole ()
	{
	
		if (m_hole.childCount > 0)
		{
			DestroyImmediate(m_hole.gameObject);
			m_hole = new GameObject("HoleParent").transform;
			m_hole.parent = transform.GetChild(0);
		}
	}

	private void Start ()
	{
		PlaceHole();
		GenerateMesh();
		if (m_hole != null)
		{
			if (ApplicationManager.config.game.useNewFXs)
				Instantiate(ApplicationManager.assets.holePrefab, m_hole);
			else
				Instantiate(ApplicationManager.assets.oldHolePrefab, m_hole);
		}
	}

	private void PlaceHole ()
	{
		if (m_hole != null)
		{
			float angle = (holePos * this.angle + startAngle) * Mathf.Deg2Rad;
			float dist = endRadius + m_wave.Evaluate(holePos) - 0.1f;
			m_hole.transform.localPosition = new Vector3(Mathf.Cos(-angle) * dist, Mathf.Sin(-angle) * dist, -0.01f);
			m_hole.localRotation = Quaternion.Euler(0f, 0f, -this.angle * holePos - startAngle - 90f);
		}
	}

	public float GetHeight ( float angle )
	{
		float position = (angle - startAngle) / this.angle;
		return m_wave.Evaluate(position);
	}

	public Vector2 GetLimits ( out float angle, out float width )
	{
		Vector2 startPoint = new Vector3(Mathf.Cos(-startAngle * Mathf.Deg2Rad) * endRadius, Mathf.Sin(-startAngle * Mathf.Deg2Rad) * endRadius);
		Vector2 endPoint = new Vector3(Mathf.Cos(-(startAngle + this.angle) * Mathf.Deg2Rad) * endRadius, Mathf.Sin(-(startAngle + this.angle) * Mathf.Deg2Rad) * endRadius);
		angle = -this.startAngle - 90f - this.angle / 2f;
		width = Vector2.Distance(endPoint, startPoint);
		return transform.TransformPoint((endPoint + startPoint) / 2f);
	}

	public void GenerateMesh ()
	{
		if (m_meshFilter == null)
			return;
		int vertexCount = (precision + 1) * 2;
		int triangleCount = precision * 6;
		Vector3[] vertices = new Vector3[vertexCount];
		Vector2[] uvs = new Vector2[vertexCount];
		int[] triangles = new int[triangleCount];
		List<Vector2> colliderPath = new List<Vector2>();
		bool isFirstTimeHole = true;
		for (int i = 0; i <= precision; i++)
		{
			float t = (float)i / precision;
			float angle = t * this.angle * Mathf.Deg2Rad;
			float finalAngle = angle + startAngle * Mathf.Deg2Rad;
			float finalInnerAngle = (startAngle + angleDiff / 2f + i * (this.angle - angleDiff) / precision) * Mathf.Deg2Rad;
			vertices[i * 2] = new Vector3(Mathf.Cos(-finalInnerAngle), Mathf.Sin(-finalInnerAngle), 0f) * startRadius;
			vertices[i * 2 + 1] = new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * (endRadius + m_wave.Evaluate(t));

			// cut hole
			if (m_hole != null)
			{
				if (Vector3.Distance(vertices[i * 2 + 1], m_hole.transform.localPosition) < holeRadius / 2f + 0.1f)
				{
					if (isFirstTimeHole)
					{
						isFirstTimeHole = false;
						//colliderPath.Add(m_hole.transform.localPosition - m_hole.transform.right * (holeRadius / 2f + 0.1f));
						colliderPath.Add(m_hole.transform.localPosition - m_hole.transform.right * holeRadius / 2f - m_hole.transform.up * 0.1f);
						colliderPath.Add(m_hole.transform.localPosition - m_hole.transform.right * holeRadius / 2f - m_hole.transform.up * 0.4f);
					}
				}
				else
				{
					if (isFirstTimeHole == false)
					{
						isFirstTimeHole = true;
						colliderPath.Add(m_hole.transform.localPosition + m_hole.transform.right * holeRadius / 2f - m_hole.transform.up * 0.4f);
						colliderPath.Add(m_hole.transform.localPosition + m_hole.transform.right * holeRadius / 2f - m_hole.transform.up * 0.1f);
						//colliderPath.Add(m_hole.transform.localPosition + m_hole.transform.right * (holeRadius / 2f + 0.1f));
					}
					colliderPath.Add(new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * (endRadius - 0.1f + m_wave.Evaluate(t)));
				}
			}
			else
			{
				colliderPath.Add(new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * (endRadius - 0.1f + m_wave.Evaluate(t)));
			}
			uvs[i * 2] = new Vector2(angle / 2f * endRadius, startRadius);
			uvs[i * 2 + 1] = new Vector2(angle / 2f * endRadius, endRadius);
			if (i < precision)
			{
				int baseIndex = i * 6;
				triangles[baseIndex] = i * 2;
				triangles[baseIndex + 1] = i * 2 + 1;
				triangles[baseIndex + 2] = (i + 1) * 2 + 1;

				triangles[baseIndex + 3] = i * 2;
				triangles[baseIndex + 4] = (i + 1) * 2 + 1;
				triangles[baseIndex + 5] = (i + 1) * 2;
			}
		}
		colliderPath.Add(vertices[precision * 2]);
		colliderPath.Add(vertices[0]);

		if (m_collider != null)
			m_collider.SetPath(0, colliderPath.ToArray());



		Mesh mesh = m_meshFilter.sharedMesh;
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.name = gameObject.name;
			m_meshFilter.sharedMesh = mesh;
		}

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	public float GetHoleAngle ( Vector3 planetCenter )
	{
		Vector2 up = m_hole.transform.position - planetCenter;
		return Vector2.SignedAngle(Vector2.up, up);
	}
}