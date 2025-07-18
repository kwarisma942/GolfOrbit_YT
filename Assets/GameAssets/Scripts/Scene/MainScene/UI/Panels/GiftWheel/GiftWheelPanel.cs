using DG.Tweening;
using Pinpin.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;
using URandom = UnityEngine.Random;

namespace Pinpin.Scene.MainScene.UI
{

	public class GiftWheelPanel : AUIPanel
	{
		[SerializeField] private PushButton m_closeButton;
		[SerializeField] private RectTransform m_container;
		[SerializeField] private PushButton m_spinButton;
		[SerializeField] private PushButton m_collectButton;
		[SerializeField] private PushButton m_respinButton;
		[SerializeField] private Text m_timeRemainingText;
		[SerializeField] private GameObject m_wheelHide;
		[SerializeField] private RectTransform m_giftWheel;
		[SerializeField] private RectTransform m_sunBurst;
		[SerializeField] private RectTransform m_closeButtonContainer;

		[SerializeField] private Image m_background;
		
		[SerializeField] private Animator[] m_leftLightsAnim;
		[SerializeField] private Animator[] m_rightLightsAnim;
		
		[SerializeField] private GameObject m_wheelGiftParent;
		private GiftWheelCard[] m_wheelGifts;
		[SerializeField] private Sprite m_ballSprite;
		[SerializeField] private Sprite m_goldSprite;
		[SerializeField] private Sprite m_diamondSprite;
		[SerializeField] private AnimationCurve m_spinCurve;
		[SerializeField] private ParticleSystem m_giftParticles;

		[SerializeField] private GiftWheelCard m_endWheelGift;


        private bool m_isRewarded;
		private int m_giftIndex;
		private bool m_hasSpin;
		private bool m_firstSpin = true;

		private float m_closeButtonStartPos;

		private Coroutine m_checkFreeGiftCoroutine;

		private Sequence m_openSequence;
		private Sequence m_endSpinWheelSequence;
		private Sequence m_endSpinGiftSequence;
		private MainPanel mp;
        private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}
		
		protected override void Awake ()
		{
			m_closeButtonStartPos = m_closeButtonContainer.anchoredPosition.x;
			float delay = 0.05f;
			m_openSequence = DOTween.Sequence();
			m_openSequence.Insert(0f, m_background.DOFade(0f, 0.5f).SetEase(Ease.OutQuint).From());
			m_openSequence.Insert(0f, m_sunBurst.DOScale(0, 0.5f).SetEase(Ease.OutBack).From());
			m_openSequence.Insert(0f, m_closeButtonContainer.DOAnchorPosX(m_closeButtonStartPos + 500f, 0.5f).SetEase(Ease.OutQuad).From());
			m_openSequence.Insert(delay, m_spinButton.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack).From());
			//m_openSequence.Insert(delay, m_respinButton.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack).From());
			m_openSequence.Pause();
			m_openSequence.SetAutoKill(false);

