using DG.Tweening;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{
	public class NewWorldPopup : AClosablePopup
	{

		[SerializeField] private RectTransform m_panelContainer;
		[SerializeField] private RectTransform m_sunBurst;
		[SerializeField] private Image m_worldOverviewImage;
		[SerializeField] private PushButton m_closeAnimButton;

		private void Start ()
		{
			m_closeAnimButton.onClick += OnCloseClick;
		}

		protected override void OnDestroy ()
		{
			m_closeAnimButton.onClick -= OnCloseClick;
			base.OnDestroy();
		}

		protected override void OnEnable ()
		{
			m_panelContainer.localScale = Vector3.zero;
			m_panelContainer.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);

			m_sunBurst.localScale = Vector3.zero;
			m_sunBurst.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
			m_sunBurst.DORotate(new Vector3(0f, 0f, 1500f), 100f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);

			if(ApplicationManager.datas.selectedWorldId + 1 < ApplicationManager.assets.planets.Length)
			{
				m_worldOverviewImage.sprite = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId + 1].shopPreview;
			}

			base.OnEnable();
		}

		private void OnCloseClick ()
		{
			m_panelContainer.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine).OnComplete(this.Close);
			m_sunBurst.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine);
		}
	}

}
