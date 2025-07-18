using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditorHelper
{  
	
	// In scene.
	public static bool IsPrefabInstance ( GameObject obj )
	{
		bool isPrefabInstance = false;
#if UNITY_EDITOR
		isPrefabInstance = UnityEditor.PrefabUtility.IsPartOfAnyPrefab(obj)
			&& !UnityEditor.PrefabUtility.IsPartOfPrefabAsset(obj)
			&& UnityEditor.PrefabUtility.IsPartOfNonAssetPrefabInstance(obj);
#endif
		return isPrefabInstance;
	} 
	
	// In project or prefab editor.
	public static bool IsPrefabAsset ( GameObject obj )
	{
		bool isPrefabAsset = false;
#if UNITY_EDITOR
		var stage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
		isPrefabAsset = (stage != null
				&& stage.scene == obj.scene)
			|| (UnityEditor.PrefabUtility.IsPartOfAnyPrefab(obj)
					&& UnityEditor.PrefabUtility.IsPartOfPrefabAsset(obj)
					&& !UnityEditor.PrefabUtility.IsPartOfNonAssetPrefabInstance(obj));
#endif
		return isPrefabAsset;
	}

}
