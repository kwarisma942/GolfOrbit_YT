using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationExporter))]
public class LocalizationExporterEditor : Editor
{
	LocalizationExporter localize;

	private void OnEnable ()
	{
		localize = (LocalizationExporter)target;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Test"))
			localize.CreateJSON();
	}
}