using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinAttribute))]
public class MinDrawer: PropertyDrawer
{
	public override void OnGUI ( Rect position, SerializedProperty property, GUIContent label )
	{

		MinAttribute minAttribute  = attribute as MinAttribute;


		switch ( property.propertyType )
		{
			case SerializedPropertyType.Integer:
				property.intValue = Mathf.Max((int)minAttribute.min, EditorGUI.IntField(position, label.text, property.intValue));
				break ;

			case SerializedPropertyType.Float:
				property.floatValue = Mathf.Max(minAttribute.min, EditorGUI.FloatField(position, label.text, property.floatValue));
				break ;
		}


	}
}
