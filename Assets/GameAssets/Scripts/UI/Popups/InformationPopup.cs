using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{

	public sealed class InformationPopup: AClosablePopup
	{

		[SerializeField] private Text m_message;
		[SerializeField] private Text m_title;

		public string title
		{
			set { m_title.text = value; }
		}

		public string message
		{
			set { m_message.text = value; }
		}
		
	}

}