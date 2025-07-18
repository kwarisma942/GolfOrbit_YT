using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ScreenshotTaker))]
public class ScreenShotTakerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		ScreenshotTaker myScript = (ScreenshotTaker)target;
		if (GUILayout.Button("Take Screen"))
		{
			myScript.TakeScreen();
		}
	}
}
