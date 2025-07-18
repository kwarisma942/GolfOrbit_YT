using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextMesh), true), CanEditMultipleObjects]
public class TextMeshEditor: Editor
{

	private Renderer renderer { get; set; }
	private string[] sortingsLayersNames { get; set; }

	private void OnEnable ()
	{
		this.renderer = (target as TextMesh).GetComponent<Renderer>();
		this.sortingsLayersNames = new string[SortingLayer.layers.Length];
		
		for ( int i = 0 ; i < SortingLayer.layers.Length ; i++ )
			this.sortingsLayersNames[i] = SortingLayer.layers[i].name;
	}

	private int FindLayerNameIndex ( int layerID )
	{
		string layerName = SortingLayer.IDToName(layerID);

		for ( int i = 0 ; i < this.sortingsLayersNames.Length ; i++ )
			if (layerName == this.sortingsLayersNames[i])
				return (i);
		return (0);
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.UpdateIfRequiredOrScript();
		int selectedLayer = this.FindLayerNameIndex(this.renderer.sortingLayerID);
		selectedLayer = EditorGUILayout.Popup("Sorting layer", selectedLayer, this.sortingsLayersNames);
		renderer.sortingLayerID = SortingLayer.NameToID(this.sortingsLayersNames[selectedLayer]);
		renderer.sortingOrder = EditorGUILayout.IntField("Sorting order", this.renderer.sortingOrder);

		serializedObject.ApplyModifiedProperties();
	}
}