			m_endSpinWheelSequence = DOTween.Sequence();
			m_endSpinWheelSequence.Append(m_spinButton.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack));
			m_endSpinWheelSequence.Insert(delay, m_sunBurst.DOScale(0f, 0.5f).SetEase(Ease.InBack));
			m_endSpinWheelSequence.Pause();
			m_endSpinWheelSequence.SetAutoKill(false);

			m_endSpinGiftSequence = DOTween.Sequence();
			m_endSpinGiftSequence.Append(m_collectButton.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
			m_endSpinGiftSequence.Insert(delay, m_endWheelGift.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
			m_endSpinGiftSequence.Insert(delay, m_sunBurst.DOScale(1, 0.5f).SetEase(Ease.OutBack));
			m_endSpinGiftSequence.Pause();
			m_endSpinGiftSequence.SetAutoKill(false);

			m_spinButton.onClick += Spin;
			m_collectButton.onClick += Collect;
			//m_respinButton.isInteractable = false;
			m_collectButton.isInteractable = false;
			//m_respinButton.onClick += Respin;

            mp = FindObjectOfType<MainPanel>();
            base.Awake();
		}

		private void Start ()
		{
		//	m_wheelGifts = m_wheelGiftParent.GetComponentsInChildren<GiftWheelCard>();
		//	float ysizeDelta = m_container != null ? m_container.sizeDelta.y : -1;

		//	float angle = 22.5f;
		//	for (int i = 0; i < m_wheelGifts.Length; i++)
		//	{
		//		InitGift(m_wheelGifts[i], i, angle);
		//		angle += 45.0f;

		//		if(m_container != null)
		//			m_wheelGifts[i].giftText.fontSize = ysizeDelta < 0 ? 150 : 200;
		//	}
		}

        void initAgain()
        {
            m_wheelGifts = m_wheelGiftParent.GetComponentsInChildren<GiftWheelCard>();
            float segmentCount = m_wheelGifts.Length;
            float angleStep = 360f / segmentCount;
            float angle = angleStep / 2f;

            Vector3 centerPos = m_giftWheel.position;

            for (int i = 0; i < m_wheelGifts.Length; i++)
            {
                GiftWheelCard card = m_wheelGifts[i];

                // Reset scale/position instantly
                card.transform.localScale = Vector3.zero;
                card.transform.rotation = Quaternion.identity;

                // Reinitialize gift value (e.g. gold or ball type)
                InitGift(card, i, angle);

                // Now animate in with small delay
                card.transform.localPosition = Vector3.zero;

                // Animate scale + rotation
                card.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).SetDelay(i * 0.03f);
                card.transform.DOLocalRotate(new Vector3(0f, 0f, angle), 0.4f, RotateMode.Fast)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(i * 0.03f);

                angle += angleStep;
            }
        }



        private IEnumerator DelayedInit()
        {
            yield return new WaitUntil(() => FindObjectOfType<MainPanel>() != null);
            initAgain();
        }


        // Use this for initialization
        private void OnEnable()
        {
            for (int i = 0; i < m_leftLightsAnim.Length; i++)
            {
                m_leftLightsAnim[i].SetInteger("LightIndex", i);
                m_rightLightsAnim[i].SetInteger("LightIndex", i);
                m_leftLightsAnim[i].SetFloat("Speed", 0.5f);
                m_rightLightsAnim[i].SetFloat("Speed", 0.5f);
                m_leftLightsAnim[i].SetBool("Stop", false);
                m_rightLightsAnim[i].SetBool("Stop", false);
            }

            m_spinButton.isInteractable = true;
            m_hasSpin = true;
            m_closeButton.isInteractable = true;

            m_openSequence.Restart();
            m_sunBurst.DORotate(new Vector3(0f, 0f, 360f), 20f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            m_checkFreeGiftCoroutine = StartCoroutine(CheckFreeGift());

            //  Instead of direct init, use delayed version
            StartCoroutine(InitWheelDelayed());

            if (GPlayPassManager.IsActive)
            {
               // m_respinButton.gameObject.SetActive(false);
            }
        }
        private IEnumerator InitWheelDelayed()
        {
            yield return new WaitForSeconds(0.1f); // let MainPanel & configs be ready
            initAgain(); // will re-run InitGift with fresh values
        }


        private IEnumerator DelayedWheelInit()
        {
            // Wait a few frames just in case wheelGifts or configs aren't initialized yet
            yield return new WaitForSeconds(0.2f);

            // Wait until MainPanel is ready and configs are loaded
            yield return new WaitUntil(() =>
                ApplicationManager.config != null &&
                ApplicationManager.config.game.wheelGifts != null &&
                ApplicationManager.config.game.wheelGifts.Length == m_wheelGiftParent.transform.childCount &&
                FindObjectOfType<MainPanel>() != null
            );

            // Re-assign wheel cards
            m_wheelGifts = m_wheelGiftParent.GetComponentsInChildren<GiftWheelCard>();

            // Balanced 360° distribution
            float ysizeDelta = m_container != null ? m_container.sizeDelta.y : -1;
            float segmentCount = m_wheelGifts.Length;
            float angleStep = 360f / segmentCount;
            float angle = angleStep / 2f;

            for (int i = 0; i < m_wheelGifts.Length; i++)
            {
                InitGift(m_wheelGifts[i], i, angle);
                angle += angleStep;

                if (m_container != null)
                    m_wheelGifts[i].giftText.fontSize = ysizeDelta < 0 ? 150 : 200;
            }
        }


        protected override void OnDisable ()
		{
			base.OnDisable();

		}

		protected override void OnDestroy ()
		{
			m_spinButton.onClick -= Spin;
			m_collectButton.onClick -= Collect;
			//m_respinButton.onClick -= Respin;
			base.OnDestroy();
		}


		private bool m_freeGiftActivated = false;

		private IEnumerator CheckFreeGift ()
		{
			m_timeRemainingText.gameObject.SetActive(true);
			m_spinButton.gameObject.SetActive(ApplicationManager.datas.vip);
			m_spinButton.isInteractable = false;
			if (GPlayPassManager.IsActive)
			{
				//m_respinButton.gameObject.SetActive(false);
            }
            else
			{
				//m_respinButton.gameObject.SetActive(!ApplicationManager.datas.vip);
			}

			//m_respinButton.isInteractable = !ApplicationManager.datas.vip;
			m_wheelHide.SetActive(true);
			WaitForSeconds oneSecoundWait = new WaitForSeconds(1f);
			TimeSpan time;
			int timeRemaining;
			while (!m_freeGiftActivated)
			{
				timeRemaining = (ApplicationManager.datas.vip ? 1 : 8) * 1800 - (int)(ApplicationManager.utcTime - ApplicationManager.datas.lastFreeGiftTime);
				if (timeRemaining <= 0)
				{
					m_freeGiftActivated = true;
				}
				else
				{
					time = TimeSpan.FromSeconds(timeRemaining);
					m_timeRemainingText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
					yield return oneSecoundWait;
				}
			}
			m_timeRemainingText.gameObject.SetActive(false);
			m_spinButton.gameObject.SetActive(true);
			m_spinButton.isInteractable = true;
			//m_respinButton.isInteractable = false;
			//m_respinButton.gameObject.SetActive(false);
			m_wheelHide.SetActive(false);
		}

		private void Spin ()
		{
			float randAngle = 360f * 5f + (URandom.Range(0, 8) + (m_firstSpin ? 0.5f : 0f)) * 360f / 8f;
			m_firstSpin = false;
			m_giftWheel.DORotate(new Vector3(0f, 0f, randAngle), 3f, RotateMode.LocalAxisAdd).SetEase(m_spinCurve).onComplete += StopSpin;
			m_closeButtonContainer.DOAnchorPosX(m_closeButtonStartPos + 500f, 0.5f).SetEase(Ease.OutQuad);
			m_spinButton.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack);
			//m_respinButton.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack);
			m_spinButton.isInteractable = false;
			//m_respinButton.isInteractable = false;
			m_closeButton.isInteractable = false;

			m_wheelHide.SetActive(false);
			m_timeRemainingText.gameObject.SetActive(false);

			for (int i = 0; i < m_leftLightsAnim.Length; i++)
			{
				m_leftLightsAnim[i].SetFloat("Speed", 2f);
				m_rightLightsAnim[i].SetFloat("Speed", 2f);
			}
			
			m_sunBurst.DOKill();
			m_sunBurst.DORotate(new Vector3(0f, 0f, 360f), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
		}

		private void StopSpin ()
		{
			m_sunBurst.DOKill();
			m_sunBurst.DORotate(new Vector3(0f, 0f, 360f), 20f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);

			for (int i = 0; i < m_leftLightsAnim.Length; i++)
			{
				m_leftLightsAnim[i].SetFloat("Speed", 0.5f);
				m_rightLightsAnim[i].SetFloat("Speed", 0.5f);
			}

			for (int i = 0; i < m_wheelGifts.Length; i++)
			{
				if(m_wheelGifts[i].transform.eulerAngles.z >= 337.5f || m_wheelGifts[i].transform.eulerAngles.z <= 22.5f)
				{
					InitGift(m_endWheelGift, i, 0f);
					m_giftIndex = i;
				}
			}

			GetGift();
			m_closeButton.isInteractable = true;
			StartCoroutine(StopSpinSequence());
		}

		private IEnumerator StopSpinSequence ()
		{
			yield return new WaitForSeconds(0.5f);

			m_endSpinWheelSequence.Restart();

			yield return new WaitForSeconds(m_endSpinWheelSequence.Duration());
			m_hasSpin = true;
			m_endSpinGiftSequence.Restart();

			yield return new WaitForSeconds(m_endSpinGiftSequence.Duration());
			m_collectButton.isInteractable = true;
			m_giftParticles.Play();
			UISounds.PlayCongratulationSound();


        }

		
		private int MinValue() 
		{
            return (int)(mp.AverageUpdateAmount() * 0.75f);

        }
		

        private void GetGift ()
		{
			switch (ApplicationManager.config.game.wheelGifts[m_giftIndex].type)
			{
                //case GameConfig.GameSettings.WheelGiftType.Diamond:
                //	ApplicationManager.datas.diamonds += ApplicationManager.config.game.wheelGifts[m_giftIndex].amount;
                //	UIManager.sceneManager.UpdateCurrencies();
                //	break;
                case GameConfig.GameSettings.WheelGiftType.Gold:
                    int goldAmount = URandom.Range(MinValue(), mp.AverageUpdateAmount() *5); // +1 to include avg as max
                    ApplicationManager.datas.coins += (ulong)goldAmount;
                    UIManager.sceneManager.UpdateCurrencies();
                    break;
                case GameConfig.GameSettings.WheelGiftType.GoldenBall:
					ApplicationManager.datas.AddBalls(1, ApplicationManager.config.game.wheelGifts[m_giftIndex].amount);
					ApplicationManager.datas.selectedBallId = 1;
					UIManager.sceneManager.UpdateBall();
					break;
				case GameConfig.GameSettings.WheelGiftType.RedBall:
					ApplicationManager.datas.AddBalls(2, ApplicationManager.config.game.wheelGifts[m_giftIndex].amount);
					ApplicationManager.datas.selectedBallId = 2;
					UIManager.sceneManager.UpdateBall();
					break;
				case GameConfig.GameSettings.WheelGiftType.BlueBall:
					ApplicationManager.datas.AddBalls(4, ApplicationManager.config.game.wheelGifts[m_giftIndex].amount);
					ApplicationManager.datas.selectedBallId = 4;
					UIManager.sceneManager.UpdateBall();
					break;
				case GameConfig.GameSettings.WheelGiftType.VioletBall:
					ApplicationManager.datas.AddBalls(3, ApplicationManager.config.game.wheelGifts[m_giftIndex].amount);
					ApplicationManager.datas.selectedBallId = 3;
					UIManager.sceneManager.UpdateBall();
					break;
				case GameConfig.GameSettings.WheelGiftType.OrangeBall:
					ApplicationManager.datas.AddBalls(5, ApplicationManager.config.game.wheelGifts[m_giftIndex].amount);
					ApplicationManager.datas.selectedBallId = 5;
					UIManager.sceneManager.UpdateBall();
					break;
				default:
					break;
			}
			ApplicationManager.datas.lastFreeGiftTime = ApplicationManager.utcTime;
			ApplicationManager.SaveGameDatas();
		}



		private void Collect ()
		{
			m_freeGiftActivated = false;
			m_collectButton.isInteractable = false;

			Sequence m_closeGiftSequence = DOTween.Sequence();
			if (ApplicationManager.datas.vip)
			{
				m_closeGiftSequence.Append(m_collectButton.transform.DOScale(1f, 0.5f).SetEase(Ease.InBack));
				//m_closeGiftSequence.Append(m_respinButton.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack));
			}
			else
			{
				m_closeGiftSequence.Append(m_collectButton.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack));
				//m_closeGiftSequence.Append(m_respinButton.transform.DOScale(1f, 0.5f).SetEase(Ease.InBack));
			}

			m_closeGiftSequence.Insert(0.05f, m_endWheelGift.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack));
			m_closeGiftSequence.Insert(0.05f, m_sunBurst.DOScale(0f, 0.5f).SetEase(Ease.InBack));
			m_closeGiftSequence.OnComplete(()=>
			{
				if (!ApplicationManager.datas.vip)
				{
				//	m_respinButton.isInteractable = true;
				}
				m_closeButton.isInteractable = true;
				m_closeButtonContainer.DOAnchorPosX(m_closeButtonStartPos, 0.5f).SetEase(Ease.InQuad);
				m_checkFreeGiftCoroutine = StartCoroutine(CheckFreeGift());
			});
		}

		private void Respin ()
		{
#if TAPNATION
			if (ApplicationManager.canWatchRewardedVideo)
			{
				m_collectButton.isInteractable = false;
				//m_respinButton.isInteractable = false;
				StopCoroutine(m_checkFreeGiftCoroutine);
				m_checkFreeGiftCoroutine = null;
				WatchRewarded();
			}
#endif
		}

		private void WatchRewarded ()
		{
			EGameEvent.Invoke(EEvents.RewardRespinWheel, new EData(this));
			//if (UIManager.sceneManager.ShowRewardedVideo(OnRewardedEnd))
			//{
			//	m_isRewarded = false;
			//}
		}

		private void OnRewardedEnd( /*AdsManagement.RewardedVideoEvent adEvent*/ )
		{
			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Opened:
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//		if (!m_isRewarded)
			//		{
			//			m_respinButton.isInteractable = true;
			//			//m_endSpinGiftSequence.Restart();
			//			this.UIManager.CloseProcessingPopup();
			//			m_checkFreeGiftCoroutine = StartCoroutine(CheckFreeGift());
			//		}
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Rewarded:
			//		m_isRewarded = true;
			//		Spin();
			//		//m_openSequence.Restart();
			//		this.UIManager.CloseProcessingPopup();
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		m_respinButton.isInteractable = true;
			//		//m_endSpinGiftSequence.Restart();
			//		m_checkFreeGiftCoroutine = StartCoroutine(CheckFreeGift());
			//		this.UIManager.CloseProcessingPopup();
			//		break;
			//	default:
			//		break;
			//}
		}

		public void OnRewardedGiftWheel(bool succeed)
		{
			if (succeed)
			{
                m_isRewarded = true;
				Spin();
				//m_openSequence.Restart();
				this.UIManager.CloseProcessingPopup();
			}
            else
			{
			//	m_respinButton.isInteractable = true;
				//m_endSpinGiftSequence.Restart();
				this.UIManager.CloseProcessingPopup();
				m_checkFreeGiftCoroutine = StartCoroutine(CheckFreeGift());
			}
        }

		public override bool OnBackAction ()
		{
			if (m_closeButton.isInteractable)
			{
				OnClose();
			}
			return m_closeButton.isInteractable;
		}

		protected override void OnBackButtonPressed ()
		{
			OnClose();
			base.OnBackButtonPressed();
		}

		private void OnClose ()
		{
			if (m_hasSpin)
			{
				m_freeGiftActivated = false;
			}
			m_spinButton.isInteractable = false;
			m_endSpinWheelSequence.Restart();
			m_collectButton.transform.localScale = Vector3.zero;
			//m_respinButton.transform.localScale = Vector3.zero;
		}

		private void InitGift ( GiftWheelCard giftCard, int index, float angle )
		{
			switch (ApplicationManager.config.game.wheelGifts[index].type)
			{
                //case GameConfig.GameSettings.WheelGiftType.Diamond:
                //	giftCard.Init(m_diamondSprite, ApplicationManager.config.game.wheelGifts[index].amount, angle, Color.white);
                //	break;
                case GameConfig.GameSettings.WheelGiftType.Gold:
                        int randomGold = URandom.Range(MinValue(), mp.AverageUpdateAmount() *5); // +1 to include avg
                        giftCard.Init(m_goldSprite, randomGold, angle, Color.white);
                        Debug.Log($"[WheelGift] Recalculated GOLD: {randomGold} (avg: {mp.AverageUpdateAmount()})");
					break;
                case GameConfig.GameSettings.WheelGiftType.GoldenBall:
					giftCard.Init(ApplicationManager.assets.balls[1].ballSprite, ApplicationManager.config.game.wheelGifts[index].amount, angle, ApplicationManager.assets.balls[1].ballColor);
					break;
				case GameConfig.GameSettings.WheelGiftType.RedBall:
					giftCard.Init(ApplicationManager.assets.balls[2].ballSprite, ApplicationManager.config.game.wheelGifts[index].amount, angle, ApplicationManager.assets.balls[2].ballColor);
					break;
				case GameConfig.GameSettings.WheelGiftType.BlueBall:
					giftCard.Init(ApplicationManager.assets.balls[4].ballSprite, ApplicationManager.config.game.wheelGifts[index].amount, angle, ApplicationManager.assets.balls[4].ballColor);
					break;
				case GameConfig.GameSettings.WheelGiftType.VioletBall:
					giftCard.Init(ApplicationManager.assets.balls[3].ballSprite, ApplicationManager.config.game.wheelGifts[index].amount, angle, ApplicationManager.assets.balls[3].ballColor);
					break;
				case GameConfig.GameSettings.WheelGiftType.OrangeBall:
					giftCard.Init(ApplicationManager.assets.balls[5].ballSprite, ApplicationManager.config.game.wheelGifts[index].amount, angle, ApplicationManager.assets.balls[5].ballColor);
					break;
				default:
					break;
			}
		}
	}

}