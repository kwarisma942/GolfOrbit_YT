using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{

	[DisallowMultipleComponent]
	public class UILabelledValue: MonoBehaviour
	{
	
		[SerializeField] private Text m_label;
		[SerializeField] private Text m_value;

		public string label
		{
			set { m_label.text = value; }
		}

		public string value
		{
			set { m_value.text = value; }
		}

		public void SetActive ( bool isActive )
		{
			this.gameObject.SetActive(isActive);
		}

	}

}