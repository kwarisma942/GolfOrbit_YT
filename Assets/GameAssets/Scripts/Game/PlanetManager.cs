
using Pinpin;
using System;
//using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using URandom = UnityEngine.Random;

public class PlanetManager : MonoBehaviour
{
	enum ObstacleType
	{
		None = -1,
		Bunker,
		Hill,
		Water,
		Houses,
		Rocks,
		PineTree,
		Tree,
		Boat
	}

	[SerializeField] private GameConfig m_config;
	[SerializeField] private float m_startDist = 30f;
	[SerializeField] private float m_firstGreenOffset = 30f;
	[SerializeField] private float m_greenPercentage = 0.2f;
	[SerializeField] private float m_greenLenght = 5f;
	[SerializeField] private float m_bunkerLenght = 5f;
	[SerializeField] private float m_hillLenght = 5f;
	[SerializeField] private float m_waterLenght = 5f;
	[SerializeField] private float m_minDistBeweenElements = 5f;
	[SerializeField] private PlanetMesh m_planet;
	[SerializeField] private GreenMesh m_greenPrefab;
	[SerializeField] private Transform m_greenParent;

	[SerializeField] private WaterMesh m_waterPrefab;
	[SerializeField] private Transform m_waterParent;
	[SerializeField] private GreenMesh m_bunkerPrefab;
	[SerializeField] private Transform m_bunkerParent;
	[SerializeField] private GreenMesh m_hillPrefab;
	[SerializeField] private GreenMesh m_greenBorderPrefab;
	[SerializeField] private Transform m_hillParent;

	[Header("Obstacles")]
	[SerializeField]
	private Transform m_obstaclesParent;
	[SerializeField] private GameObject m_treePrefab;
	[SerializeField] private GameObject m_pineTreePrefab;
	[SerializeField] private GameObject m_boatPrefab;
	[SerializeField] private GameObject[] m_rocksPrefabs;
	[SerializeField] private GameObject[] m_housesPrefabs;
	[SerializeField] private bool easyPlanet = false;

	[Header("Decorations")]
	[SerializeField] private float m_minDist = 1f;
	[SerializeField] private float m_maxDist = 2f;
	[SerializeField] private float m_innerGrassDist = 0f;
	[SerializeField] private float m_innerGrassDist2 = 0f;
	[SerializeField] private float m_outerGrassDist = 0f;
	[SerializeField] private float m_topGrassDist = 0f;
	[SerializeField] private float m_cloudDist1 = 0f;
	[SerializeField] private float m_cloudDist2 = 0f;
	[SerializeField] private float m_birdsHeight = 0f;
	[SerializeField] private Transform m_decorationParent;
	[SerializeField] private Transform m_cloudsParent;
	[SerializeField] private Transform m_birdsParent;
	[SerializeField] private GameObject m_innerGrassPrefab;
	[SerializeField] private GameObject[] m_outerGrassPrefabs;
	[SerializeField] private GameObject[] m_topGrassPrefabs;
	[SerializeField] private GameObject m_CloudPrefab1;
	[SerializeField] private GameObject m_CloudPrefab2;
	[SerializeField] private GameObject[] m_birdsPrefabs;

	[SerializeField] private float m_cloudsRotationSpeed = 0.1f;
	[SerializeField] private float m_birdsRotationSpeed = 0.1f;

	[SerializeField] private List<WorldElement> m_worldElements = new List<WorldElement>();

	[SerializeField] private Material m_seaSparklesMaterial;
	[SerializeField] private GameObject m_greenColliderParent;
	[SerializeField] private GameObject m_hillColliderParent;
	[SerializeField] private Collider2D m_planetCollider;
	[SerializeField] private Collider2D m_hillCollider;
	private Collider2D[] m_greenColliders;
	private Collider2D[] m_hillColliders;

	public float endRadius { get { return m_planet.endRadius; } }
	public Transform decorationParent { get { return m_decorationParent; } }

	[Serializable]
	class Paralax
	{
		public Transform transform;
		public float speed;
	}
	[SerializeField] private Paralax[] m_paralax;

	private void Start ()
	{
		m_hillColliders = m_hillColliderParent.GetComponentsInChildren<Collider2D>();
		m_greenColliders = m_greenColliderParent.GetComponentsInChildren<Collider2D>();
	}

