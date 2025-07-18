using UnityEngine;
using UnityEngine.Events;

namespace Pinpin.UI
{

	public abstract class AConfirmPopup: AClosablePopup
	{

		[SerializeField] private PushButton	m_acceptButton;
		[SerializeField] private PushButton	m_declineButton;

		public event UnityAction onConfirm;
		public event UnityAction onDecline;

		protected PushButton declineButton
		{
			get { return (m_declineButton); }
		}
		
		protected PushButton confirmButton
		{
			get { return (m_acceptButton); }
		}

		protected override void Awake ()
		{
			base.Awake();
			m_acceptButton.onClick += OnAcceptButtonPressed;
			m_declineButton.onClick += OnDeclineButtonPressed;
		}

		protected override void OnDestroy ()
		{
			m_acceptButton.onClick -= OnAcceptButtonPressed;
			m_declineButton.onClick -= OnDeclineButtonPressed;
			base.OnDestroy();
		}

		protected override void OnDisable ()
		{
			this.onConfirm = null;
			this.onDecline = null;
			base.OnDisable();
		}

		protected virtual void OnAcceptButtonPressed ()
		{
			if (this.onConfirm != null)
				this.onConfirm.Invoke();
			//this.onConfirm = null;
		}

		protected virtual void OnDeclineButtonPressed ()
		{
			if (this.onDecline != null)
				this.onDecline.Invoke();
			this.onDecline = null;
		}

		protected sealed override void OnClose ()
		{
			this.OnDeclineButtonPressed();
			base.OnClose();
		}

	}

}