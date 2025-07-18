using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMesh : MonoBehaviour
{

	public int precision = 60;
	public float startAngle = 0f;
	public float angleDiff = 0f;
	public float angle = 0.1f;
	public float startRadius = 0f;
	public float endRadius = 1000f;
	[SerializeField] private MeshFilter m_meshFilter;
	[SerializeField] private PolygonCollider2D m_collider;
	[SerializeField] private PolygonCollider2D m_floorCollider;

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
		GenerateMesh();
	}
#endif

	private void Start ()
	{
		GenerateMesh();
	}

	public void Configure ( float startAngle, float angle, float endRadius)
	{
		this.startAngle = startAngle;
		this.angle = angle;
		precision = (int)(angle * 40);
		this.endRadius = endRadius;
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name;
		m_meshFilter.sharedMesh = mesh;
		GenerateMesh();
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
		Vector2[] floorColliderPath = new Vector2[8];


		for (int i = 0; i <= precision; i++)
		{
			float angle = i * this.angle * Mathf.Deg2Rad / precision;
			float finalAngle = angle + startAngle * Mathf.Deg2Rad;
			float finalInnerAngle = (startAngle + angleDiff / 2f + i * (this.angle - angleDiff) / precision) * Mathf.Deg2Rad;
			vertices[i * 2] = new Vector3(Mathf.Cos(-finalInnerAngle), Mathf.Sin(-finalInnerAngle), 0f) * startRadius;
			vertices[i * 2 + 1] = new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * endRadius;
			colliderPath.Add(new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * (endRadius - 0.1f));
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

		floorColliderPath[0] = new Vector3(Mathf.Cos((-startAngle+ 0.02f) * Mathf.Deg2Rad), Mathf.Sin((-startAngle + 0.02f) * Mathf.Deg2Rad), 0f) * (endRadius - 0.1f);
		floorColliderPath[1] = new Vector3(Mathf.Cos(-startAngle * Mathf.Deg2Rad), Mathf.Sin(-startAngle * Mathf.Deg2Rad), 0f) * (endRadius - 0.1f);
		floorColliderPath[2] = vertices[0];
		floorColliderPath[3] = vertices[precision * 2];
		floorColliderPath[4] = new Vector3(Mathf.Cos(-(this.angle + startAngle) * Mathf.Deg2Rad), Mathf.Sin(-(this.angle + startAngle) * Mathf.Deg2Rad), 0f) * (endRadius - 0.1f);
		floorColliderPath[5] = new Vector3(Mathf.Cos(-(this.angle + startAngle + 0.02f) * Mathf.Deg2Rad), Mathf.Sin(-(this.angle + startAngle + 0.02f) * Mathf.Deg2Rad), 0f) * (endRadius - 0.1f);
		floorColliderPath[6] = new Vector3(Mathf.Cos(-(this.angle + startAngle) * Mathf.Deg2Rad), Mathf.Sin(-(this.angle + startAngle) * Mathf.Deg2Rad), 0f) * (startRadius - 1f);
		floorColliderPath[7] = new Vector3(Mathf.Cos(-startAngle * Mathf.Deg2Rad), Mathf.Sin(-startAngle * Mathf.Deg2Rad), 0f) * (startRadius - 1f);

		if (m_collider != null)
			m_collider.SetPath(0, colliderPath.ToArray());

		if (m_floorCollider != null)
			m_floorCollider.SetPath(0, floorColliderPath);


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

}