using UnityEngine;
using Pinpin.UI;
using UnityEngine.UI;
using Pinpin.Helpers;
using DG.Tweening;
using System.Collections;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class ChristmasPanel : AUIPanel
	{
		[SerializeField] private PushButton m_getRewardButton;
		[SerializeField] private RectTransform m_panel;
		[SerializeField] private ParticleSystem m_particles;
		[SerializeField] private Text m_coinText;
		private bool m_rewardedVideoRewarded;

		private float m_panelContainerBaseYPosition;
		private float m_panelContainerScreenYDelta;
		private ulong m_reward;


		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			base.Awake();
			m_panelContainerBaseYPosition = m_panel.localPosition.y;
			m_panelContainerScreenYDelta = -m_panel.rect.height;
		}

		private void Start ()
		{
			m_getRewardButton.onClick += OnGetRewardButtonPressed;
			GameConfig.GameSettings gamesettings = ApplicationManager.config.game;
			m_reward = ((gamesettings.GetUpgradePrice(ApplicationManager.datas.GetUpgradeLevel(UIShopButton.UpgradeType.Strength)) +
						 gamesettings.GetUpgradePrice(ApplicationManager.datas.GetUpgradeLevel(UIShopButton.UpgradeType.Speed)) +
						 gamesettings.GetUpgradePrice(ApplicationManager.datas.GetUpgradeLevel(UIShopButton.UpgradeType.Bounce))) * 2 / 50000) + 1 * 50000;
			m_coinText.text = MathHelper.ConvertToEgineeringNotation(m_reward);
			
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy();
			m_getRewardButton.onClick -= OnGetRewardButtonPressed;
		}

		private void OnEnable ()
		{
			m_panel.localPosition = new Vector3(m_panel.localPosition.x, m_panelContainerScreenYDelta, 0f);
			m_panel.DOLocalMoveY(m_panelContainerBaseYPosition, 0.5f).SetEase(Ease.OutBack, 1.0f);
		}
		
		protected override void OnDisable ()
		{
			base.OnDisable();
			ApplicationManager.datas.lastChristmasEventSeenTime = ApplicationManager.utcLaunchTime;
		}

		public void PlayCloseAnimation ()
		{
			UIManager.sceneManager.UpdateCurrencies();

			m_particles.Play();
			m_panel.DOLocalMoveY(m_panelContainerScreenYDelta * 1f, 0.5f).SetEase(Ease.InBack).SetDelay(1f).onComplete += OnBackButtonPressed;

		}

		private void OnGetRewardButtonPressed()
		{
			if (ApplicationManager.datas.vip)
			{
				ApplicationManager.datas.coins += m_reward;
				ApplicationManager.datas.diamonds += 100;
				ApplicationManager.datas.AddBalls(3, 3);
				ApplicationManager.datas.selectedBallId = 3;
				ApplicationManager.SaveGameDatas();
				UIManager.sceneManager.UpdateBall();
				PlayCloseAnimation();
				ApplicationManager.datas.lastChristmasEventSeenTime = ApplicationManager.utcLaunchTime;
			}
			//else if (UIManager.sceneManager.ShowRewardedVideo(OnCollectVideoEnd))
			//{
			//	m_rewardedVideoRewarded = false;
			//}
			//TODO: REWARDED event
		}

		private void OnCollectVideoEnd (/* AdsManagement.RewardedVideoEvent adEvent */)
		{
			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//		if (!m_rewardedVideoRewarded)
			//		{
			//			UIManager.CloseProcessingPopup();
			//		}
			//		break;

			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		UIManager.CloseProcessingPopup();
			//		break;

			//	case AdsManagement.RewardedVideoEvent.Rewarded:

			//		m_rewardedVideoRewarded = true;
			//		UIManager.sceneManager.rewardedShown = true;
			//		ApplicationManager.datas.coins += m_reward;
			//		ApplicationManager.datas.diamonds += 100;
			//		ApplicationManager.datas.AddBalls(3, 3);
			//		ApplicationManager.datas.selectedBallId = 3;
			//		ApplicationManager.SaveGameDatas();
			//		UIManager.sceneManager.UpdateBall();
			//		UIManager.CloseProcessingPopup();
			//		PlayCloseAnimation();
			//		ApplicationManager.datas.lastChristmasEventSeenTime = ApplicationManager.utcLaunchTime;
			//		break;

			//	default:
			//		break;
			//}
		}

	}

}