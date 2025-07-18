using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMesh : MonoBehaviour
{

	public int precision = 60;
	public float startAngle = 0f;
	public float angle = 0.1f;
	public float startRadius = 0f;
	public float endRadius = 1000f;
	[SerializeField] private MeshFilter m_meshFilter;

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

	public void GenerateMesh ()
	{
		if (m_meshFilter == null)
			return;
		int vertexCount = (precision + 1) * 2;
		int triangleCount = precision * 6;
		Vector3[] vertices = new Vector3[vertexCount];
		Vector2[] uvs = new Vector2[vertexCount];
		int[] triangles = new int[triangleCount];
		Vector2[] colliderPath = new Vector2[precision + 3];
		//Vector2[] floorColliderPath = new Vector2[6];


		for (int i = 0; i <= precision; i++)
		{
			float angle = i * this.angle * Mathf.Deg2Rad / precision;
			float finalAngle = angle + startAngle * Mathf.Deg2Rad;
			vertices[i * 2] = new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * startRadius;
			vertices[i * 2 + 1] = new Vector3(Mathf.Cos(-finalAngle), Mathf.Sin(-finalAngle), 0f) * endRadius;
			colliderPath[i] = vertices[i * 2 + 1];
			uvs[i * 2] = new Vector2((float)i / precision, 0f);
			uvs[i * 2 + 1] = new Vector2((float)i / precision, 1f);
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