	private void Update ()
	{
		m_cloudsParent.Rotate(0f, 0f, m_cloudsRotationSpeed * Time.deltaTime);
		m_birdsParent.Rotate(0f, 0f, m_birdsRotationSpeed * Time.deltaTime);
		if (m_seaSparklesMaterial != null)
			m_seaSparklesMaterial.mainTextureOffset += Vector2.up * Time.deltaTime * 0.1f;

	}

	public void UpdateParalax (float angle)
	{
		for (int i = 0; i < m_paralax.Length; i++)
		{
			m_paralax[i].transform.eulerAngles = new Vector3(0, 0f, angle * m_paralax[i].speed);
		}
	}

	public IEnumerator EnableColliders (PhysicsMaterial2D m_bounceMaterial, PhysicsMaterial2D m_greenMaterial)
	{
		m_planetCollider.sharedMaterial = m_bounceMaterial;
		m_hillCollider.sharedMaterial = m_bounceMaterial;
		int collidersPerFrame = m_greenColliders.Length / 30;
		int i = 0;
		while (i < m_greenColliders.Length)
		{
			m_greenColliders[i].sharedMaterial = m_greenMaterial;

			i++;

			if (i % collidersPerFrame == collidersPerFrame - 1)
				yield return null;
		}

		collidersPerFrame = m_hillColliders.Length / 30;
		i = 0;
		while (i < m_hillColliders.Length)
		{
			m_hillColliders[i].sharedMaterial = m_bounceMaterial;

			i++;

			if (i % collidersPerFrame == collidersPerFrame - 1)
				yield return null;
		}
		yield return false;
	}

#if UNITY_EDITOR

