using DG.Tweening;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;
using MobileJoyPad;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using LocManager = I2.Loc.LocalizationManager;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class GamePanel : AUIPanel
	{
		[SerializeField] private ShotArea m_shotArea;

		[Header("Hole Feedbacks")]
		[SerializeField] private WordAnimator m_wordAnimator;
		[SerializeField] private string m_birdieText;
		[SerializeField] private Color m_birdieColor = Color.white;
		[SerializeField] private Image m_birdieImage;
		private Vector3 m_birdieImageBaseScale;
		[SerializeField] private string m_eagleText;
		[SerializeField] private Color m_eagleColor = Color.white;
		[SerializeField] private Image m_eagleImage;
		private Vector3 m_eagleImageBaseScale;
		[SerializeField] private string m_albatrosText;
		[SerializeField] private Color m_albatrosColor = Color.white;
		[SerializeField] private Image m_albatrosImage;
		private Vector3 m_albatrosImageBaseScale;
		[SerializeField] private Image m_sunBurstImage;
		private Vector3 m_sunBurstImageBaseScale;
		[SerializeField] private GameManager.HoleScores testAnim;
		private Sequence holeSequence;
		[SerializeField] private ParticleSystem m_fireworksParticles;

		[Header("One More Chance")]
		[SerializeField] private PushButton m_oneMoreChanceButton;
		[SerializeField] private PushButton m_giveUpButton;
		[SerializeField] private float m_giveUpTime = 3f;

		[Header("Tuto")]
		[SerializeField] private RectTransform m_greenTutoContainer;
		[SerializeField] private Image m_greenTutoThumb;
		private TutoThumbMovement m_tutoThumbMovement;

		private Coroutine m_tutoThumbCoroutine;

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			base.Awake();

            m_shotArea = FindObjectOfType<ShotArea>();

            m_birdieImageBaseScale = m_birdieImage.rectTransform.localScale;
			m_eagleImageBaseScale = m_eagleImage.rectTransform.localScale;
			m_albatrosImageBaseScale = m_albatrosImage.rectTransform.localScale;
			m_sunBurstImageBaseScale = m_sunBurstImage.rectTransform.localScale;
			holeSequence = DOTween.Sequence();
			m_giveUpButton.gameObject.SetActive(false);
			m_oneMoreChanceButton.gameObject.SetActive(false);

			m_tutoThumbMovement = m_greenTutoThumb.gameObject.GetComponent<TutoThumbMovement>();
		}

		private void OnEnable ()
		{
			Color baseCol = Color.white;
			baseCol.a = 0.0f;
			m_birdieImage.color = baseCol;
			m_birdieImage.rectTransform.localScale = m_birdieImageBaseScale;

			m_eagleImage.rectTransform.localScale = m_eagleImageBaseScale;
			m_eagleImage.color = baseCol;
			m_albatrosImage.rectTransform.localScale = m_albatrosImageBaseScale;
			m_albatrosImage.color = baseCol;

			m_sunBurstImage.rectTransform.localScale = m_sunBurstImageBaseScale;
			m_sunBurstImage.transform.rotation = Quaternion.identity;

			m_greenTutoThumb.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			m_greenTutoThumb.gameObject.SetActive(false);

			m_giveUpButton.onClick += StopGame;
			m_giveUpButton.gameObject.SetActive(false);
		}

		protected override void OnDisable()
		{
			m_giveUpButton.gameObject.SetActive(false);
			m_oneMoreChanceButton.gameObject.SetActive(false);

			m_oneMoreChanceButton.onClick -= WatchVideo;
			m_shotArea.onPress.RemoveListener(FadeTuto);

			base.OnDisable();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		public override bool OnBackAction ()
		{
#if DEBUG
			UIManager.sceneManager.StopGame();
#endif
			return false;
		}
		public void OnFirstGreenLanding ()
		{
			m_shotArea.onPress.AddListener(FadeTuto);
			m_greenTutoContainer.localScale = UIManager.sceneManager.GetGolferDirection();
			m_tutoThumbCoroutine = StartCoroutine(TutoGreenShot());
		}


		private IEnumerator TutoGreenShot ()
		{
			yield return new WaitForSeconds(1.0f);
			m_greenTutoThumb.gameObject.SetActive(true);
			m_greenTutoThumb.DOFade(1.0f, 0.5f);

			yield return new WaitForEndOfFrame();

			Vector2 baseThumbPosition = m_greenTutoThumb.rectTransform.localPosition;

			while (!InputManager.IsMoving)
			{
				Vector2 axis = (baseThumbPosition - (Vector2)m_greenTutoThumb.rectTransform.localPosition) * UIManager.transform.localScale.x * 0.1f;
				axis.Scale(m_greenTutoContainer.localScale);

				UIManager.sceneManager.TutoGreenShot(axis, m_tutoThumbMovement.isThumbDown);

				yield return null;
			}

			m_greenTutoThumb.DOFade(0.0f, 0.1f);

			yield return false;
		}

		private void FadeTuto ()
		{
			if(m_tutoThumbCoroutine != null)
			{
				StopCoroutine(m_tutoThumbCoroutine);
			}

			m_greenTutoThumb.DOKill();
			m_greenTutoThumb.DOComplete();
			m_greenTutoThumb.DOFade(0.0f, 0.1f);
			m_shotArea.onPress.RemoveListener(FadeTuto);
		}

#region one more chance

		public void OnOtherGreenLanding()
		{
			m_giveUpButton.gameObject.SetActive(true);
			//m_oneMoreChanceButton.gameObject.SetActive(true);
			if (GPlayPassManager.IsActive)
            {
				m_oneMoreChanceButton.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
				m_oneMoreChanceButton.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text="Retry";

			}
			m_giveUpButton.Hide(0f);
			m_oneMoreChanceButton.Hide(0f);

			if (ApplicationManager.networkReachable || ApplicationManager.datas.vip)
			{
				m_oneMoreChanceButton.Show(0.5f);
				StartCoroutine(ShowButton(m_giveUpButton, m_giveUpTime, 0.5f));
				m_oneMoreChanceButton.onClick += WatchVideo;
			}
			else
			{
				m_giveUpButton.Show(0.5f);
				m_oneMoreChanceButton.Hide(0f);
			}
		}

		public void WatchVideo()
		{
			StopAllCoroutines();

			if (ApplicationManager.datas.vip || GPlayPassManager.IsActive)
			{
				m_oneMoreChanceButton.onClick -= WatchVideo;
				OnOneMoreChance();
				return;
			}
			//else if (UIManager.sceneManager.ShowRewardedVideo(OnRewardedVideoEnd))
			//{
			//	m_oneMoreChanceButton.onClick -= WatchVideo;
			//	m_rewardedVideoRewarded = false;
			//}
			EGameEvent.Invoke(EEvents.RewardOneMoreChance, new EData(this));
		}

		public void OnRewardOneMoreChance(bool succeed)
		{
			if (succeed)
			{
                m_rewardedVideoRewarded = true;
				UIManager.sceneManager.rewardedShown = true;
				OnOneMoreChance();
				//LoadingScreen.Hide();
				this.UIManager.CloseProcessingPopup();
			}
			else
			{
				//LoadingScreen.Hide();
				this.UIManager.CloseProcessingPopup();
				OnOtherGreenLanding();
			}
        }

		public void OnOneMoreChance()
		{
			m_oneMoreChanceButton.Hide(0.2f);
			m_giveUpButton.Hide(0.2f);

			UIManager.sceneManager.OneMoreChance();
		}

        private void StopGame()
        {
            UIManager.sceneManager.stoppedByGiveUp = true; // Set the flag
            UIManager.sceneManager.StopGame();
            m_giveUpButton.onClick -= StopGame;
        }


        bool m_rewardedVideoRewarded = false;
		private void OnRewardedVideoEnd(/*AdsManagement.RewardedVideoEvent adEvent */)
		{
			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//		if (!m_rewardedVideoRewarded)
			//		{
			//			//LoadingScreen.Hide();
			//			this.UIManager.CloseProcessingPopup();
			//			OnOtherGreenLanding();
			//		}
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		//LoadingScreen.Hide();
			//		this.UIManager.CloseProcessingPopup();
			//		OnOtherGreenLanding();
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Rewarded:
			//		m_rewardedVideoRewarded = true;
			//		UIManager.sceneManager.rewardedShown = true;
			//		OnOneMoreChance();
			//		//LoadingScreen.Hide();
			//		this.UIManager.CloseProcessingPopup();
			//		break;
			//	default:
			//		break;
			//}
		}

		private IEnumerator ShowButton(PushButton button, float waitTime, float fadeTime)
		{
			yield return new WaitForSeconds(waitTime);

			button.Show(fadeTime);

			yield return false;
		}

		private IEnumerator HideButton(PushButton button, float waitTime, float fadeTime )
		{
			yield return new WaitForSeconds(waitTime);

			button.Hide(fadeTime);

			yield return false;
		}

#endregion

		IEnumerator WaitHoleTest ()
		{
			yield return new WaitForSeconds(0.2f);

			OnHoleLanding(testAnim);

			yield return false;
		}

		public void OnHoleLanding (GameManager.HoleScores score)
		{
			Image animation = m_birdieImage;
			switch (score)
			{
				case GameManager.HoleScores.Birdie:
					m_wordAnimator.AnimateWordsSequence(Regex.Split(I2.Loc.ScriptLocalization.birdie, string.Empty), m_birdieColor, 0.05f, 1.0f, out holeSequence);
					break;
				case GameManager.HoleScores.Eagle:
					m_wordAnimator.AnimateWordsSequence(Regex.Split(I2.Loc.ScriptLocalization.eagle, string.Empty), m_eagleColor, 0.05f, 1.0f, out holeSequence);
					animation = m_eagleImage;
					holeSequence.Insert(0.8f, m_sunBurstImage.rectTransform.DOScale(Vector3.one * 3.0f, 0.5f).SetEase(Ease.OutCirc));
					break;
				case GameManager.HoleScores.Albatros:
					m_wordAnimator.AnimateWordsSequence(Regex.Split(I2.Loc.ScriptLocalization.albatros, string.Empty), m_albatrosColor, 0.05f, 1.0f, out holeSequence);
					animation = m_albatrosImage;
					holeSequence.Insert(0.8f, m_sunBurstImage.rectTransform.DOScale(Vector3.one * 3.0f, 0.8f).SetEase(Ease.OutBack));
					holeSequence.Insert(0.8f, m_sunBurstImage.rectTransform.DORotate(Vector3.forward * 30.0f, 6.0f));
					StartCoroutine(PlayFireworks(0.3f));
					break;
				default:
					break;
			}
			UISounds.PlayBirdSound(score);

			holeSequence.Insert(0.8f, animation.DOFade(1.0f, 0.3f).SetEase(Ease.InCirc));
			holeSequence.Insert(0.8f, animation.rectTransform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.InCirc));
			holeSequence.Insert(1.25f, animation.rectTransform.DOPunchRotation(Vector3.forward * 20.0f, 0.5f));
		}

		IEnumerator PlayFireworks(float delay )
		{
			yield return new WaitForSeconds(delay);

			m_fireworksParticles.Play();

			yield return false;
		}
	}

}