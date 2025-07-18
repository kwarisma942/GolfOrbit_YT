using DG.Tweening;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;
using I2LocManager = I2.Loc.LocalizationManager;

namespace Pinpin.Scene.MainScene.UI
{
	public class OrbitPanel : AUIPanel
	{
		[SerializeField] private RectTransform m_panelContainer;
		[SerializeField] private RectTransform m_sunBurst;
		//[SerializeField] private Text m_nextWorldNameText;
		[SerializeField] private Image m_nextWorldImage;
		[SerializeField] private PushButton m_nextWorldButton;
		[SerializeField] private PushButton m_currentWorldButton;

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			base.Awake();
		}

		private void Start ()
		{
			m_nextWorldButton.onClick += OnGoClick;
			m_currentWorldButton.onClick += OnStayClick;
		}

		protected override void OnDestroy ()
		{
			m_nextWorldButton.onClick -= OnGoClick;
			m_currentWorldButton.onClick -= OnStayClick;
			base.OnDestroy();
		}

		private void OnEnable ()
		{
			m_nextWorldButton.isInteractable = true;
			m_currentWorldButton.isInteractable = true;
			
			float scaleTime = 1.5f;

			m_panelContainer.localScale = Vector3.zero;
			m_panelContainer.DOScale(Vector3.one, scaleTime).SetEase(Ease.InQuad);
			m_panelContainer.DORotate(Vector3.forward * 1440f, scaleTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).onComplete += () => UISounds.PlayCongratulationSound(); 
			m_sunBurst.localScale = Vector3.zero;
			m_sunBurst.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack, 3f).SetDelay(scaleTime);

			m_sunBurst.DORotate(Vector3.forward * -360f, 20f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
			
			if (ApplicationManager.datas.selectedWorldId + 1 < ApplicationManager.assets.planets.Length)
			{
				/*m_nextWorldNameText.text = "You can still go to the " + ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId + 1].name
					+ " whenever you want by going into the clubhouse !";*/
				m_nextWorldImage.sprite = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId + 1].orbitButtonSprite;
				/*if (ApplicationManager.config.game.testOldUI)
				{
					m_nextWorldButton.text = "Go to the " + I2LocManager.GetTermTranslation(ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId + 1].name);
					m_currentWorldButton.text = "Stay on " + I2LocManager.GetTermTranslation(ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].name);
				}*/
			}
		}

		private void OnStayClick ()
		{
			CloseAnimation(UIManager.sceneManager.OnWorldStay);
		}

		private void OnGoClick ()
		{
			CloseAnimation(UIManager.sceneManager.OnNextWorld);
		}

		private void CloseAnimation ( TweenCallback onCloseEvent )
		{
			m_nextWorldButton.isInteractable = false;
			m_currentWorldButton.isInteractable = false;
			
			m_panelContainer.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine).onComplete = onCloseEvent;
			m_sunBurst.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine);
		}
		public override bool OnBackAction ()
		{
			return false;
		}
	}
}
