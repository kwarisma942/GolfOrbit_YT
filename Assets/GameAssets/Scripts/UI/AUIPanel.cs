using System;
using UnityEngine;

namespace Pinpin.UI
{

	[DisallowMultipleComponent,
	DefaultExecutionOrder(-45)]
	public abstract class AUIPanel: MonoBehaviour
	{

		[SerializeField] private PushButton	m_backButton;

		protected AUIManager UIManager { get; private set; }

		protected virtual void Awake ()
		{
			if (m_backButton != null)
				m_backButton.onClick += this.OnBackButtonPressed;
		}

		protected virtual void OnDestroy ()
		{
			if (m_backButton != null)
				m_backButton.onClick -= this.OnBackButtonPressed;
		}

		protected virtual void OnDisable ()
		{
		}

		public void SetUIManager ( AUIManager manager )
		{
			this.UIManager = manager;
		}

		protected virtual void OnBackButtonPressed ()
		{
			this.UIManager.Back();
		}

		public new virtual Type GetType ()
		{
			return (base.GetType());
		}

		/// <summary>
		/// Called when Back is requiered.
		/// Return false if cannot perform the back action
		/// </summary>
		public virtual bool OnBackAction ()
		{
			return (true);
		}
		
	}

}