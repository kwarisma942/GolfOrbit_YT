using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Pinpin.UI.Editor
{

	[CustomEditor(typeof(LanguageButton), true), CanEditMultipleObjects]
	public class LanguageButtonEditor : PushButtonEditor
	{

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_language"), new GUIContent("Language"));
			serializedObject.ApplyModifiedProperties();
		}

	}

}