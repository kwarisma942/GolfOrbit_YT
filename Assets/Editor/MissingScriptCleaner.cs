using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MissingScriptCleaner : EditorWindow
{
    private bool includeInactive = true;
    private bool includePrefabs = true;
    private int cleanedObjectsCount = 0;

    [MenuItem("Tools/Cleanup Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow<MissingScriptCleaner>("Missing Script Cleaner");
    }

    void OnGUI()
    {
        GUILayout.Label("Missing Script Cleanup Tool", EditorStyles.boldLabel);
        includeInactive = EditorGUILayout.Toggle("Include Inactive Objects", includeInactive);
        includePrefabs = EditorGUILayout.Toggle("Include Prefabs", includePrefabs);

        if (GUILayout.Button("Clean Selected Objects"))
        {
            CleanSelectedObjects();
        }

        if (GUILayout.Button("Clean Entire Scene"))
        {
            CleanEntireScene();
        }

        if (GUILayout.Button("Clean All Prefabs in Project"))
        {
            CleanAllPrefabs();
        }

        EditorGUILayout.LabelField($"Cleaned objects: {cleanedObjectsCount}");
    }

    private void CleanSelectedObjects()
    {
        cleanedObjectsCount = 0;
        foreach (GameObject go in Selection.gameObjects)
        {
            cleanedObjectsCount += CleanGameObject(go);
        }
        Debug.Log($"Cleaned {cleanedObjectsCount} selected objects");
    }

    private void CleanEntireScene()
    {
        cleanedObjectsCount = 0;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(includeInactive);
        foreach (GameObject go in allObjects)
        {
            cleanedObjectsCount += CleanGameObject(go);
        }
        Debug.Log($"Cleaned {cleanedObjectsCount} objects in scene");
    }

    private void CleanAllPrefabs()
    {
        cleanedObjectsCount = 0;
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string path in prefabPaths)
        {
            if (path.EndsWith(".prefab"))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    cleanedObjectsCount += CleanGameObject(prefab);
                    EditorUtility.SetDirty(prefab);
                }
            }
        }
        AssetDatabase.SaveAssets();
        Debug.Log($"Cleaned {cleanedObjectsCount} prefabs in project");
    }

    public static int CleanGameObject(GameObject go)
    {
        int removedCount = 0;
        
        // Check the GameObject itself for missing scripts
        var components = go.GetComponents<Component>();
        var serializedObject = new SerializedObject(go);
        var prop = serializedObject.FindProperty("m_Component");
        
        for (int i = components.Length - 1; i >= 0; i--)
        {
            if (components[i] == null)
            {
                prop.DeleteArrayElementAtIndex(i);
                removedCount++;
            }
        }
        
        if (removedCount > 0)
        {
            serializedObject.ApplyModifiedProperties();
            Debug.Log($"Removed {removedCount} missing scripts from {go.name}", go);
        }

        // Recursively check children
        foreach (Transform child in go.transform)
        {
            removedCount += CleanGameObject(child.gameObject);
        }

        return removedCount;
    }
}