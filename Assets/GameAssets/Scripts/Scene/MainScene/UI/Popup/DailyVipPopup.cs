using DG.Tweening;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{
	public class DailyVipPopup : AClosablePopup
	{
		[SerializeField] private PushButton m_closeAnimButton;
		[SerializeField] protected RectTransform m_panel;
		[SerializeField] protected ParticleSystem[] m_particles;
		[SerializeField] protected Image m_background;

		protected float m_panelContainerScreenYDelta;

		protected new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected virtual void Start ()
		{
			m_closeAnimButton.onClick += OnCloseClick;
			m_panelContainerScreenYDelta = -GetComponent<RectTransform>().rect.height;
			m_background.DOFade(0.45f, 0.5f).SetEase(Ease.OutQuint);
		}

		protected override void OnDestroy ()
		{
			m_closeAnimButton.onClick -= OnCloseClick;
			base.OnDestroy();
		}

		protected virtual void OnCloseClick ()
		{
			UIManager.sceneManager.UpdateCurrencies();

			for (int i = 0; i < m_particles.Length; i++)
			{
				m_particles[i].Play();
			}
			m_panel.DOLocalMoveY(m_panelContainerScreenYDelta, 0.5f).SetEase(Ease.InBack).SetDelay(0.7f).onComplete += Close;
			m_background.DOFade(0f, 0.5f).SetDelay(0.7f).SetEase(Ease.InQuint);
		}

	}

}
