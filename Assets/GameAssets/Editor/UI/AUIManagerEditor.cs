using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Pinpin.UI.Editor
{

	[CustomEditor(typeof(AUIManager), true)]
	public class AUIManagerEditor: UnityEditor.Editor
	{

		private SerializedProperty	m_panelsProperty;
		private ReorderableList		m_panelsList;
		private SerializedProperty	m_popupsProperty;
		private ReorderableList		m_popupsList;

		private void OnEnable ()
		{
			m_panelsProperty = serializedObject.FindProperty("m_panels");
			m_panelsList = new ReorderableList(serializedObject, m_panelsProperty, true, true, true, true);
			m_panelsList.drawHeaderCallback = OnDrawPanelsListHeader;
			m_panelsList.drawElementCallback = OnDrawPanelsListElement;

			m_popupsProperty = serializedObject.FindProperty("m_popups");
			m_popupsList = new ReorderableList(serializedObject, m_popupsProperty, true, true, true, true);
			m_popupsList.drawHeaderCallback = OnDrawPopupsListHeader;
			m_popupsList.drawElementCallback = OnDrawPopupsListElement;
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.UpdateIfRequiredOrScript();

			GUILayout.Space(10);
			this.DisplayPanelsList();
			GUILayout.Space(10);
			this.DisplayPopupList();

			this.DisplayAllChildsProperties();

			serializedObject.ApplyModifiedProperties();
		}

		protected void DisplayPanelsList ()
		{
			m_panelsList.DoLayoutList();
		}

		protected void DisplayPopupList ()
		{
			m_popupsList.DoLayoutList();
		}

		private void OnDrawPanelsListHeader ( Rect rect )
		{
			EditorGUI.LabelField(rect, "Panels");
		}

		private void OnDrawPanelsListElement ( Rect rect, int index, bool isActive, bool isFocused )
		{
			SerializedProperty element = m_panelsList.serializedProperty.GetArrayElementAtIndex(index);
			EditorGUI.PropertyField( new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
		}

		private void OnDrawPopupsListHeader ( Rect rect )
		{
			EditorGUI.LabelField(rect, "Popups");
		}

		private void OnDrawPopupsListElement ( Rect rect, int index, bool isActive, bool isFocused )
		{
			SerializedProperty element = m_popupsList.serializedProperty.GetArrayElementAtIndex(index);
			rect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(rect, element, GUIContent.none);
		}

		private void DisplayAllChildsProperties ()
		{
			SerializedProperty it = serializedObject.GetIterator();

			it.NextVisible(true);
			for ( int i = 0 ; i < 2 ; i++ )
				it.NextVisible(false);

			while (it.NextVisible(false))
				EditorGUILayout.PropertyField(it, new GUIContent(it.displayName));
		}

	}

}