using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pinpin.UI
{
	public class GDPRPopup : MonoBehaviour
	{
		public Action<bool> onGDPRAnswered;
		[SerializeField] PushButton m_acceptButton;
		[SerializeField] PushButton m_declineButton;
		[SerializeField] PushButton m_privacyPolicyLinkButton;
		[SerializeField] string m_privacyPolicyLink;
		
		void Start ()
		{
			m_acceptButton.onClick += OnAcceptButtonClicked;
			m_declineButton.onClick += OnDeclineButtonClicked;
			m_privacyPolicyLinkButton.onClick += OnPrivacyPolicyButtonClicked;
		}

		private void OnDestroy ()
		{
			m_acceptButton.onClick -= OnAcceptButtonClicked;
			m_declineButton.onClick -= OnDeclineButtonClicked;
			m_privacyPolicyLinkButton.onClick -= OnPrivacyPolicyButtonClicked;
		}

		private void OnAcceptButtonClicked ()
		{
			onGDPRAnswered?.Invoke(true);
		}

		private void OnDeclineButtonClicked ()
		{
			onGDPRAnswered?.Invoke(false);
		}

		private void OnPrivacyPolicyButtonClicked ()
		{
			Application.OpenURL(m_privacyPolicyLink);
		}
	}
}