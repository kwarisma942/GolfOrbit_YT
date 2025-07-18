using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Pinpin.UI.Editor
{

	[CustomEditor(typeof(PushButton), true), CanEditMultipleObjects]
	public class PushButtonEditor: UnityEditor.UI.ButtonEditor
	{
		private PushButton button;

		private SerializedProperty m_disableImageProperty;
		private SerializedProperty	m_textProperty;
		private SerializedProperty	m_iconProperty;

		private SerializedProperty m_isShiningProperty;
		private SerializedProperty m_shineImageProperty;
		private SerializedProperty m_shineStartDelayProperty;
		private SerializedProperty m_shinePauseDurationProperty;
		private SerializedProperty m_shineDurationProperty;

		private SerializedProperty m_isBouncingProperty;
		private SerializedProperty m_bounceStartDelayProperty;
		private SerializedProperty m_bouncePauseDuration;
		private SerializedProperty m_bounceDurationProperty;

		private SerializedProperty m_bounceOnClickProperty;

		private SerializedProperty m_repeatOnHoldProperty;
		private SerializedProperty m_holdFrequencyProperty;
		private SerializedProperty m_holdDelayProperty;

		private SerializedProperty m_adsButtonProperty;

		private bool m_isShining
		{
			get { return m_isShiningProperty.boolValue; }
			set
			{
				if(m_isShiningProperty.boolValue != value)
				{
					OnShiningValueChange(value);
				}
}
		}

		protected override void OnEnable ()
		{
			button = (PushButton)target;

			base.OnEnable();
			m_disableImageProperty = serializedObject.FindProperty("m_disableImage");
			m_textProperty = serializedObject.FindProperty("m_text");
			m_iconProperty = serializedObject.FindProperty("m_icon");

			m_shineImageProperty = serializedObject.FindProperty("m_shineImage");
			m_isShiningProperty = serializedObject.FindProperty("m_isShining");
			m_shineStartDelayProperty = serializedObject.FindProperty("m_shineStartDelay");
			m_shinePauseDurationProperty = serializedObject.FindProperty("m_shinePauseDuration");
			m_shineDurationProperty = serializedObject.FindProperty("m_shineDuration");

			m_isBouncingProperty = serializedObject.FindProperty("m_isBouncing");
			m_bounceStartDelayProperty = serializedObject.FindProperty("m_bounceStartDelay");
			m_bouncePauseDuration = serializedObject.FindProperty("m_bouncePauseDuration");
			m_bounceDurationProperty = serializedObject.FindProperty("m_bounceDuration");

			m_bounceOnClickProperty = serializedObject.FindProperty("m_bounceOnClick");

			m_repeatOnHoldProperty = serializedObject.FindProperty("m_repeatOnHold");
			m_holdFrequencyProperty = serializedObject.FindProperty("m_holdFrequency");
			m_holdDelayProperty = serializedObject.FindProperty("m_holdDelay");

			m_adsButtonProperty = serializedObject.FindProperty("m_adButton");
		}

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			serializedObject.Update();

			this.DisplayDisableImageProperty();
			this.DisplayTextProperty();
			this.DisplayIconProperty();
			this.DisplayIsShiningProperty();
			if (m_isShiningProperty.boolValue)
			{
				m_isShining = m_isShiningProperty.boolValue;
				this.DisplayShineImageProperty();
				this.DisplayShineStartDelayProperty();
				this.DisplayShinePauseDurationProperty();
				this.DisplayShineDurationProperty();
			}
			this.DisplayIsBouncingProperty();
			if (m_isBouncingProperty.boolValue)
			{
				this.DisplayBounceStartDelayProperty();
				this.DisplayBouncePauseDurationProperty();
				this.DisplayBounceDurationProperty();
			}
			this.DisplayBounceOnClickProperty();

			this.DisplayRepeatOnHoldProperty();
			if (m_repeatOnHoldProperty.boolValue)
			{
				this.DisplayHoldDelayProperty();
				this.DisplayHoldFrequencyProperty();
			}

			this.DisplayAdProperty();

			serializedObject.ApplyModifiedProperties();

		}

		protected void DisplayDisableImageProperty ()
		{
			EditorGUILayout.PropertyField(m_disableImageProperty, new GUIContent("Disable Image"));
		}

		protected void DisplayTextProperty ()
		{
			EditorGUILayout.PropertyField(m_textProperty, new GUIContent("Text (optional)"));
		}

		protected void DisplayIconProperty ()
		{
			EditorGUILayout.PropertyField(m_iconProperty, new GUIContent("Icon (optional)"));
		}

		protected void DisplayIsShiningProperty ()
		{
			EditorGUILayout.PropertyField(m_isShiningProperty, new GUIContent("Shining animation"));
		}

		protected void DisplayShineImageProperty ()
		{
			EditorGUILayout.PropertyField(m_shineImageProperty, new GUIContent("Shine Image"));
		}

		protected void DisplayShineStartDelayProperty ()
		{
			EditorGUILayout.PropertyField(m_shineStartDelayProperty, new GUIContent("Shine Start Delay"));
		}

		protected void DisplayShinePauseDurationProperty ()
		{
			EditorGUILayout.PropertyField(m_shinePauseDurationProperty, new GUIContent("Shine Pause Duration"));
		}

		protected void DisplayShineDurationProperty ()
		{
			EditorGUILayout.PropertyField(m_shineDurationProperty, new GUIContent("Shine Duration"));
		}

		protected void DisplayIsBouncingProperty ()
		{
			EditorGUILayout.PropertyField(m_isBouncingProperty, new GUIContent("Bouncing animation"));
		}

		protected void DisplayBounceStartDelayProperty ()
		{
			EditorGUILayout.PropertyField(m_bounceStartDelayProperty, new GUIContent("Bouncing Start Delay"));
		}

		protected void DisplayBouncePauseDurationProperty ()
		{
			EditorGUILayout.PropertyField(m_bouncePauseDuration, new GUIContent("Bouncing pause duration"));
		}

		protected void DisplayBounceDurationProperty ()
		{
			EditorGUILayout.PropertyField(m_bounceDurationProperty, new GUIContent("Bounce Duration"));
		}

		protected void DisplayBounceOnClickProperty()
		{
			EditorGUILayout.PropertyField(m_bounceOnClickProperty, new GUIContent("Bouncing on click"));
		}

		private void OnShiningValueChange (bool value)
		{
			if (value)
			{
				if (ReferenceEquals(button.gameObject.GetComponent<Mask>(), null))
				{
					button.gameObject.AddComponent<Mask>();
				}
			}
			else
			{
				if(!ReferenceEquals(button.gameObject.GetComponent<Mask>(), null))
				{
					Destroy(button.gameObject.GetComponent<Mask>());
				}
			}
		}

		protected void DisplayRepeatOnHoldProperty ()
		{
			EditorGUILayout.PropertyField(m_repeatOnHoldProperty, new GUIContent("Repeat On Hold"));
		}

		protected void DisplayHoldDelayProperty ()
		{
			EditorGUILayout.PropertyField(m_holdDelayProperty, new GUIContent("Hold Delay duration"));
		}

		protected void DisplayHoldFrequencyProperty ()
		{
			EditorGUILayout.PropertyField(m_holdFrequencyProperty, new GUIContent("Hold Frenquency Delay duration"));
		}

		protected void DisplayAdProperty ()
		{
			EditorGUILayout.PropertyField(m_adsButtonProperty, new GUIContent("Ads Button"));
		}
	}

}