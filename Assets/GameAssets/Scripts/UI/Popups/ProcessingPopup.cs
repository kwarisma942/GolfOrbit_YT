using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{

	public sealed class ProcessingPopup: MonoBehaviour
	{

		[SerializeField] private Text			m_message;
		[SerializeField] private GameObject		m_processIndicator;
	
		public string message
		{
			set { m_message.text = value; }
		}
		
	}

}