	public void PopulateMap ()
	{
		float planetPerimeter = m_planet.endRadius * 2f * Mathf.PI;

		for (int i = m_greenParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_greenParent.GetChild(i).gameObject);
		}
		for (int i = m_hillParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_hillParent.GetChild(i).gameObject);
		}
		for (int i = m_waterParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_waterParent.GetChild(i).gameObject);
		}
		for (int i = m_bunkerParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_bunkerParent.GetChild(i).gameObject);
		}
		for (int i = m_obstaclesParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_obstaclesParent.GetChild(i).gameObject);
		}

		int greenCount = (int)(planetPerimeter * m_greenPercentage / m_greenLenght);

		float distBetweenGreen = planetPerimeter / greenCount;
		m_worldElements.Clear();
		Debug.Log("Green Count: " + greenCount);
		float totalDist = m_startDist;
		int level = 0;
		float distSinceLastGreen = m_firstGreenOffset;
		float distBeweenObstacles = m_minDistBeweenElements;
		while (totalDist < planetPerimeter - 50f)
		{
			float angle = -totalDist / planetPerimeter * 360f;

			if (totalDist > distSinceLastGreen + distBetweenGreen)
			{
				// calculate grren entering hill
				float amplitude = URandom.Range(0.5f, 0.7f) + URandom.Range(0.2f, 0.2f * level);
				float borderAngle = amplitude * 3f / planetPerimeter * 360f;
				angle -= borderAngle;

				GreenMesh newGreenBorder = Instantiate(m_greenBorderPrefab, m_hillParent);
				newGreenBorder.ConfigureBorder(-90f - angle - borderAngle, m_planet.endRadius, borderAngle, amplitude, false);
				m_worldElements.Add(newGreenBorder.GetComponent<WorldElement>());

				float lenght = m_greenLenght * Mathf.Pow(1.1f, level) * URandom.Range(0.8f, 1.2f);
				GreenMesh newGreen = Instantiate(m_greenPrefab, m_greenParent);
				float angleLength = lenght / planetPerimeter * 360f;

				int waveCount = 3 + (int)(level * 0.7f) + URandom.Range(0, 2);
				newGreen.Configure(-90f - angle, angleLength, m_planet.endRadius, amplitude, waveCount, Mathf.Max(waveCount - 1 - URandom.Range(0, level), 0), true);
				m_worldElements.Add(newGreen.GetComponent<WorldElement>());
				totalDist += lenght + amplitude * 6f + 1f;
				distSinceLastGreen = totalDist;

				newGreenBorder = Instantiate(m_greenBorderPrefab, m_hillParent);
				newGreenBorder.ConfigureBorder(-90f - angle + angleLength, m_planet.endRadius, borderAngle, amplitude, true);
				m_worldElements.Add(newGreenBorder.GetComponent<WorldElement>());
				angle = -totalDist / planetPerimeter * 360f;

				if (easyPlanet)
				{
					GameObject newObstacles = Instantiate(URandom.Range(0, 100) > 50f ? m_pineTreePrefab : m_treePrefab, m_obstaclesParent);
					newObstacles.transform.localEulerAngles = new Vector3(0f, 0f, angle);
					newObstacles.transform.GetChild(0).localScale = Vector3.one * (URandom.Range(0.8f, 1.2f));
				}

			}
			else
			{
				ObstacleType type = GetRandomType(level);

				switch (type)
				{
					case ObstacleType.Bunker:
						{
							float lenght = m_bunkerLenght * Mathf.Pow(1.1f, level) * URandom.Range(0.8f, 1.2f);
							GreenMesh newBunker = Instantiate(m_bunkerPrefab, m_bunkerParent);
							float angleLength = lenght / planetPerimeter * 360f;
							newBunker.Configure(-90f - angle, angleLength, m_planet.endRadius, 0f, 0, 0);
							m_worldElements.Add(newBunker.GetComponent<WorldElement>());
							totalDist += lenght;
							break;
						}
					case ObstacleType.Hill:
						{
							float lenght = m_hillLenght * Mathf.Pow(1.1f, level) * URandom.Range(0.8f, 1.2f);
							GreenMesh newHill = Instantiate(m_hillPrefab, m_hillParent);
							float angleLength = lenght / planetPerimeter * 360f;
							newHill.Configure(-90f - angle, angleLength, m_planet.endRadius, 1f + URandom.Range(0.2f, 0.3f) * level, 3 + (int)(level * 0.7f), 0);
							m_worldElements.Add(newHill.GetComponent<WorldElement>());
							totalDist += lenght;
							break;
						}
					case ObstacleType.Water:
						{
							float lenght = m_waterLenght * Mathf.Pow(1.1f, level) * URandom.Range(0.8f, 1.2f);
							WaterMesh newHill = Instantiate(m_waterPrefab, m_waterParent);
							float angleLength = lenght / planetPerimeter * 360f;
							newHill.Configure(-90f - angle, angleLength, m_planet.endRadius);
							m_worldElements.Add(newHill.GetComponent<WorldElement>());
							totalDist += lenght;
							break;
						}
					case ObstacleType.Houses:
						{
							totalDist += 5f;
							angle = -totalDist / planetPerimeter * 360f;
							GameObject newHouse = PrefabUtility.InstantiatePrefab(m_housesPrefabs[URandom.Range(0, m_housesPrefabs.Length)]) as GameObject;
							newHouse.transform.parent = m_obstaclesParent;
							newHouse.transform.localPosition = Vector3.zero;
							newHouse.transform.localEulerAngles = new Vector3(0f, 0f, angle);
							newHouse.transform.GetChild(0).localScale = Vector3.one * (URandom.Range(0.5f, 0.6f) * (1f + 0.2f * level));
							totalDist += 5f;
							break;
						}
					case ObstacleType.Rocks:
						{
							totalDist += 2f;
							angle = -totalDist / planetPerimeter * 360f;
							GameObject newRock = PrefabUtility.InstantiatePrefab(m_rocksPrefabs[URandom.Range(0, m_rocksPrefabs.Length)]) as GameObject;
							newRock.transform.parent = m_obstaclesParent;
							newRock.transform.localPosition = Vector3.zero;
							newRock.transform.localEulerAngles = new Vector3(0f, 0f, angle);
							newRock.transform.GetChild(0).localScale = Vector3.one * (URandom.Range(0.5f, 0.6f) * (1f + 0.2f * level));
							totalDist += 2f;
							break;
						}
					case ObstacleType.PineTree:
						{
							GameObject newTree = PrefabUtility.InstantiatePrefab(m_pineTreePrefab) as GameObject;
							newTree.transform.parent = m_obstaclesParent;
							newTree.transform.localPosition = Vector3.zero;
							newTree.transform.localEulerAngles = new Vector3(0f, 0f, angle);
							newTree.transform.GetChild(0).localScale = Vector3.one * (URandom.Range(0.8f, 1.2f) * (1f + 0.1f * level));
							break;
						}
					case ObstacleType.Tree:
						{
							GameObject newTree = PrefabUtility.InstantiatePrefab(m_treePrefab) as GameObject;
							newTree.transform.parent = m_obstaclesParent;
							newTree.transform.localPosition = Vector3.zero;
							newTree.transform.localEulerAngles = new Vector3(0f, 0f, angle);
							newTree.transform.GetChild(0).localScale = Vector3.one * (URandom.Range(0.8f, 1.2f) * (1f + 0.1f * level));
							break;
						}
					case ObstacleType.Boat:
						{
							totalDist += 6f;
							GameObject newBoat = PrefabUtility.InstantiatePrefab(m_boatPrefab) as GameObject;
							newBoat.transform.parent = m_obstaclesParent;
							newBoat.transform.localPosition = Vector3.zero;
							newBoat.transform.localEulerAngles = new Vector3(0f, 0f, angle);
							newBoat.transform.GetChild(0).localScale = Vector3.one * (URandom.Range(0.8f, 1.2f) * (1f + 0.1f * level));
							totalDist += 6f;
							break;
						}
				}
			}
			//Level up
			if (level < m_config.game.characterUnlockDistance.Length - 1 && totalDist > m_config.game.characterUnlockDistance[level])
			{
				level++;
				distBeweenObstacles /= 1.2f;
			}

			totalDist += distBeweenObstacles * URandom.Range(0.3f, 1.1f);
		}
	}

	private ObstacleType m_lastType = ObstacleType.None;
	private ObstacleType GetRandomType (int level)
	{
		ObstacleType type = 0;
		do
		{
			int randomValue = URandom.Range(0, 100);
			switch (level)
			{
				case 0:
					if (randomValue < 33)
					{
						type = ObstacleType.Bunker;
					}
					else if (randomValue < 66)
					{
						type = ObstacleType.Hill;
					}
					else
					{
						type = ObstacleType.Rocks;
					}
					break;
				case 1:
					if (randomValue < 20)
					{
						type = ObstacleType.Bunker;
					}
					else if (randomValue < 40)
					{
						type = ObstacleType.Hill;
					}
					else if (randomValue < 60)
					{
						type = ObstacleType.Water;
					}
					else if (randomValue < 80)
					{
						type = ObstacleType.Rocks;
					}
					else if (randomValue < 90)
					{
						type = ObstacleType.Rocks;
					}
					else
					{
						type = ObstacleType.Houses;
					}
					break;
				case 2:
					if (randomValue < 20)
					{
						type = ObstacleType.Bunker;
					}
					else if (randomValue < 40)
					{
						type = ObstacleType.Hill;
					}
					else if (randomValue < 60)
					{
						type = ObstacleType.Water;
					}
					else
					{
						type = (ObstacleType)URandom.Range(3, 6);
					}
					break;
				case 3:
				case 4:
				case 5:
					if (randomValue < 20)
					{
						type = ObstacleType.Bunker;
					}
					else if (randomValue < 40)
					{
						type = ObstacleType.Hill;
					}
					else if (randomValue < 60)
					{
						type = ObstacleType.Water;
					}
					else
					{
						type = (ObstacleType)URandom.Range(3, 7);
					}
					break;
				case 6:
					if (randomValue < 20)
					{
						type = ObstacleType.Bunker;
					}
					else if (randomValue < 40)
					{
						type = ObstacleType.Hill;
					}
					else if (randomValue < 60)
					{
						type = ObstacleType.Water;
					}
					else
					{
						type = (ObstacleType)URandom.Range(3, 8);
					}
					break;
			}
		} while (m_lastType == type);

		m_lastType = type;
		return type;
	}
