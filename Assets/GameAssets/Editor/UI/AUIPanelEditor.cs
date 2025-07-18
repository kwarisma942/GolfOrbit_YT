using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Pinpin.UI
{
	[CustomEditor(typeof(AUIPanel), true)]
	public class AUIPanelEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();
			if (GUILayout.Button("RenameButtons"))
			{
				AUIPanel panel = target as AUIPanel;
				if (panel != null)
				{
					PushButton[] pushButtons = panel.GetComponentsInChildren<PushButton>();
					for (int i = 0; i < pushButtons.Length; i++)
					{
						if(!pushButtons[i].name.Contains(panel.name))
						{
							pushButtons[i].name = panel.name + "_" + pushButtons[i].name;
							if (pushButtons[i].isAdButton)
								pushButtons[i].name += "_RV";
						}
					}
					EditorUtility.SetDirty(panel);
				}
			}
		}
	}
}