using UnityEngine;
using UnityEditor;
using System.IO;

namespace Pinpin.Editor
{

	[CustomEditor(typeof(GameConfig))]
	public class GameConfigEditor: UnityEditor.Editor
	{
	
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Export JSON"))
			{
				 string path = EditorUtility.SaveFilePanel("Export global config", "", "GameConfig.json", "json");
				 if (!string.IsNullOrEmpty(path))
				 	 File.WriteAllText(path, JsonUtility.ToJson(target as GameConfig));
			}
			if (GUILayout.Button("Import JSON"))
			{
				 string path = EditorUtility.OpenFilePanel("Import global config", "", "json");
				 if (!string.IsNullOrEmpty(path))
				 	 JsonUtility.FromJsonOverwrite(File.ReadAllText(path), target as GameConfig);
			}

			base.OnInspectorGUI();
		
		}
		
	}

}