using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public abstract class AShopCard : MonoBehaviour
	{

		public enum State
		{
			Locked,
			Unlocked,
			Selected,
			LockedSelected,
		}

		public enum Type
		{
			Normal,
			Premium,
			Instagram
		}

		[SerializeField] protected Button m_selectButton;
		[SerializeField] protected Image m_skinImage;
		[SerializeField] private GameObject m_selectTickMark;
		[SerializeField] private GameObject m_lock;
		[SerializeField] private Image m_selectOutline;
		[SerializeField] private Image m_background;
		[SerializeField] private Image m_stateBackground;
		[SerializeField] private Image m_premiumIcon;
		[SerializeField] private Image m_instagramIcon;
		[SerializeField] protected Image m_goldImage;
		[SerializeField] private Color m_backgroundLockColor;
		[SerializeField] private Color m_backgroundUnlockColor;
		[SerializeField] private Color m_backgroundTickColor;
		[SerializeField] private Color m_backgroundBoldColor;
		[SerializeField] private Material m_lockedMaterial;

		protected Type m_type = Type.Normal;
		protected int m_id = 0;

		public Action<int, Type> onSelect;

		private void Awake ()
		{
			m_selectButton.onClick.AddListener(OnSelectButtonPressed);
		}

		private void OnDestroy ()
		{
			m_selectButton.onClick.RemoveListener(OnSelectButtonPressed);
		}

		public abstract void Configure ( int skinId, Type type = Type.Normal);

		public void SetState ( State state )
		{
			m_lock.SetActive(state == State.Locked || state == State.LockedSelected);
			m_skinImage.material = (state == State.Locked || state == State.LockedSelected) ? m_lockedMaterial : null;
			m_selectOutline.enabled = state == State.Selected || state == State.LockedSelected;
			m_background.color = (state == State.Locked || state == State.LockedSelected) ? m_backgroundLockColor : m_backgroundUnlockColor;
			m_selectTickMark.SetActive(state == State.Selected);
			m_stateBackground.color = state == State.Locked ? m_backgroundBoldColor : m_backgroundTickColor;

			if (m_premiumIcon != null)
				m_premiumIcon.gameObject.SetActive(false);
			if (m_instagramIcon != null)
				m_instagramIcon.gameObject.SetActive(false);
			switch (m_type)
			{
				case Type.Normal:
					break;
				case Type.Premium:
					if(m_premiumIcon != null)
						m_premiumIcon.gameObject.SetActive(true);
					break;
				case Type.Instagram:
					if (m_instagramIcon != null)
						m_instagramIcon.gameObject.SetActive(true);
					break;
				default:
					break;
			}
		}

		private void OnSelectButtonPressed ()
		{
			if (onSelect != null)
				onSelect.Invoke(m_id, m_type);
		}

	}

}