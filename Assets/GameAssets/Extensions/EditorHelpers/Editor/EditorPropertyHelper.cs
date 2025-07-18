using UnityEngine;
using UnityEditor;

public static class EditorPropertyHelper
{

	public static float GetPropertyHeight ( SerializedProperty element )
	{
		int depth = element.depth;

		if (element.hasVisibleChildren)
		{
			if (element.isExpanded)
			{
				float height = 0f;

				element.NextVisible(true);
				do
				{
					if (element.depth == depth)
						break ;

					if (element.hasVisibleChildren)
					{
						if (element.isExpanded)
							height += (element.Copy().CountInProperty() + 1) * EditorGUIUtility.singleLineHeight;
						else
							height += (element.Copy().CountInProperty()) * EditorGUIUtility.singleLineHeight;
					}
					else
						height += element.Copy().CountInProperty() * EditorGUIUtility.singleLineHeight;
				} while (element.NextVisible(false));

				return (height + EditorGUIUtility.singleLineHeight);
			}
		}
		return (EditorGUIUtility.singleLineHeight);
	}

	public static void DrawProperty ( Rect rect, SerializedProperty element, GUIContent label = null )
	{
		rect.height = EditorGUIUtility.singleLineHeight;
		int depth = element.depth;

		if (element.hasVisibleChildren)
		{
			rect.x += 15;
			rect.width -= 15;

			SerializedProperty	cpy = element.Copy();
			string				displayName;

			cpy.NextVisible(true);
			if (cpy.displayName == "Name" && !string.IsNullOrEmpty(cpy.stringValue))
				displayName = cpy.stringValue;
			else
				displayName = element.displayName;

			element.isExpanded = EditorGUI.Foldout(rect, element.isExpanded, displayName);

			if (element.isExpanded)
			{
				rect.x += 15;
				rect.width -= 15;
				rect.y += EditorGUIUtility.singleLineHeight;

				if (element.NextVisible(true))
				{
					do
					{
						if (element.depth <= depth)
							break ;

						if (element.hasVisibleChildren)
						{
							EditorGUI.PropertyField(rect, element, true);
							if (element.isExpanded)
								rect.y += (element.Copy().CountInProperty() + 1) * EditorGUIUtility.singleLineHeight;
							else
								rect.y += (element.Copy().CountInProperty()) * EditorGUIUtility.singleLineHeight;
						}
						else
						{
							EditorGUI.PropertyField(rect, element, false);
							rect.y += element.Copy().CountInProperty() * EditorGUIUtility.singleLineHeight;
						}
					} while (element.NextVisible(false));
				}
			}
		}
		else
		{
			if (label == null)
				EditorGUI.PropertyField(rect, element);
			else
				EditorGUI.PropertyField(rect, element, label);
		}
	}
}
