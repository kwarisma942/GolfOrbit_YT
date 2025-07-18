using UnityEngine;
using UnityEngine.Events;

namespace Pinpin.UI
{

	[DisallowMultipleComponent]
	public abstract class AUIPopup: MonoBehaviour
	{

		public static event UnityAction<AUIPopup>	onPopupOpened;
		public event UnityAction<AUIPopup>			onPopupClosed;

		protected AUIManager UIManager { get; private set; }

		protected virtual void OnEnable ()
		{
			if (AUIPopup.onPopupOpened != null)
				AUIPopup.onPopupOpened.Invoke(this);
		}

		protected virtual void OnDisable ()
		{
			if (this.onPopupClosed != null)
				this.onPopupClosed.Invoke(this);
		}

		public void SetUIManager ( AUIManager manager )
		{
			this.UIManager = manager;
		}

		public virtual bool IsClosable ()
		{
			return (false);
		}

		public virtual void Close ()
		{
			this.OnClose();
			this.gameObject.SetActive(false);
		}

		protected virtual void OnClose () {}
		
	}

}