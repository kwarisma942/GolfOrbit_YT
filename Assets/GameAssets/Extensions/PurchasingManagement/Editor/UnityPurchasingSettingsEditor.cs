using UnityEngine;
using UnityEditor;
using System.IO;

namespace PurchasingManagement.Editor
{

	[CustomEditor(typeof(UnityPurchasingSettings))]
	public class UnityPurchasingSettingsEditor: UnityEditor.Editor
	{

		private SerializedProperty	m_androidProductNameProperty;
		private SerializedProperty	m_iosProductNameProperty;
		private SerializedProperty	m_debugModeProperty;
	
		[MenuItem("Tools/PurchasingManagement/UnityPurchasingSettings")]
		private static void ShowSettings ()
		{
			Object asset = Resources.Load("UnityPurchasingSettings");
			if (asset == null)
			{
				if(!Directory.Exists("Assets/Resources"))
					Directory.CreateDirectory("Assets/Resources");
				asset = ScriptableObject.CreateInstance<UnityPurchasingSettings>() as Object;
				UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/Resources/UnityPurchasingSettings.asset");
				UnityEditor.AssetDatabase.SaveAssets();
			}
			EditorGUIUtility.PingObject(asset);
			Selection.activeObject = asset;
		}
	
		private void OnEnable ()
		{
			m_androidProductNameProperty = serializedObject.FindProperty("m_androidProductName");
			m_iosProductNameProperty = serializedObject.FindProperty("m_iosProductName");
			m_debugModeProperty = serializedObject.FindProperty("m_debugMode");
		}
		
		public override void OnInspectorGUI ()
		{
			// Settings
			EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
		
			serializedObject.UpdateIfRequiredOrScript();
			
			EditorGUILayout.PropertyField(m_androidProductNameProperty, new GUIContent("Android Product Name"));
			EditorGUILayout.PropertyField(m_iosProductNameProperty, new GUIContent("iOS Product Name"));
			EditorGUILayout.PropertyField(m_debugModeProperty, new GUIContent("Debug mode"));
			
			serializedObject.ApplyModifiedProperties();
	
		}
	
	}

}