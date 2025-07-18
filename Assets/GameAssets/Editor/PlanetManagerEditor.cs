using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetManager))]
public class PlanetManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PlanetManager greenManager = target as PlanetManager;
		if (GUILayout.Button("GenerateLD"))
		{
			greenManager.PopulateMap();
		}

		if (GUILayout.Button("CleanHoles"))
		{
			greenManager.CleanHoles();
		}
	}
}

