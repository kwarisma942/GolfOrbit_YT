using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MaxAttribute))]
public class MaxDrawer: PropertyDrawer
{
	public override void OnGUI ( Rect position, SerializedProperty property, GUIContent label )
	{

		MaxAttribute maxAttribute  = attribute as MaxAttribute;

		switch ( property.propertyType )
		{
			case SerializedPropertyType.Integer:
				property.intValue = Mathf.Min((int)maxAttribute.max, EditorGUI.IntField(position, label.text, property.intValue));
				break ;

			case SerializedPropertyType.Float:
				property.floatValue = Mathf.Min(maxAttribute.max, EditorGUI.FloatField(position, label.text, property.floatValue));
				break ;
		}


	}
}
