using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Pinpin.UI
{
	[CustomEditor(typeof(AUIPopup), true)]
	public class AUIPopupEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();
			if (GUILayout.Button("RenameButtons"))
			{
				AUIPopup popup = target as AUIPopup;
				if (popup != null)
				{
					PushButton[] pushButtons = popup.GetComponentsInChildren<PushButton>();
					for (int i = 0; i < pushButtons.Length; i++)
					{
						if(!pushButtons[i].name.Contains(popup.name))
						{
							pushButtons[i].name = popup.name + "_" + pushButtons[i].name;
							if (pushButtons[i].isAdButton)
								pushButtons[i].name += "_RV";
						}
					}
					EditorUtility.SetDirty(popup);
				}
			}
		}
	}
}