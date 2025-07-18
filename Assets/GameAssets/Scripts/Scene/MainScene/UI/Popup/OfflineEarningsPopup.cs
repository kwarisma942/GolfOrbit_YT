using DG.Tweening;
using Pinpin.UI;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{
	public class OfflineEarningsPopup : MonoBehaviour
	{
		[SerializeField] protected RectTransform m_panel;
		[SerializeField] protected ParticleSystem[] m_particles;
		[SerializeField] protected Image m_background;
		[SerializeField] private UICurrencyUpdater m_coinGainText;
		[SerializeField] private PushButton m_collectCoinsButton;
		[SerializeField] private PushButton m_collectCoinsRewardButton;
		[SerializeField] private Text m_multiplierNumberText;


		private ulong m_coinGain;
		private float m_panelContainerBaseYPosition;
		protected float m_panelContainerScreenYDelta;

		public Action onClose;
		private bool m_rewardedVideoRewarded;
		private bool m_hasCollectCoins;


		private void Awake ()
		{
			m_panelContainerBaseYPosition = m_panel.localPosition.y;
			m_panelContainerScreenYDelta = -m_panel.rect.height;
		}

		protected void Start ()
		{
			m_collectCoinsButton.onClick += OnCollectCoins;
			m_collectCoinsRewardButton.onClick += OnCollectRewardedClick;

			m_panel.localPosition = new Vector3(m_panel.localPosition.x, m_panelContainerScreenYDelta, 0f);
		}

		private void OnDestroy ()
		{
			m_collectCoinsButton.onClick -= OnCollectCoins;
			m_collectCoinsRewardButton.onClick -= OnCollectRewardedClick;
		}

		private void OnEnable ()
		{
			m_panel.localPosition = new Vector3(m_panel.localPosition.x, m_panelContainerScreenYDelta, 0f);
			m_panel.DOLocalMoveY(m_panelContainerBaseYPosition, 0.5f).SetEase(Ease.OutBack, 1.0f);
			m_collectCoinsRewardButton.isInteractable = true;
			if (GPlayPassManager.IsActive)
				m_collectCoinsRewardButton.gameObject.SetActive(false);
		}

		private MainSceneUIManager m_UIManager;

		public void Setup ( MainSceneUIManager uiManager )
		{
			m_UIManager = uiManager;
		}

		public void SetEarning ( ulong offlineEarling )
		{
			ApplicationManager.oeNeedToBeClaimed = true;
			m_hasCollectCoins = false;
			ulong oldValue = m_coinGain;
			m_coinGain = offlineEarling;
			m_coinGainText.UpdateValue(oldValue, m_coinGain);
			if (m_coinGain - oldValue > 0)
			{
				m_coinGainText.TriggerUpdate(3f);
				m_coinGainText.onAnimationEndEvent += EnableCollectButton;

				m_multiplierNumberText.gameObject.SetActive(false);

				m_collectCoinsButton.isInteractable = false;

				m_collectCoinsRewardButton.gameObject.SetActive(true);
			}
		}

		private void Close ()
		{
			if (onClose != null)
				onClose.Invoke();
		}

		public void PlayCloseAnimation ()
		{

			for (int i = 0; i < m_particles.Length; i++)
			{
				m_particles[i].Play();
			}
			m_panel.DOLocalMoveY(m_panelContainerScreenYDelta * 1f, 0.5f).SetEase(Ease.InBack).SetDelay(0.7f).onComplete += Close;

		}

		private void EnableCollectButton ()
		{
			m_collectCoinsButton.isInteractable = true;
			m_coinGainText.onAnimationEndEvent -= EnableCollectButton;
		}

		private void DisableCollectButtons ()
		{
			m_collectCoinsButton.isInteractable = false;
			m_collectCoinsRewardButton.isInteractable = false;
		}

		#region Collect x3

		private void OnCollectRewardedClick ()
		{
			if (ApplicationManager.datas.vip)
			{
				DisableCollectButtons();
				ApplicationManager.oeNeedToBeClaimed = false;
				AnimateRewardMutliplierText("x3", 3);
				return;
			}
			//else if (m_UIManager.sceneManager.ShowRewardedVideo(OnCollectVideoEnd))
			//{
			//	DisableCollectButtons();
			//	m_rewardedVideoRewarded = false;
			//}
			EGameEvent.Invoke(EEvents.RewardCollectOfflineRewardMultiply, new EData(this));
		}

		public void OnRewardedCollectOfflineRewardMultiply(bool succeed)
		{
			if (succeed)
			{
                ApplicationManager.oeNeedToBeClaimed = false;
				ApplicationManager.canTakeOfflineEarning = false;
				m_rewardedVideoRewarded = true;
				m_UIManager.sceneManager.rewardedShown = true;
				AnimateRewardMutliplierText("x3", 3);
				this.m_UIManager.CloseProcessingPopup();
			}
            else
			{
                m_collectCoinsButton.isInteractable = true;
				m_collectCoinsRewardButton.isInteractable = true;
				this.m_UIManager.CloseProcessingPopup();
			}
        }

		private IEnumerator WaitToCollectCoins ( float waitTime )
		{
			yield return new WaitForSeconds(waitTime);

			OnCollectCoins();

			yield return false;
		}

		private void OnCollectCoins ()
		{
			if (!m_hasCollectCoins)
			{
				m_hasCollectCoins = true;
				DisableCollectButtons();
				UISounds.PlayCoinGainSound();

				m_UIManager.sceneManager.AddCoins(m_coinGain);
				m_coinGain = 0;
				ApplicationManager.oeNeedToBeClaimed = false;
				ApplicationManager.canTakeOfflineEarning = false;
				for (int i = 0; i < m_particles.Length; i++)
				{
					m_particles[i].Play();
				}

				if (ApplicationManager.datas.isVibrationActive)
					MMVibrationManager.Haptic(HapticTypes.MediumImpact);

				PlayCloseAnimation();
			}
		}

		private void OnCollectVideoEnd ( /*AdsManagement.RewardedVideoEvent adEvent*/ )
		{
			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//		if (!m_rewardedVideoRewarded)
			//		{
			//			m_collectCoinsButton.isInteractable = true;
			//			m_collectCoinsRewardButton.isInteractable = true;
			//			this.m_UIManager.CloseProcessingPopup();
			//		}
			//		break;

			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		m_collectCoinsButton.isInteractable = true;
			//		m_collectCoinsRewardButton.isInteractable = true;
			//		this.m_UIManager.CloseProcessingPopup();
			//		break;

			//	case AdsManagement.RewardedVideoEvent.Rewarded:

			//		ApplicationManager.oeNeedToBeClaimed = false;
			//		ApplicationManager.canTakeOfflineEarning = false;
			//		m_rewardedVideoRewarded = true;
			//		m_UIManager.sceneManager.rewardedShown = true;
			//		AnimateRewardMutliplierText("x3", 3);
			//		this.m_UIManager.CloseProcessingPopup();
			//		break;

			//	default:
			//		break;
			//}
		}

		private void AnimateRewardMutliplierText ( string text, int multiplier )
		{
			m_multiplierNumberText.text = text;
			m_multiplierNumberText.gameObject.SetActive(true);
			m_multiplierNumberText.rectTransform.localScale = Vector3.one * 20.0f;
			m_multiplierNumberText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InSine).OnComplete(() => CollectCoinMultiplier(multiplier));
		}

		private void CollectCoinMultiplier ( int multiplier )
		{
			if (ApplicationManager.datas.isVibrationActive)
			{
				MMVibrationManager.Haptic(HapticTypes.LightImpact);
			}

			m_coinGainText.UpdateValue(m_coinGain, m_coinGain * (ulong)multiplier);
			m_coinGainText.TriggerUpdate(1f);
			StartCoroutine(WaitToCollectCoins(1f));
			m_coinGain *= (ulong)multiplier;
		}

		#endregion
	}
}
