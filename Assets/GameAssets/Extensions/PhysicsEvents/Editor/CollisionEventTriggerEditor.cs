using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CollisionEventTrigger))]
public class CollisionEventTriggerEditor: Editor
{

	private enum EventType
	{
		onCollisionEnter,
		onCollisionEnter2D,
		onCollisionStay,
		onCollisionStay2D,
		onCollisionExit,
		onCollisionExit2D,
		onTriggerEnter,
		onTriggerEnter2D,
		onTriggerStay,
		onTriggerStay2D,
		onTriggerExit,
		onTriggerExit2D
	}

	public override void OnInspectorGUI ()
	{
		this.DisplayEvents();

		string[] options = Enum.GetNames(typeof(EventType));
		serializedObject.FindProperty("showedEventsMask").intValue = EditorGUILayout.MaskField("Inspected events", serializedObject.FindProperty("showedEventsMask").intValue, options);
		serializedObject.ApplyModifiedProperties();
	}

	private void DisplayEvents ()
	{
		for ( int i = 0 ; i < Enum.GetValues(typeof(EventType)).Length ; i++ )
		{
			if ((serializedObject.FindProperty("showedEventsMask").intValue & (1 << i)) != 0)
			{
				SerializedProperty sprop = serializedObject.FindProperty(Enum.GetNames(typeof(EventType))[i]);
	            EditorGUILayout.PropertyField(sprop);
	 			serializedObject.ApplyModifiedProperties();
			}
		}
	}

}
