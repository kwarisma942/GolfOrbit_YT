using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SliceDuplicator: EditorWindow
{
 
	Object copyFrom;
	//Object copyTo;
	List<Object> copyTo = new List<Object>() { null };
	Vector2 scrollPos = Vector2.zero;

	// Creates a new option in "Windows"
	[MenuItem ("Tools/SpriteSheet Slice Duplicator")]
	static void Init ()
	{
		// Get existing open window or if none, make a new one:
		SliceDuplicator window = (SliceDuplicator)EditorWindow.GetWindow (typeof (SliceDuplicator));

		window.Show();
	}

	void OnGUI ()
	{

		scrollPos = GUILayout.BeginScrollView(scrollPos, false, true); 

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Copy from:", EditorStyles.boldLabel);
		copyFrom = EditorGUILayout.ObjectField(copyFrom, typeof(Texture2D), false, GUILayout.Width(220));
		GUILayout.EndHorizontal ();

		//GUILayout.BeginHorizontal ();
		GUILayout.Label ("Copy to:", EditorStyles.boldLabel);
		//copyTo = EditorGUILayout.ObjectField(copyTo, typeof(Texture2D), false, GUILayout.Width(220));

		EditorGUI.BeginChangeCheck();

		for ( int i = 0 ; i < copyTo.Count ; i++ )
			copyTo[i] = EditorGUILayout.ObjectField(copyTo[i], typeof(Texture2D), false, GUILayout.Width(220));

		if (EditorGUI.EndChangeCheck())
		{
			for ( int i = 0 ; i < copyTo.Count - 1 ; i++ )
			{
				if (copyTo[i] == null)
					copyTo.RemoveAt(i);
			}

			if (copyTo[copyTo.Count - 1] != null)
				copyTo.Add(null);
		}
		//GUILayout.EndHorizontal ();

		GUILayout.Space (25f);
		if (GUILayout.Button ("Copy pivots, slices and borders"))
			CopyPivotsSlicesAndBorders();

		GUILayout.EndScrollView();
	}
 
   void CopyPivotsSlicesAndBorders ()
   {
		if (!copyFrom || copyTo.Count == 0 || copyTo[0] == null)
		{
			Debug.Log("Missing one object");
			return ;
		}

		string copyFromPath = AssetDatabase.GetAssetPath(copyFrom);
		TextureImporter ti1 = AssetImporter.GetAtPath(copyFromPath) as TextureImporter;
		ti1.isReadable = true;

		foreach ( Object to in copyTo)
		{
			if (to != null)
			{

				string copyToPath = AssetDatabase.GetAssetPath(to);
				TextureImporter ti2 = AssetImporter.GetAtPath(copyToPath) as TextureImporter;
				ti2.isReadable = true;

				if (ti1.spriteImportMode == SpriteImportMode.Multiple)
				{
					if (ti2.spriteImportMode == SpriteImportMode.Multiple)
					{
						// Bug? Need to convert to single then back to multiple in order to make changes when it's already sliced
						ti2.spriteImportMode = SpriteImportMode.Single;
						AssetDatabase.ImportAsset(copyToPath, ImportAssetOptions.ForceUpdate);
					}
					ti2.spriteImportMode = SpriteImportMode.Multiple;
					List < SpriteMetaData > newData = new List < SpriteMetaData > ();

					for (int i = 0; i < ti1.spritesheet.Length; i++)
					{
						SpriteMetaData d = ti1.spritesheet[i];
						d.name = d.name.Replace(copyFrom.name, to.name);
						newData.Add(d);
					}
					ti2.spritesheet = newData.ToArray();
				}
				else
				{
					ti2.spriteBorder = ti1.spriteBorder;
				}


				AssetDatabase.ImportAsset(copyToPath, ImportAssetOptions.ForceUpdate);
			}
		}
	}
}
 
