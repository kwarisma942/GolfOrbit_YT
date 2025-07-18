using DG.Tweening;
using Pinpin.Helpers;
using Pinpin.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class MainPanel : AUIPanel
	{
		[FormerlySerializedAs("m_shopButtons")]
		[SerializeField] private UIShopButton[] m_upgradeButtons;
		[SerializeField] private PushButton m_goldenBallAdButton;
		[SerializeField] private PushButton m_goldenBallShortcutButton;
		[SerializeField] private PushButton m_goldenBallVIPButton;
		[SerializeField] private UIShopButton m_goldenBallDiamondButton;
		[SerializeField] private PushButton m_shopButton;

		[SerializeField] private PushButton m_clubHouseBoutton;
		[SerializeField] private GameObject m_blockRaycast;
		[SerializeField] private PushButton m_settingsButton;
		[SerializeField] private PushButton m_hideUIButton;
		[SerializeField] private GameObject m_hideUIContainer;

		[Header("Shot Gauge")]
		[SerializeField] private RectTransform m_shotArrow;
		[SerializeField] private float m_shotArrowSpeed = 1.0f;

		[Header("Currency")]
		[SerializeField] private Text m_coinText;
		[SerializeField] private Text m_diamondText;

		[Header("Score")]
		[SerializeField] private Text m_bestDistanceText;
		[SerializeField] private RectTransform m_bestDistanceFeedbackContainer;
		[SerializeField] private RectTransform m_bestDistanceFeedbackLayout;
		[SerializeField] private Text m_currentDistanceMiddleText;
		[SerializeField] private RectTransform m_currentDistanceFeedbackContainer;
		[SerializeField] private RectTransform m_currentDistanceBallLine;
		[SerializeField] private RectTransform m_currentDistanceLine;
		[SerializeField] private Text m_distanceTargetText;
		[SerializeField] private GameObject m_missionTextContainer;
		[SerializeField] private Text m_nextCharacterUnlockText;
		[SerializeField] private AnimationCurve m_bestScoreYpositionCurve;

		[Header("To Deactivate In Game")]
		[SerializeField] private GameObject m_gauge;
		[SerializeField] private GameObject m_leftLayout;
		[SerializeField] private GameObject m_rightLayout;
		[SerializeField] private GameObject m_currentDistance;
		[SerializeField] private PushButton m_freeGiftButton;
		[SerializeField] private RectTransform m_freeGiftImageContainer;

		[Header("Upgrades / Bonus")]
		[SerializeField] private RectTransform m_upgradeLayout;
		private float m_upgradeContainerScreenXDelta;
		//private float m_upgradeContainerBaseXPosition;
		[SerializeField] private RectTransform m_bonusLayout;
		[SerializeField] private PushButton m_closeBonusButton;
		[SerializeField] private PushButton m_openBonusButton;
		private bool m_isBonusOpen;

		[Header("Free Gift Diamonds")]
		[SerializeField] private List<Diamond> m_diamonds;

		[SerializeField] private GameObject m_startAnimation;
		[SerializeField] private PushButton m_debugMenuButton;
		[SerializeField] private OfflineEarningsPopup m_offlineEarningsPopup;
        [SerializeField] private PushButton m_redBallAdButton;
        [SerializeField] private PushButton m_blueBallAdButton;
        [SerializeField] private PushButton m_purpleBallAdButton;
        [SerializeField] private PushButton m_orangeBallAdButton;
		[SerializeField] private PushButton m_goldenBallAdButton_new;


		private GameObject m_BallButtonParent;

        private bool m_freeGiftActivated = false;
		private bool m_isActivated = true;

		private int RVCount = 0;
		private ulong coins
		{
			set { m_coinText.text = MathHelper.ConvertToEgineeringNotation(value); }//value.ToString("n0", ApplicationManager.currentCulture); }
		}

		private int diamonds
		{
			set { m_diamondText.text = MathHelper.ConvertToEgineeringNotation((ulong)value); }//value.ToString("n0", ApplicationManager.currentCulture); }
		}

		private int m_bestDistance;
		public int bestDistance
		{
			get { return m_bestDistance; }
			set
			{
				m_bestDistanceText.text = value.ToString("n0", ApplicationManager.currentCulture);
				m_bestDistance = value;
			}
		}

		private int m_targetDistance
		{
			set
			{
				if (ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) >= ApplicationManager.config.game.characterUnlockDistance.Length)
				{
					if (!UIManager.sceneManager.IsLastPlanet() && !ApplicationManager.datas.IsWorldUnlocked(ApplicationManager.datas.selectedWorldId + 1))
					{
						m_nextCharacterUnlockText.text = I2.Loc.ScriptLocalization.mission_orbit;
						m_distanceTargetText.text = "";
					}
					else
					{
						m_nextCharacterUnlockText.text = I2.Loc.ScriptLocalization.mission_planet_ended;
						m_distanceTargetText.text = "";
					}
				}
				else
				{
					if (UIManager.sceneManager.IsLastPlanet() && ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) == UIManager.sceneManager.GetMaxCharacterUnlock())
					{
						m_nextCharacterUnlockText.text = I2.Loc.ScriptLocalization.mission_planet_ended;
					}
					else
					{
						m_distanceTargetText.text = value.ToString("n0", ApplicationManager.currentCulture);
						if (ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) == UIManager.sceneManager.GetMaxCharacterUnlock())
						{
							m_nextCharacterUnlockText.text = String.Format(I2.Loc.ScriptLocalization.mission_new_planet, value.ToString("n0", ApplicationManager.currentCulture));
						}
						else
						{
							m_nextCharacterUnlockText.text = String.Format(I2.Loc.ScriptLocalization.mission_new_golfer, value.ToString("n0", ApplicationManager.currentCulture));
						}
					}
				}
			}
		}

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
			for (int i = 0; i < m_upgradeButtons.Length; i++)
			{
				m_upgradeButtons[i].onPurchase += CoinPurchase;
			}

			m_clubHouseBoutton.onClick += OpenClubHouse;
			m_freeGiftButton.onClick += OnClickFreeGiftButton;
			m_settingsButton.onClick += UIManager.sceneManager.OpenSettingsPanel;
			StartCoroutine(CheckFreeGift());

			m_goldenBallAdButton.onClick += WatchBonusGoldenBallAd;
			m_goldenBallShortcutButton.onClick += WatchGoldenBallAd;
			m_goldenBallDiamondButton.onPurchase += GetGoldenBall;
			m_goldenBallDiamondButton.onPurchase += DiamondPurchase;
			m_goldenBallVIPButton.onClick += OnGoldenBallVIPButtonPressed;
            m_redBallAdButton.onClick += WatchRedBallAd;
            m_blueBallAdButton.onClick += WatchBlueBallAd;
            m_purpleBallAdButton.onClick += WatchPurpleBallAd;
            m_orangeBallAdButton.onClick += WatchOrangeBallAd;
			m_goldenBallAdButton_new.onClick += WatchBonusGoldenBallAd;

			m_BallButtonParent = m_redBallAdButton.gameObject.transform.parent.gameObject;


            ApplicationManager.onUTCTimeUpdated += OnUTCTimeUpdated;

			m_debugMenuButton.onClick += OpenDebugMenuPanel;
			
			SetBestDistanceFeedbackanchor();

			m_targetDistance = (int)ApplicationManager.config.game.characterUnlockDistance[Mathf.Min(ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId), ApplicationManager.config.game.characterUnlockDistance.Length - 1)];

			//m_upgradeContainerBaseXPosition = m_leftLayout.transform.localPosition.x;
			m_upgradeContainerScreenXDelta = -this.GetComponent<RectTransform>().rect.width;

			m_closeBonusButton.onClick += CloseBonusLayout;
			m_openBonusButton.onClick += OpenBonusLayout;
			m_hideUIButton.onClick += HideUI;

			m_bonusLayout.localPosition = new Vector3(m_bonusLayout.localPosition.x + m_upgradeContainerScreenXDelta / 2 - 300f, m_bonusLayout.localPosition.y, m_bonusLayout.localPosition.z);

			m_shopButton.onClick += UIManager.sceneManager.OpenShop;
			if (!ApplicationManager.config.application.enablePurchasing)
			{
				m_shopButton.gameObject.SetActive(false);
			}

			m_missionTextContainer.SetActive(true);
			m_offlineEarningsPopup.Setup(UIManager);
			m_offlineEarningsPopup.onClose += OnOfflineEarningPopupClosed;

			CheckOfflineEarning();

			ApplicationManager.onVipActivated += OnVipActivated;
			m_openBonusButton.gameObject.SetActive(!ApplicationManager.datas.vip);
			//m_goldenBallShortcutButton.gameObject.SetActive(true);

			//enable golden ball ad buttons
			m_goldenBallShortcutButton.isInteractable = true;
			m_goldenBallAdButton.isInteractable = true;

			if (ApplicationManager.IsFirstTimeLaunch())
			{
				m_redBallAdButton.transform.parent.gameObject.SetActive(false);
				m_rightLayout.gameObject.SetActive(false);
			}

		}

		private void OnUTCTimeUpdated()
		{
			if (m_isActivated)
				CheckOfflineEarning();
		}

		private void OnEnable ()
		{
			m_shotArrow.GetComponent<Animator>().SetFloat("ShotArrowSpeed", m_shotArrowSpeed);
			coins = ApplicationManager.datas.coins;
			//Debug.LogError(" coins "+ ApplicationManager.datas.coins);
			diamonds = ApplicationManager.datas.diamonds;
			bestDistance = ApplicationManager.datas.GetWorldBestScore(ApplicationManager.datas.selectedWorldId);

            //if (ApplicationManager.datas.vip)
            //{
            //	if (CheckVIPDailyGift() == false)
            //	{
            //		CheckOfflineEarning();
            //	}
            //}
            //else
            //{
            //	CheckOfflineEarning();
            //}
            CheckOfflineEarning();
        }

		public override bool OnBackAction ()
		{
			if (m_offlineEarningsPopup.gameObject.activeInHierarchy)
			{
				m_offlineEarningsPopup.PlayCloseAnimation();
				return false;
			}
			else
				return base.OnBackAction();
		}

		public void StartIntroAnimation ()
		{
			if (ApplicationManager.config.game.enableStartAnimation && m_startAnimation != null)
			{
				Instantiate(m_startAnimation, Camera.main.transform);
			}
		}

		private void OpenDebugMenuPanel ()
		{
			UIManager.OpenPanel<DebugPanel>();
		}

		private bool CheckVIPDailyGift ()
		{
			if (ApplicationManager.newDay)
			{
				ApplicationManager.newDay = false;
				ApplicationManager.datas.lastVIPDailyGiftTime = ApplicationManager.utcTime;
				ApplicationManager.datas.diamonds += 100;
				ApplicationManager.datas.coins += 10000;
				ApplicationManager.datas.SaveDatas();
				if (!GPlayPassManager.IsActive)
					UIManager.OpenPopup<DailyVipPopup>().onClosed += CheckOfflineEarning;
				else
					CheckOfflineEarning();
				return true;
			}
			return false;
		}

		private void CheckOfflineEarning ()
		{

			if (ApplicationManager.utcInitialized == false)
				return;
			if (ApplicationManager.config.application.enableOfflineEarning == false)
				return;
			if (!ApplicationManager.needOfflineEarningCheck)
				return;
			if (ApplicationManager.canTakeOfflineEarning == false)
				return;
			Debug.Log("Main Panel - CheckOfflineEarning");

			ApplicationManager.needOfflineEarningCheck = false;
#if GPP_TEST_AD || (UNITY_EDITOR && !UNITY_CLOUD_BUILD)
			if (ApplicationManager.timeSinceLastAppQuit.TotalMinutes >= 1f)
#else
			if (ApplicationManager.timeSinceLastAppQuit.TotalMinutes >= 5f)
#endif
			{
				GameConfig.GameSettings gamesettings = ApplicationManager.config.game;
				ulong averageUpgradePrice = (gamesettings.GetUpgradePrice(ApplicationManager.datas.GetUpgradeLevel(UIShopButton.UpgradeType.Strength)) +
										   gamesettings.GetUpgradePrice(ApplicationManager.datas.GetUpgradeLevel(UIShopButton.UpgradeType.Speed)) +
										   gamesettings.GetUpgradePrice(ApplicationManager.datas.GetUpgradeLevel(UIShopButton.UpgradeType.Bounce))) / 3;
				ulong offlineEarning = (ulong)(Mathf.Min((float)ApplicationManager.timeSinceLastAppQuit.TotalHours, 48) * averageUpgradePrice * 12f / 100f);
				if (offlineEarning > 0)
				{
					m_offlineEarningsPopup.gameObject.SetActive(true);
					m_offlineEarningsPopup.SetEarning(offlineEarning);
				}
			}
		}

		private IEnumerator CheckFreeGift ()
		{
			WaitForSeconds oneSecoundWait = new WaitForSeconds(1f);
			TimeSpan time;
			int timeRemaining;
			while (true)
			{
				timeRemaining = (ApplicationManager.datas.vip ? 1 : 8) * 1800 - (int)(ApplicationManager.utcTime - ApplicationManager.datas.lastFreeGiftTime);

				if (timeRemaining <= 0 && !m_freeGiftActivated)
				{
					m_freeGiftActivated = true;
					m_freeGiftButton.text = I2.Loc.ScriptLocalization.ready;
					m_freeGiftImageContainer.DOPunchRotation(Vector3.forward * 20.0f, 1.0f, 8).SetLoops(-1);
				}
				else if (timeRemaining > 0)
				{
					if (m_freeGiftActivated)
					{
						m_freeGiftActivated = false;
						m_freeGiftImageContainer.DOKill();
						m_freeGiftImageContainer.localRotation = Quaternion.identity;
					}
					time = TimeSpan.FromSeconds(timeRemaining);
					m_freeGiftButton.text = string.Format("{0:D2}:{1:D2}", time.Hours, time.Minutes);
					yield return oneSecoundWait;
				}
				else
				{
					yield return oneSecoundWait;
				}
			}
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy();

			for (int i = 0; i < m_upgradeButtons.Length; i++)
			{
				m_upgradeButtons[i].onPurchase -= CoinPurchase;
			}

			m_clubHouseBoutton.onClick -= OpenClubHouse;
			m_freeGiftButton.onClick -= OnClickFreeGiftButton;
			m_settingsButton.onClick -= UIManager.sceneManager.OpenSettingsPanel;

			m_goldenBallAdButton.onClick -= WatchBonusGoldenBallAd;
			m_goldenBallShortcutButton.onClick -= WatchGoldenBallAd;
			m_goldenBallDiamondButton.onPurchase -= GetGoldenBall;
			m_goldenBallDiamondButton.onPurchase -= DiamondPurchase;
			m_goldenBallVIPButton.onClick -= OnGoldenBallVIPButtonPressed;

			m_closeBonusButton.onClick -= CloseBonusLayout;
			m_openBonusButton.onClick -= OpenBonusLayout;
			m_hideUIButton.onClick -= HideUI;

			m_shopButton.onClick -= UIManager.sceneManager.OpenShop;
			m_debugMenuButton.onClick -= OpenDebugMenuPanel;
			ApplicationManager.onVipActivated -= OnVipActivated;


			ApplicationManager.onUTCTimeUpdated -= OnUTCTimeUpdated;
		}

		private void OnVipActivated ()
		{
			m_openBonusButton.gameObject.SetActive(false);
			//m_goldenBallShortcutButton.gameObject.SetActive(false);
			if (m_isBonusOpen)
			{
				CloseBonusLayout();
			}
		}

		private void OpenBonusLayout ()
		{
			m_bonusLayout.DOComplete();
			m_bonusLayout.DOKill();
			//m_bonusLayout.DOLocalMoveX(m_bonusLayout.localPosition.x - m_upgradeContainerScreenXDelta / 2 + 300f, 0.5f).SetEase(Ease.OutCubic);
			m_openBonusButton.interactable = false;

			m_isBonusOpen = true;
		}

		private void CloseBonusLayout ()
		{
			m_bonusLayout.DOComplete();
			m_bonusLayout.DOKill();
			//m_bonusLayout.DOLocalMoveX(m_bonusLayout.localPosition.x + m_upgradeContainerScreenXDelta / 2 - 300f, 0.5f).SetEase(Ease.OutCubic);
			m_openBonusButton.interactable = true;

			m_isBonusOpen = false;
		}

		private void OnClickFreeGiftButton ()
		{
			UIManager.OpenPanel<GiftWheelPanel>();
		}

		private void DiamondExplosion ( int amount )
		{
			int amountPerDiamonds = amount / 5;
			ApplicationManager.datas.diamonds += amountPerDiamonds;
			diamonds = ApplicationManager.datas.diamonds;
		}

		public float GetShotAngle ()
		{
			return m_shotArrow.transform.localEulerAngles.z;
		}

		public void StopArrow ()
		{
			m_shotArrow.GetComponent<Animator>().SetFloat("ShotArrowSpeed", 0.0f);
			//m_leftLayout.transform.DOLocalMoveX(m_upgradeContainerScreenXDelta, 1.0f).SetEase(Ease.InBack);
            CanvasGroupFading(m_leftLayout, true);
			CanvasGroupFading (m_rightLayout, true);

            if (m_isBonusOpen)
			{
				CloseBonusLayout();
			}
			m_missionTextContainer.SetActive(false);
			m_clubHouseBoutton.isInteractable = false;
		}

		public void OnStartGame ()
		{
			m_gauge.SetActive(false);
			m_leftLayout.SetActive(false);
			m_rightLayout.SetActive(false);
			m_currentDistance.SetActive(true);
			m_blockRaycast.SetActive(false);
			m_isActivated = false;
			m_lastDistanceTrigger = 100;
		}

		
		private int m_lastDistanceTrigger = 100;
		public void UpdateCurrentDistance ( Vector2 anchor, int distance )
		{
			if (!ReferenceEquals(m_currentDistanceFeedbackContainer, null))
			{
				m_currentDistanceFeedbackContainer.anchorMin = m_currentDistanceFeedbackContainer.anchorMax = anchor;
			}
			if (!ReferenceEquals(m_currentDistanceBallLine, null) && !ReferenceEquals(m_currentDistanceLine, null))
			{
				m_currentDistanceBallLine.localScale = m_currentDistanceLine.localScale = new Vector2(anchor.y, anchor.x);
			}
			if (!ReferenceEquals(m_currentDistanceMiddleText, null))
			{
				m_currentDistanceMiddleText.text = distance.ToString("n0", ApplicationManager.currentCulture.NumberFormat) + " " + I2.Loc.ScriptLocalization.distance_unit_abrevation;
				if (distance > m_lastDistanceTrigger)
				{
					m_lastDistanceTrigger += 100;
					m_currentDistanceMiddleText.rectTransform.DOComplete();
					m_currentDistanceMiddleText.rectTransform.DOPunchScale(Vector3.one * 0.25f, 0.2f);
				}
			}
		}

		public void CoinPurchase ()
		{
			coins = ApplicationManager.datas.coins;

			for (int i = 0; i < m_upgradeButtons.Length; i++)
			{
				m_upgradeButtons[i].SetState();
			}
		}

		public void DiamondPurchase ()
		{
			diamonds = ApplicationManager.datas.diamonds;
			m_goldenBallDiamondButton.SetState();
		}

		private void WatchGoldenBallAd ()
		{
			EGameEvent.Invoke(EEvents.RewardGoldenBall, new EData(this));
			//UIManager.sceneManager.ShowRewardedVideo(OnGoldenBallAdEnd);
		}

		public void OnRewardedGoldenBall(bool succeed)
		{
			if (succeed)
			{
                this.UIManager.CloseProcessingPopup();
				//LoadingScreen.Hide();
				OnGoldenBallPurchase(1);
			}
            else
			{
                this.UIManager.CloseProcessingPopup();
				//LoadingScreen.Hide();
			}
		}

		private void OnGoldenBallAdEnd ( /*AdsManagement.RewardedVideoEvent adEvent*/ )
		{
			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Opened:
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		this.UIManager.CloseProcessingPopup();
			//		//LoadingScreen.Hide();
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Rewarded:
			//		this.UIManager.CloseProcessingPopup();
			//		//LoadingScreen.Hide();
			//		OnGoldenBallPurchase(1);
			//		break;
			//	default:
			//		break;
			//}
		}

		private void OnOfflineEarningPopupClosed ()
		{
			m_offlineEarningsPopup.gameObject.SetActive(false);
			UIManager.sceneManager.UpdateCurrencies();

			//disable popup notification
			//if (ApplicationManager.datas.firstTimeOEPopup)
			//{
			//	ApplicationManager.datas.firstTimeOEPopup = false;
			//	UIManager.OpenPopup<OfflineEarningNotificationPopup>();
			//}
		}

		private void WatchBonusGoldenBallAd ()
		{
			EGameEvent.Invoke(EEvents.RewardBonusGoldenBall, new EData(this));
			//UIManager.sceneManager.ShowRewardedVideo(OnBonusGoldenBallAdEnd);
		}

		public void OnRewardedBonusGoldenBall(bool succeed)
		{
			if (succeed)
			{
				//LoadingScreen.Hide();
				this.UIManager.CloseProcessingPopup();
				OnGoldenBallPurchase(1);
			}
            else
			{
				//LoadingScreen.Hide();
				this.UIManager.CloseProcessingPopup();
			}
        }

        public void OnRewardedRedBall(bool succeed)
        {
			if (succeed) 
			{
                OnColoredBallPurchase(2); // Red = 2
            m_BallButtonParent.SetActive(false);
				m_goldenBallShortcutButton.gameObject.SetActive(false);


            }
        }

        public void OnRewardedBlueBall(bool succeed)
        {
			if (succeed) {
                OnColoredBallPurchase(3); // Blue = 3
            m_BallButtonParent.SetActive(false);
                m_goldenBallShortcutButton.gameObject.SetActive(false);


            }
        }

        public void OnRewardedPurpleBall(bool succeed)
        {
			if (succeed) { 
                OnColoredBallPurchase(4); // Purple = 4
                m_BallButtonParent.SetActive(false);
                m_goldenBallShortcutButton.gameObject.SetActive(false);


            }
        }

        public void OnRewardedOrangeBall(bool succeed)
        {
			if (succeed) 
			{
                OnColoredBallPurchase(5); // Orange = 5
                m_BallButtonParent.SetActive(false);
                m_goldenBallShortcutButton.gameObject.SetActive(false);

            }

        }
        private void OnColoredBallPurchase(int ballId)
        {
            ApplicationManager.datas.AddBalls(ballId, 1);
            ApplicationManager.datas.selectedBallId = ballId;
            UIManager.sceneManager.UpdateBall();
        }


        private void OnGoldenBallVIPButtonPressed ()
		{
			SubscriptionPanel panel;
			if (ApplicationManager.config.game.subPopupABTest >= 0)
				panel = UIManager.OpenPanel<NewSubscriptionPanel>();
			else
				panel = UIManager.OpenPanel<SubscriptionPanel>();
			panel.Setup(1, 1, 0);
		}

        private void WatchRedBallAd()
        {
            EGameEvent.Invoke(EEvents.RewardRedBall, new EData(this));
           // m_BallButtonParent.SetActive(false);

        }

        private void WatchBlueBallAd()
        {
            EGameEvent.Invoke(EEvents.RewardBlueBall, new EData(this));
            //m_BallButtonParent.SetActive(false);

        }

        private void WatchPurpleBallAd()
        {
            EGameEvent.Invoke(EEvents.RewardPurpleBall, new EData(this));
            //m_BallButtonParent.SetActive(false);

        }

        private void WatchOrangeBallAd()
        {
            EGameEvent.Invoke(EEvents.RewardOrangeBall, new EData(this));
           // m_BallButtonParent.SetActive(false);
        }

        private void OnBonusGoldenBallAdEnd ( /*AdsManagement.RewardedVideoEvent adEvent*/ )
		{
			//switch (adEvent)
			//{
			//	case AdsManagement.RewardedVideoEvent.Opened:
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Closed:
			//	case AdsManagement.RewardedVideoEvent.Failed:
			//		//LoadingScreen.Hide();
			//		this.UIManager.CloseProcessingPopup();
			//		break;
			//	case AdsManagement.RewardedVideoEvent.Rewarded:
			//		//LoadingScreen.Hide();
			//		this.UIManager.CloseProcessingPopup();
			//		OnGoldenBallPurchase(1);
			//		break;
			//	default:
			//		break;
			//}
		}

		private void OnGoldenBallPurchase ( int amount )
		{
			ApplicationManager.datas.AddBalls(1, amount);
			ApplicationManager.datas.selectedBallId = 1;
			UIManager.sceneManager.UpdateBall();
			m_BallButtonParent.gameObject.SetActive(false);
			m_goldenBallShortcutButton.gameObject.SetActive(false);
		}

		private void GetGoldenBall ()
		{
			OnGoldenBallPurchase(10);
		}

		private void OpenClubHouse ()
		{
			UIManager.OpenPanel<ClubHousePanel>();
		}

		public void HideUI ()
		{
			m_hideUIContainer.SetActive(!m_hideUIContainer.activeInHierarchy);

		}

		public void ShowUI ()
		{
			m_hideUIContainer.SetActive(true);
		}

		private void SetBestDistanceFeedbackanchor ()
		{
			Vector2 bestDistanceAnchor = UIManager.sceneManager.GetDistanceFromUnlock(bestDistance);
			m_bestDistanceFeedbackContainer.anchorMin = m_bestDistanceFeedbackContainer.anchorMax = bestDistanceAnchor;
			bestDistanceAnchor.y = m_bestScoreYpositionCurve.Evaluate(bestDistanceAnchor.x);
			bestDistanceAnchor.x = m_bestDistanceFeedbackLayout.anchorMin.x;
			m_bestDistanceFeedbackLayout.anchorMin = m_bestDistanceFeedbackLayout.anchorMax = bestDistanceAnchor;
		}

		public void ResetGame ()
		{
			m_gauge.SetActive(true);
			m_leftLayout.SetActive(true);
			m_rightLayout.SetActive(true);
			m_blockRaycast.SetActive(true);
			m_isActivated = true;
            m_BallButtonParent.SetActive(true);
			//m_goldenBallShortcutButton.gameObject.SetActive(true);
            UIShopButton.ResetRVCount();
            m_clubHouseBoutton.isInteractable = true;
			//m_leftLayout.transform.DOLocalMoveX(m_upgradeContainerBaseXPosition, 1.0f).SetEase(Ease.OutBack);
			CanvasGroupFading(m_leftLayout,false);
			CanvasGroupFading(m_rightLayout,false);
			m_shotArrow.GetComponent<Animator>().SetFloat("ShotArrowSpeed", m_shotArrowSpeed);
			coins = ApplicationManager.datas.coins;
			diamonds = ApplicationManager.datas.diamonds;
			bestDistance = ApplicationManager.datas.GetWorldBestScore(ApplicationManager.datas.selectedWorldId);
			m_targetDistance = (int)ApplicationManager.config.game.characterUnlockDistance[Mathf.Min(ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId), ApplicationManager.config.game.characterUnlockDistance.Length - 1)];

			SetBestDistanceFeedbackanchor();

			m_goldenBallDiamondButton.SetState();
			m_missionTextContainer.SetActive(true);
			
			CheckOfflineEarning();

			if (ApplicationManager.IsFirstTimeLaunch())
			{
				ApplicationManager.SetFirstTimeLaunch(false);
				m_redBallAdButton.transform.parent.gameObject.SetActive(true);
				m_rightLayout.SetActive(true);
            }
		}

		void CanvasGroupFading(GameObject Panel, bool isFadeIn)
		{
			CanvasGroup leftCanvasGroup = Panel.GetComponent<CanvasGroup>();
			if (leftCanvasGroup == null)
				leftCanvasGroup = Panel.AddComponent<CanvasGroup>();

			if (isFadeIn)
				// First fade out to 0 (make it invisible)
				leftCanvasGroup.DOFade(0f, 1.0f).SetEase(Ease.InBack);
			else
				// After fading out, fade in the layout (moving in effect)
				leftCanvasGroup.DOFade(1f, 1.0f).SetEase(Ease.OutBack);
		}


		public void DisableRvButtons() 
		{
			for (int i = 0; i < m_upgradeButtons.Length; i++)
			{
				m_upgradeButtons[i].RvButon.SetActive(false);
			}
		}
        private ulong totalCost = 0;

        public int AverageUpdateAmount()
        {
            if (m_upgradeButtons == null || m_upgradeButtons.Length == 0)
                return 0;

            totalCost = 0;

            for (int i = 0; i < m_upgradeButtons.Length; i++)
            {
                totalCost += m_upgradeButtons[i].cost;
            }

            return (int)(totalCost / (ulong)m_upgradeButtons.Length);
        }

    }

}