using UnityEngine;
using UnityEngine.Events;

namespace Pinpin.UI
{

	public abstract class AClosablePopup: AUIPopup
	{

		[SerializeField] protected PushButton	m_closeButton;

		public event UnityAction onClosed;

		protected PushButton closeButton
		{
			get { return (m_closeButton); }
		}

		protected virtual void Awake ()
		{
			if (m_closeButton != null)
				m_closeButton.onClick += this.OnCloseButtonPressed;
		}

		protected virtual void OnDestroy ()
		{
			if (m_closeButton != null)
				m_closeButton.onClick -= this.OnCloseButtonPressed;
		}

		public override bool IsClosable ()
		{
			return (true);
		}

		protected virtual void OnCloseButtonPressed ()
		{
			this.Close();
		}

		protected override void OnClose ()
		{
			if (this.onClosed != null)
				this.onClosed.Invoke();
			this.onClosed = null;
		}
		
	}

}