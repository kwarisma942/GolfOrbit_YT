using DG.Tweening;
using Pinpin.UI;
using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public class JackpotPanel : AUIPanel
	{

		[SerializeField] private PushButton m_jackpotButton;
		[SerializeField] private PushButton m_collectButton;
		[SerializeField] private RectTransform m_panelContainer;
		[SerializeField] private RectTransform m_sunBurst;
		[SerializeField] private float m_jackpotAnimDuration;
		[SerializeField] private ParticleSystem m_jackpotParticles;
		[SerializeField] private MultiplierText m_multiplierPrefab;

		[SerializeField] private Color m_blueMultiplierBackgroundColor;
		[SerializeField] private Color m_greenMultiplierBackgroundColor;

		[Header("Before Jackpot Animation")]
		[SerializeField] private RectTransform m_jackpotStick;
		[SerializeField] private RectTransform m_jackpotBall;

		[Header("During Jackpot Animation")]
		[SerializeField] private RectTransform m_jackpotContainer;
		[SerializeField] private RectTransform m_multiplierTextContainer;
		[SerializeField] private ParticleSystem m_duringJackpotParticles;

		[Header("After Jackpot Animation")]
		[SerializeField] private Text m_finalJackpotMultiplier;
		[SerializeField] private Text m_gainText;
		[SerializeField] private ParticleSystem m_jackpotExplosion;
		[SerializeField] private Image m_endJackpotImage;
		private Color m_endJackpotImageBaseColor;


		[SerializeField] private Animator[] m_leftLightsAnim;
		[SerializeField] private Animator[] m_rightLightsAnim;

		private ulong m_lastEarnedCoins;

		private bool m_hasPlayedJackpot;
		private bool m_hasCollected;
		private int m_jackpotMultiplier = 10;
		private int jackpotMultiplier
		{
			get { return m_jackpotMultiplier; }
			set
			{
				m_finalJackpotMultiplier.text = "x" + value;
				m_jackpotMultiplier = value;
			}
		}

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		private void Start ()
		{
			m_endJackpotImageBaseColor = m_endJackpotImage.color;
			m_endJackpotImage.gameObject.SetActive(false);
			m_collectButton.transform.localScale = Vector3.zero;

			m_jackpotButton.onClick += WatchRewardedVideo;
			m_collectButton.onClick += OnCollectClick;
		}

		private void OnEnable ()
		{
			m_jackpotParticles.Play();
			m_jackpotButton.transform.localScale = Vector3.one;
			m_jackpotButton.gameObject.SetActive(true);

			if(m_multiplierPrefab != null)
			{
				StartCoroutine(GenerateMultiplierTexts());
			}

			m_hasPlayedJackpot = false;
			m_hasCollected = false;

			for (int i = 0; i < m_leftLightsAnim.Length; i++)
			{
				m_leftLightsAnim[i].SetInteger("LightIndex", i);
				m_rightLightsAnim[i].SetInteger("LightIndex", i);
				m_leftLightsAnim[i].SetFloat("Speed", 1f);
				m_rightLightsAnim[i].SetFloat("Speed", 1f);
				m_leftLightsAnim[i].SetBool("Stop", false);
				m_rightLightsAnim[i].SetBool("Stop", false);
			}

			m_panelContainer.localScale = Vector3.zero;
			m_panelContainer.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);

			m_sunBurst.localScale = Vector3.zero;
			m_sunBurst.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
			m_sunBurst.DORotate(new Vector3(0f, 0f, 1500f), 100f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);

			m_multiplierTextContainer.anchoredPosition = new Vector3(m_multiplierTextContainer.anchoredPosition.x, 0f);
			m_multiplierTextContainer.gameObject.SetActive(true);
			
			if (m_multiplierPrefab == null)
			{
				m_finalJackpotMultiplier.gameObject.SetActive(false);

				jackpotMultiplier = GetJackpotGain();
			}
			
		}

		protected override void OnDestroy ()
		{
			m_jackpotButton.onClick -= WatchRewardedVideo;
			m_collectButton.onClick -= OnCollectClick;

			base.OnDestroy();
		}

		private IEnumerator GenerateMultiplierTexts()
		{
			yield return new WaitForEndOfFrame(); //Wait rect transform setup

			foreach (Transform item in m_multiplierTextContainer.transform)
			{
				Destroy(item.gameObject);
			}
			float anchorHeight = (m_panelContainer.anchorMax.y - m_panelContainer.anchorMin.y) * GetComponent<RectTransform>().rect.height;
			float sizeDeltaNormalized = (m_panelContainer.sizeDelta.y + anchorHeight) / anchorHeight;

			for (int i = 0; i < 50; i++)
			{
				MultiplierText newMultiplierText = Instantiate(m_multiplierPrefab, m_multiplierTextContainer);
				newMultiplierText.color = i % 2 == 0 ? m_blueMultiplierBackgroundColor : m_greenMultiplierBackgroundColor;
				int rand = Random.Range(0, 100);
				int multiplier = ApplicationManager.config.game.GetJackpotMultiplier(rand);

				if (i == 1)
				{
					multiplier = jackpotMultiplier;
				}
				if (i == 48)
				{
					jackpotMultiplier = GetJackpotGain();
					multiplier = jackpotMultiplier;
				}

				newMultiplierText.text = "x" + multiplier.ToString();
				newMultiplierText.preferredHeight = newMultiplierText.preferredHeight * sizeDeltaNormalized;
			}
		}

		private void LaunchJackpot ()
		{
			m_jackpotParticles.Stop();
			m_duringJackpotParticles.Play();
			m_jackpotButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuad);

			for (int i = 0; i < m_leftLightsAnim.Length; i++)
			{
				m_leftLightsAnim[i].SetFloat("Speed", 2f);
				m_rightLightsAnim[i].SetFloat("Speed", 2f);
			}

			if(m_jackpotStick != null)
			{
				Sequence jackpotBar = DOTween.Sequence();
				jackpotBar.Insert(0f,m_jackpotStick.DOScaleY(-1f, 0.5f).SetEase(Ease.InOutCubic));
				jackpotBar.Insert(0f,m_jackpotBall.DOAnchorPosY(-m_jackpotBall.anchoredPosition.y * 12f, 0.5f).SetEase(Ease.InOutCubic));
				jackpotBar.SetLoops(2, LoopType.Yoyo);
			}

			m_jackpotContainer.DOShakeRotation(0.3f, new Vector3(0f, 0f, 13f), 10, 90).SetEase(Ease.InSine).SetDelay(0.8f).SetLoops(10).OnStepComplete(() => {
				if (ApplicationManager.datas.isVibrationActive)
					MMVibrationManager.Haptic(HapticTypes.LightImpact);
			});
			m_multiplierTextContainer.DOAnchorPosY(m_multiplierTextContainer.rect.size.y - 915, m_jackpotAnimDuration).SetEase(Ease.InBack, 0.5f).SetDelay(0.5f);

			m_sunBurst.DOKill();
			m_sunBurst.DORotate(new Vector3(0f, 0f, 600f), m_jackpotAnimDuration * 2, RotateMode.LocalAxisAdd);

			StartCoroutine(WaitforJackpotEnd());

			ApplicationManager.datas.jackpotProgression = 0;
			m_hasPlayedJackpot = true;
		}

		private IEnumerator WaitforJackpotEnd ()
		{
			yield return new WaitForSeconds(m_jackpotAnimDuration + 0.5f);

			JackpotEnd();

			yield return false;
		}

		private void JackpotEnd ()
		{
			m_endJackpotImage.gameObject.SetActive(true);
			m_endJackpotImage.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);

			m_jackpotExplosion.Play();
			m_jackpotParticles.Play();
			m_duringJackpotParticles.Stop();

			for (int i = 0; i < m_leftLightsAnim.Length; i++)
			{
				m_leftLightsAnim[i].SetFloat("Speed", 1f);
				m_rightLightsAnim[i].SetFloat("Speed", 1f);
			}

			m_jackpotContainer.DOKill();
			m_jackpotContainer.rotation = Quaternion.identity;

			if(m_multiplierPrefab == null)
			{
				m_finalJackpotMultiplier.gameObject.SetActive(true);
				m_finalJackpotMultiplier.rectTransform.DOPunchScale(Vector3.one * 0.5f, 1f, 5).SetLoops(-1);

				m_multiplierTextContainer.DOKill();
				m_multiplierTextContainer.gameObject.SetActive(false);
			}


			m_sunBurst.DOKill();
			m_sunBurst.localScale = Vector3.one;
			m_sunBurst.DORotate(new Vector3(0f, 0f, 1500f), 100f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1);


			m_gainText.text = (m_lastEarnedCoins * (ulong)jackpotMultiplier).ToString();
			m_gainText.transform.DOPunchScale(Vector3.one, 1.0f, 3);

			m_collectButton.gameObject.SetActive(true);
			m_collectButton.transform.DOScale(Vector3.one, 1.0f).SetEase(Ease.InQuad);
			m_collectButton.transform.DOPunchRotation(new Vector3(0, 0, 5f), 2.0f, 3, 0.5f).SetDelay(1.0f).SetLoops(-1);

			UISounds.PlayCongratulationSound();
		}

		private void OnCollectClick ()
		{
			m_collectButton.DOComplete();
			m_collectButton.DOKill();
			m_collectButton.gameObject.SetActive(false);
			
			m_panelContainer.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine).OnComplete(OnBackButtonPressed);
			m_sunBurst.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine);
		}

		private int GetJackpotGain ()
		{
			if (ApplicationManager.datas.isFirstJackpot)
			{
				ApplicationManager.datas.isFirstJackpot = false;
				return 30;
			}
			else
			{
				int rand = Random.Range(0, 100);
				return ApplicationManager.config.game.GetJackpotMultiplier(rand);
			}
		}

		private void WatchRewardedVideo ()
		{
			if (ApplicationManager.datas.vip)
			{
				m_jackpotButton.gameObject.SetActive(false);
				LaunchJackpot();
				return;
			}
			//else if (UIManager.sceneManager.ShowRewardedVideo(OnJackpotRewardedEnd))
			//{
			//	m_rewardedVideoRewarded = false;
			//	m_jackpotButton.gameObject.SetActive(false);
			//}
			OnRewardedJackpot(true);

            EGameEvent.Invoke(EEvents.RewardJackpot, new EData(this));
		}

		public void OnRewardedJackpot(bool succeed)
		{
			if (succeed)
			{
                m_rewardedVideoRewarded = true;
				UIManager.sceneManager.rewardedShown = true;
				this.UIManager.CloseProcessingPopup();
				LaunchJackpot();
			}
            else
			{
                m_jackpotButton.gameObject.SetActive(true);
				this.UIManager.CloseProcessingPopup();
			}
        }

		bool m_rewardedVideoRewarded = false;
		private void OnJackpotRewardedEnd ( /*AdsManagement.RewardedVideoEvent adEvent */)
		{

			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//		if (!m_rewardedVideoRewarded)
			//		{
			//			m_jackpotButton.gameObject.SetActive(true);
			//			this.UIManager.CloseProcessingPopup();
			//		}
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		m_jackpotButton.gameObject.SetActive(true);
			//		this.UIManager.CloseProcessingPopup();
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Rewarded:
			//		m_rewardedVideoRewarded = true;
			//		UIManager.sceneManager.rewardedShown = true;
			//		this.UIManager.CloseProcessingPopup();
			//		LaunchJackpot();
			//		break;
			//	default:
			//		break;
			//}
		}

		public void SetLastEarnedCoin ( ulong value )
		{
			m_lastEarnedCoins = value;
			m_gainText.text = value.ToString();
		}

		public override bool OnBackAction ()
		{
			OnClose();
			return base.OnBackAction();
		}

		protected override void OnBackButtonPressed ()
		{
			OnClose();
			base.OnBackButtonPressed();
		}

		private void OnClose ()
		{
			if (!m_hasCollected)
			{
				m_hasCollected = true;
				m_multiplierTextContainer.DOKill();
				m_multiplierTextContainer.gameObject.SetActive(false);

				m_endJackpotImage.color = m_endJackpotImageBaseColor;
				m_endJackpotImage.gameObject.SetActive(false);

				m_finalJackpotMultiplier.rectTransform.DOKill();
				m_finalJackpotMultiplier.rectTransform.localScale = Vector3.one;

				m_jackpotButton.transform.DOComplete();
				m_jackpotButton.transform.DOKill();
				m_jackpotButton.transform.localScale = Vector3.one;

				for (int i = 0; i < m_leftLightsAnim.Length; i++)
				{
					m_leftLightsAnim[i].SetBool("Stop", true);
					m_rightLightsAnim[i].SetBool("Stop", true);
				}

				m_collectButton.gameObject.SetActive(false);
				m_collectButton.transform.localScale = Vector3.zero;
				m_collectButton.transform.localRotation = Quaternion.identity;

				m_sunBurst.DOKill();
				m_sunBurst.localRotation = Quaternion.identity;
				m_sunBurst.localScale = Vector3.zero;

				for (int i = 0; i < m_leftLightsAnim.Length; i++)
				{
					m_leftLightsAnim[i].SetBool("Stop", true);
					m_rightLightsAnim[i].SetBool("Stop", true);
				}

				m_panelContainer.localScale = Vector3.zero;

				UIManager.GetPanel<EndGamePanel>().OnCloseJackpotPopup(jackpotMultiplier, m_hasPlayedJackpot);
			}
		}
	}

}

