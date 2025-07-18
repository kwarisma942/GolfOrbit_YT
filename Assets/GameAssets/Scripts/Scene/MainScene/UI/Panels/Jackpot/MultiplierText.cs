using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierText : MonoBehaviour
{
	[SerializeField] private Image m_image;
	[SerializeField] private Text m_text;
	[SerializeField] private LayoutElement m_layoutElement;
	public Color color { set { m_image.color = value; } }
	public string text { set { m_text.text = value; } }
	public float preferredHeight { get { return m_layoutElement.preferredHeight; } set { m_layoutElement.preferredHeight = value; } }
}