#endif

	public void PlaceDecorations ()
	{
		/*for (int i = m_decorationParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_decorationParent.GetChild(i).gameObject);
		}
		for (int i = m_cloudsParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_cloudsParent.GetChild(i).gameObject);
		}
		for (int i = m_birdsParent.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(m_birdsParent.GetChild(i).gameObject);
		}*/

		float planetPerimeter = m_planet.endRadius * 2f * Mathf.PI;
		float totalDist = URandom.Range(m_minDist, m_maxDist);

		int worldElementIndex = 0;

		float lastWorldElemStartAngle = m_worldElements[worldElementIndex].GetStartAngle();
		float lastWorldElemEndAngle = m_worldElements[worldElementIndex].GetEndAngle();

		//inner grass 1st layer
		while (totalDist < planetPerimeter)
		{
			totalDist += URandom.Range(m_minDist, m_maxDist);
			float angle = -totalDist / planetPerimeter * 360f;
			bool createGrass = true;
			float height = 0f;
			if (angle < lastWorldElemEndAngle)
			{
				if (worldElementIndex < m_worldElements.Count - 1)
				{
					worldElementIndex++;
					lastWorldElemStartAngle = m_worldElements[worldElementIndex].GetStartAngle();
					lastWorldElemEndAngle = m_worldElements[worldElementIndex].GetEndAngle();
				}
			}
			if (angle < lastWorldElemStartAngle && angle > lastWorldElemEndAngle)
			{
				if (m_worldElements[worldElementIndex].worldElementType != WorldElement.WorldElementType.Fair && m_worldElements[worldElementIndex].worldElementType != WorldElement.WorldElementType.Green)
				{
					createGrass = false;
				}
				else
				{
					height = m_worldElements[worldElementIndex].GetHeightAtAngle(angle);
				}
			}
			if (createGrass)
			{
				GameObject newGrass = Instantiate(m_innerGrassPrefab);
				newGrass.transform.parent = m_decorationParent;
				newGrass.transform.localPosition = Vector3.zero;
				newGrass.transform.localEulerAngles = new Vector3(0f, 0f, angle);
				newGrass.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_innerGrassDist + height + URandom.Range(-0.1f, 1f), -0.1f);
			}
		}

		//inner grass 2nd layer
		totalDist = URandom.Range(m_minDist, m_maxDist);
		while (totalDist < planetPerimeter)
		{
			totalDist += URandom.Range(m_minDist, m_maxDist);
			float angle = -totalDist / planetPerimeter * 360f;
			GameObject newGrass = Instantiate(m_innerGrassPrefab);
			newGrass.transform.parent = m_decorationParent;
			newGrass.transform.localPosition = Vector3.zero;
			newGrass.transform.localEulerAngles = new Vector3(0f, 0f, angle);
			newGrass.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_innerGrassDist2 + URandom.Range(-1f, 1f), -0.1f);

		}

		//Clouds
		totalDist = 0f;
		while (totalDist < planetPerimeter)
		{
			totalDist += URandom.Range(5f, 20f);
			float angle = -totalDist / planetPerimeter * 360f;
			GameObject newCloud = Instantiate(m_CloudPrefab1);
			newCloud.transform.parent = m_cloudsParent;
			newCloud.transform.localPosition = Vector3.zero;
			newCloud.transform.localEulerAngles = new Vector3(0f, 0f, angle);
			newCloud.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_cloudDist1 + URandom.Range(-1f, 1f), -0.1f);
			newCloud.transform.GetChild(0).localScale = Vector3.one * URandom.Range(1f, 2f);

		}

		//Clouds
		totalDist = 0f;
		while (totalDist < planetPerimeter)
		{
			totalDist += URandom.Range(50f, 100f);
			float angle = -totalDist / planetPerimeter * 360f;
			GameObject newCloud = Instantiate(m_CloudPrefab2);
			newCloud.transform.parent = m_cloudsParent;
			newCloud.transform.localPosition = Vector3.zero;
			newCloud.transform.localEulerAngles = new Vector3(0f, 0f, angle);
			newCloud.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_cloudDist2 + URandom.Range(-1f, 1f), -0.1f);
			newCloud.transform.GetChild(0).localScale = Vector3.one * URandom.Range(1f, 2f);

		}

		worldElementIndex = 0;
		lastWorldElemStartAngle = m_worldElements[worldElementIndex].GetStartAngle();
		lastWorldElemEndAngle = m_worldElements[worldElementIndex].GetEndAngle();
		totalDist = URandom.Range(m_minDist, m_maxDist);
		while (totalDist < planetPerimeter - 8f)
		{
			totalDist += URandom.Range(m_minDist, m_maxDist);
			float angle = -totalDist / planetPerimeter * 360f;
			bool createGrass = true;
			float height = 0f;
			if (angle < lastWorldElemEndAngle)
			{
				if (worldElementIndex < m_worldElements.Count - 1)
				{
					worldElementIndex++;
					lastWorldElemStartAngle = m_worldElements[worldElementIndex].GetStartAngle();
					lastWorldElemEndAngle = m_worldElements[worldElementIndex].GetEndAngle();
				}
			}
			if (angle < lastWorldElemStartAngle && angle > lastWorldElemEndAngle)
			{
				if (m_worldElements[worldElementIndex].worldElementType != WorldElement.WorldElementType.Fair)
				{
					createGrass = false;
				}
				else
				{
					height = m_worldElements[worldElementIndex].GetHeightAtAngle(angle);
				}
			}
			if (createGrass)
			{
				GameObject newGrass = Instantiate(m_outerGrassPrefabs[URandom.Range(0, m_outerGrassPrefabs.Length)]);
				newGrass.transform.parent = m_decorationParent;
				newGrass.transform.localPosition = Vector3.zero;
				newGrass.transform.localEulerAngles = new Vector3(0f, 0f, angle);
				newGrass.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_outerGrassDist + height, -0.1f);
			}

		}

		worldElementIndex = 0;
		lastWorldElemStartAngle = m_worldElements[worldElementIndex].GetStartAngle();
		lastWorldElemEndAngle = m_worldElements[worldElementIndex].GetEndAngle();
		totalDist = URandom.Range(m_minDist, m_maxDist);
		while (totalDist < planetPerimeter - 8f)
		{
			totalDist += URandom.Range(m_minDist, m_maxDist);
			float angle = -totalDist / planetPerimeter * 360f;
			bool createGrass = true;
			float height = 0f;
			if (angle < lastWorldElemEndAngle)
			{
				if (worldElementIndex < m_worldElements.Count - 1)
				{
					worldElementIndex++;
					lastWorldElemStartAngle = m_worldElements[worldElementIndex].GetStartAngle();
					lastWorldElemEndAngle = m_worldElements[worldElementIndex].GetEndAngle();
				}
			}
			if (angle < lastWorldElemStartAngle && angle > lastWorldElemEndAngle)
			{
				if (m_worldElements[worldElementIndex].worldElementType != WorldElement.WorldElementType.Fair)
				{
					createGrass = false;
				}
				else
				{
					height = m_worldElements[worldElementIndex].GetHeightAtAngle(angle);
				}
			}
			if (createGrass)
			{
				GameObject newGrass = Instantiate(m_topGrassPrefabs[URandom.Range(0, m_topGrassPrefabs.Length)]);
				newGrass.transform.parent = m_decorationParent;
				newGrass.transform.localPosition = Vector3.zero;
				newGrass.transform.localEulerAngles = new Vector3(0f, 0f, angle);
				newGrass.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_topGrassDist + height, -0.1f);
			}

		}

		//Birds
		totalDist = 0f;
		while (totalDist < planetPerimeter)
		{
			totalDist += URandom.Range(40f, 50f);
			float angle = -totalDist / planetPerimeter * 360f;
			GameObject newBird = Instantiate(m_birdsPrefabs[URandom.Range(0, m_birdsPrefabs.Length)]);
			newBird.transform.parent = m_birdsParent;
			newBird.transform.localPosition = Vector3.zero;
			newBird.transform.localEulerAngles = new Vector3(0f, 0f, angle);
			newBird.transform.GetChild(0).localPosition = new Vector3(0f, m_planet.endRadius + m_birdsHeight + URandom.Range(-5f, 5f), 0.0f);
			newBird.transform.GetChild(0).localScale *= URandom.Range(0.9f, 1.1f);
		}
	}
	public void CleanHoles()
	{
		foreach (WorldElement elem in m_worldElements)
		{
			if (elem.worldElementType == WorldElement.WorldElementType.Green)
				elem.GetComponent<GreenMesh>().CleanHole();
		}
	}

}