using System;
using System.Collections;
using Pinpin.Scene.MainScene.UI;
using UnityEngine;
using PaperPlaneTools;
using Pinpin.UI;
using I2LocManager = I2.Loc.LocalizationManager;
using YTGameSDK;

namespace Pinpin.Scene.MainScene
{

	public sealed class MainSceneManager : ASceneManager
	{
		[SerializeField] private YTGameWrapper gameWrapper;
		[SerializeField] private GameManager m_gameManager;
		private float m_bestDistanceContainerSize;
		private MainPanel m_mainPanel;
		private GameManager.HoleScores m_holeScore;
		private WorldElement.WorldElementType m_landingElementType;
		public bool characterUnlocked = false;
		public bool newWorldUnlocked = false;
		private bool m_worldChange = false;
		private int m_sessionShotCount = 0;
		private int m_sessionInterstitialCount = 0;
		public int lastUnlockedCharacter = 0;
        public bool stoppedByGiveUp = false;
        public new MainSceneUIManager UI
		{
			get { return (base.UI as MainSceneUIManager); }
		}

		public bool rewardedShown = false;

		#region MonoBehaviour

		protected override void Awake ()
		{
			base.Awake();
			if (!ApplicationManager.isInitialized)
				return;
		}

		protected override void Start ()
		{
			base.Start();
            gameWrapper = GameObject.Find("YTGameWrapper").GetComponent<YTGameWrapper>();
            ApplicationManager.datas.isSoundActive = gameWrapper.IsYTGameAudioEnabled();

            m_gameManager.onGameOver += OnGameOver;
			m_gameManager.onStartGame += StartGame;
			m_gameManager.onFirstGreenLanding += FirstGreenLanding;
			m_gameManager.onOtherGreenLanding += OtherGreenLanding;
			m_gameManager.onHoleLanding += OnHoleLanding;
			m_gameManager.onWorldLanding += WorldLanding;
			m_gameManager.onOrbit += OpenOrbitPanel;
			m_gameManager.onDistanceChange += UpdateDistance;
			m_gameManager.onOrbitShot += HideScore;

			m_mainPanel = this.UI.OpenPanelAllTime<MainPanel>();
            if (PlayerPrefs.GetInt("itsFirstTime") > 0)
            {
                m_mainPanel.UpdateCurrentDistance(GetDistanceFromUnlock(0), 0);
            }

			UI.OpenPanel<FakeMainPanel>();
			UI.SetCurrentPanelAsRoot();
            gameWrapper.SetOnAudioEnabledChangeCallback(OnYTAudioChangedCallback);

            //if (UI.GetPopup<RatingPopup>().GetComponent<IAlertPlatformAdapter>() != null)
            //{
            //	RateBox.Instance.AlertAdapter = UI.GetPopup<RatingPopup>().GetComponent<IAlertPlatformAdapter>();
            //}

            //UI.GetPanel<ClubHousePanel>().Init();
            //UI.GetPopup<CharacterUnlockPopup>().Init();

            base.SetSceneReady();
            //if (!ApplicationManager.datas.vip)
            //{
            //	if (ApplicationManager.datas.sessionCount == 1 || ApplicationManager.playerLostVIP)
            //	{
            //		SubscriptionPanel panel;
            //		if (ApplicationManager.config.game.subPopupABTest >= 0)
            //			panel = UI.OpenPanel<NewSubscriptionPanel>();
            //		else
            //			panel = UI.OpenPanel<SubscriptionPanel>();
            //		panel.Setup(1, 0, 0);
            //		ApplicationManager.datas.lastWeeklySubPopupSeenTime = ApplicationManager.utcTime;
            //	}
            //	else if ((7 * 24 * 3600 - (int)(ApplicationManager.utcTime - ApplicationManager.datas.lastWeeklySubPopupSeenTime) <= 0))
            //	{
            //		SubscriptionPanel panel;
            //		if (ApplicationManager.config.game.subPopupABTest >= 0)
            //			panel = UI.OpenPanel<NewSubscriptionPanel>();
            //		else
            //			panel = UI.OpenPanel<SubscriptionPanel>();
            //		panel.Setup(1, 0, 1);
            //		ApplicationManager.datas.lastWeeklySubPopupSeenTime = ApplicationManager.utcTime;
            //	}
            //}
			gameWrapper.SendGameIsReady();

        }

        public void OnYTAudioChangedCallback(bool isAudioEnabled)
        {
            Debug.Log("In Unity: Audio change callback >> " + isAudioEnabled);
            ApplicationManager.datas.isSoundActive = isAudioEnabled;
			AudioListener.volume = isAudioEnabled ? 1 : 0;

        }

        public void InitUI() 
		{
			UI.GetPanel<ClubHousePanel>().Init();
			UI.GetPopup<CharacterUnlockPopup>().Init();
		}
        #endregion

        #region ASceneManager

        public override void OnQuit ()
		{
			m_gameManager.onGameOver -= OnGameOver;
			m_gameManager.onStartGame -= StartGame;
			m_gameManager.onFirstGreenLanding -= FirstGreenLanding;
			m_gameManager.onOtherGreenLanding -= OtherGreenLanding;
			m_gameManager.onHoleLanding -= OnHoleLanding;
			m_gameManager.onWorldLanding -= WorldLanding;
			m_gameManager.onOrbit -= OpenOrbitPanel;
			m_gameManager.onDistanceChange -= UpdateDistance;
			m_gameManager.onOrbitShot -= HideScore;

			ApplicationManager.Quit();
		}

		#endregion

		#region GameStates

		public void StartGame ()
		{
			m_mainPanel.OnStartGame();

			this.UI.OpenPanel<GamePanel>();

			RateBox.Instance.IncrementCustomCounter(1);

			//OdeeoManager.Instance.CloseAdLogo();
		}

		public void UpdateDistance ( int distance )
		{
			m_mainPanel.UpdateCurrentDistance(GetDistanceFromUnlock(distance), distance);
		}

		public void StopGame ()
		{
			m_holeScore = GameManager.HoleScores.None;
			m_gameManager.StopGame();
		}

		public void OnGameOver ()
		{
			AddDiamonds();

			if (!ApplicationManager.datas.IsBallUnlocked(ApplicationManager.datas.selectedBallId) && ApplicationManager.datas.GetBallCount(ApplicationManager.datas.selectedBallId) > 0)
			{
				if (ApplicationManager.datas.RemoveBall(ApplicationManager.datas.selectedBallId) == 0)
				{
					if (ApplicationManager.datas.vip)
						ApplicationManager.datas.selectedBallId = 1;
					else
					{
						ApplicationManager.datas.selectedBallId = 0;
						for (int i = 5; i > 0 && ApplicationManager.datas.selectedBallId == 0; i--)
						{
							if (ApplicationManager.datas.GetBallCount(i) > 0)
								ApplicationManager.datas.selectedBallId = i;
						}
					}
				}
				ApplicationManager.datas.SaveDatas();
			}

			EndGamePanel endGamePanel = this.UI.OpenPanel<EndGamePanel>();
			endGamePanel.SetScore(m_gameManager.GetScore(), m_gameManager.lastEarnedCoins, m_gameManager.lastEarnedDiamonds, m_holeScore, m_landingElementType);

			if (m_gameManager.gameState == GameManager.GameState.Green) 
				EGameEvent.Invoke(EEvents.OnLevelCompleted, null);
			else 
				EGameEvent.Invoke(EEvents.OnLevelFailed, null);

			Debug.Log("level is over now ");
        }

		public void ResetGame ()
		{
			bool isFirstGoldenBall = m_gameManager.ResetGame();
			StartCoroutine(WaitToCloseEndPanel(this.UI.GetPanel<EndGamePanel>().Close(), isFirstGoldenBall));

			m_mainPanel.ResetGame();
		}

		#endregion

		public void OpenShop ()
		{	
			UI.OpenPanel<ShopPanel>();
		}

		public void WorldLanding ( WorldElement.WorldElementType elementType )
		{
			m_landingElementType = elementType;
			m_holeScore = GameManager.HoleScores.None;
		}

		public void UpdateCurrencies ()
		{
			m_mainPanel.CoinPurchase();
			m_mainPanel.DiamondPurchase();
		}

		#region EndPanel

		public void OpenJackpotPanel ( ulong lastEarnedCoins )
		{
			JackpotPanel jackpotPopup = this.UI.OpenPanel<JackpotPanel>(true, true);
			jackpotPopup.SetLastEarnedCoin(lastEarnedCoins);
		}

		private IEnumerator WaitToCloseEndPanel ( float time, bool isFirstGoldenBall )
		{
			yield return new WaitForSeconds(time);

			this.UI.ClosePanel<EndGamePanel>();
			UI.OpenPanel<FakeMainPanel>();
			UI.SetCurrentPanelAsRoot();

			if (!ApplicationManager.datas.alreadyUsedAGoldenBall && ApplicationManager.datas.sessionCount > 1 && ++m_sessionShotCount > 3)
			{
				ApplicationManager.datas.AddBalls(1, 1);
				ApplicationManager.datas.selectedBallId = 1;
				UI.OpenPopup<GoldenBallPopup>();
				m_gameManager.UpdateBall();
				//OdeeoManager.Instance.ShowAdLogo();
			}
			else if (!m_worldChange && !newWorldUnlocked && !characterUnlocked)
			{
				if (!isFirstGoldenBall)
				{
					bool canShowChristmasPopup = false;
					if (ApplicationManager.config.game.enableChristmasEvent && Time.realtimeSinceStartup > 60
#if TAPNATION
						&& ApplicationManager.canWatchRewardedVideo
#endif
						)
					{
						DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
						DateTime lastChrismasTime = epoch.AddSeconds(ApplicationManager.datas.lastChristmasEventSeenTime);
						DateTime launchTime = epoch.AddSeconds(ApplicationManager.utcLaunchTime);
						launchTime = launchTime.AddMonths(-1);
						if (launchTime.Year > lastChrismasTime.Year)
						{
#if GPP_TEST_AD
							if (launchTime.Month >= 10 && launchTime.Month <= 12)
#else
							if (launchTime.Month >= 11 && launchTime.Month <= 12)
#endif
							{
								canShowChristmasPopup = true;
							}
						}
					}

					if (canShowChristmasPopup)
					{
						UI.OpenPanel<ChristmasPanel>();
					}
					else if (!rewardedShown)
					{
						bool canWatchInterstitial = ApplicationManager.canWatchInterstitial;

						if (canWatchInterstitial)
						{
							if (m_sessionInterstitialCount >= 4 && (8 * 3600 - (int)(ApplicationManager.utcTime - ApplicationManager.datas.lastNoAdSubPopupSeenTime) <= 0))
							{
								SubscriptionPanel panel;
								if (ApplicationManager.config.game.subPopupABTest >= 0)
									panel = UI.OpenPanel<NewSubscriptionPanel>();
								else
									panel = UI.OpenPanel<SubscriptionPanel>();
								panel.Setup(1, 1, 2);
								ApplicationManager.datas.lastNoAdSubPopupSeenTime = ApplicationManager.utcTime;
								ApplicationManager.datas.SaveDatas();
							}
							else
							{
								Debug.LogError("Show Inter Test");
                                if (ExampleRemoteConfigABtests._instance.UseAdPause)
                                {
									PanelADPause.Instance.Open();
                                }
                                else
                                {
									m_sessionInterstitialCount++;
									this.UI.OpenProcessingPopup(I2.Loc.ScriptLocalization.ad_loading);
						
									//FGMediation.ShowInterstitial("Inter",(status) =>
									//{
									//	this.UI.CloseProcessingPopup();
									//	//OdeeoManager.Instance.ShowAdLogo();
									//});
								}


							}
						}
						else
						{
							this.UI.CloseProcessingPopup();

							/*if (ApplicationManager.datas.rateboxShown < 3)
							{
								RateBox.Instance.Show();
                            }*/
							//if (!OdeeoManager.Instance.adPlaying)
							//OdeeoManager.Instance.ShowAdLogo();
							
						}
                    }
				}
				//else if (!ApplicationManager.datas.vip)
				//{
				//	SubscriptionPanel panel;
				//	if (ApplicationManager.config.game.subPopupABTest >= 0)
				//		panel = UI.OpenPanel<NewSubscriptionPanel>();
				//	else
				//		panel = UI.OpenPanel<SubscriptionPanel>();
				//	panel.Setup(1, 1, 0);
				//}
            }
			m_worldChange = false;
			newWorldUnlocked = false;
			characterUnlocked = false;
			rewardedShown = false;


			yield return null;
		}


	

		#endregion

		#region Green

		public void TutoGreenShot ( Vector2 axis, bool isThumbDown )
		{
			m_gameManager.PrepareGreenShot(axis, isThumbDown);
		}

		public Vector3 GetGolferDirection ()
		{
			Vector3 axis = Vector3.one;
			axis.x *= m_gameManager.GetGolferScaleSign();
			return axis;
		}

		public void FirstGreenLanding ()
		{
			GamePanel gamePanel = this.UI.GetPanel<GamePanel>();
			gamePanel.OnFirstGreenLanding();
			m_holeScore = GameManager.HoleScores.None;
		}

		public void OtherGreenLanding ()
		{
			GamePanel gamePanel = this.UI.GetPanel<GamePanel>();
			gamePanel.OnOtherGreenLanding();
		}

		public void OnHoleLanding ( GameManager.HoleScores score )
		{
			GamePanel gamePanel = this.UI.GetPanel<GamePanel>();
			gamePanel.OnHoleLanding(score);
			m_holeScore = score;
		}

		public void OneMoreChance ()
		{
			m_gameManager.OneMoreChance();
		}

		#endregion

		#region Currencies

		public void AddCoins ( ulong coinGain )
		{
			ApplicationManager.datas.coins += coinGain;
			ApplicationManager.SaveGameDatas();
		}

		private void AddDiamonds ()
		{
			ApplicationManager.datas.diamonds += m_gameManager.lastEarnedDiamonds;
			ApplicationManager.SaveGameDatas();
		}

		#endregion

		#region Ads

		public bool ShowRewardedVideo ( /*Action<RewardedVideoEvent> adEvent*/ )
		{
//#if CAPTURE
//			adEvent?.Invoke(AdsManagement.RewardedVideoEvent.Rewarded);
//			return true;
//#else
//			if (ApplicationManager.canWatchRewardedVideo)
//			{
//				Time.timeScale = 0f;
//				this.UI.OpenProcessingPopup(I2.Loc.ScriptLocalization.ad_loading);
//				 FGMediation.ShowRewarded((status) =>
//					{
//					Time.timeScale = 1f;
//					if (status)
//						adEvent?.Invoke(RewardedVideoEvent.Rewarded);
//					else
//						adEvent?.Invoke(RewardedVideoEvent.Failed);
//					this.UI.CloseProcessingPopup();
//				 });
//				return true;
//			}
//			return false;
//#endif
			return true;
		}

		#endregion

		#region Popups

		public void OpenSettingsPanel ()
		{
			this.UI.OpenPanel<SettingsPanel>();
		}

		#endregion

		#region EntitiesUpdates

		public void UpdateGolfer ()
		{
			m_gameManager.UpdateGolfer();
		}

		public void UpdateBall ()
		{
			m_gameManager.UpdateBall();
		}

		#endregion

		#region WorldUpdate

		private void HideScore ()
		{
			m_mainPanel.HideUI();
		}

		private void OpenOrbitPanel ()
		{
			this.UI.OpenPanel<OrbitPanel>();
		}

		public void OnWorldStay ()
		{
			int nextWorldId = ApplicationManager.datas.selectedWorldId + 1;
			UnlockWorld(nextWorldId);
			m_gameManager.StopOrbit();
			StartCoroutine(WaitForReset());
			/*
			Pinpin.UI.BlackScreen.Show(0.5f);
			StartCoroutine(UpdateWorld(0.5f));*/
		}

		public void OnNextWorld ()
		{
			m_worldChange = true;
			//ApplicationManager.datas.SetWorldMultiplier(ApplicationManager.datas.selectedWorldId, ApplicationManager.config.game.worldEndedGoldMultiplier);
			int nextWorldId = ApplicationManager.datas.selectedWorldId + 1;
			UnlockWorld(nextWorldId);
			ApplicationManager.datas.selectedWorldId = nextWorldId;

			WorldResetDatas();
			Pinpin.UI.BlackScreen.Show(0.5f);
			StartCoroutine(UpdateWorld(0.5f));
		}

		public void OnWorldChange ( int worldId )
		{
			m_worldChange = true;
			if (worldId < ApplicationManager.assets.planets.Length)
			{
				ApplicationManager.datas.selectedWorldId = worldId;
				if (UIShopButton.forceUpdate != null)
					UIShopButton.forceUpdate.Invoke();
				this.UI.GetPanel<ClubHousePanel>().OnWorldUpdate();
				Pinpin.UI.BlackScreen.Show(0.5f);
				StartCoroutine(UpdateWorld(0.5f));
			}
		}

		private void UnlockWorld ( int worldId )
		{
			if (worldId < ApplicationManager.assets.planets.Length)
			{
				ApplicationManager.datas.UnlockWorld(worldId);
			}
		}

		private IEnumerator UpdateWorld ( float delay )
		{
			yield return new WaitForSeconds(0.5f);

			LoadingScreen.Show(I2.Loc.ScriptLocalization.travelling_loading + " " +
				I2LocManager.GetTermTranslation(ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].name) + "...");

			yield return new WaitForSeconds(0.1f);

			m_gameManager.UpdateWorld();

			m_gameManager.StopOrbit();

			StartCoroutine(WaitForReset());

			yield return null;
		}

		private IEnumerator WaitForReset ()
		{
			yield return new WaitForSeconds(0.05f);

			ResetGame();

			yield return new WaitForSeconds(0.5f);

			m_mainPanel.ShowUI();
			BlackScreen.Hide(0.5f);
			LoadingScreen.Hide();
		}

		public void WorldResetDatas ()
		{
			ApplicationManager.SaveGameDatas();
			UpdateCurrencies();
			UpdateDistance(0);
		}

		#endregion

		#region UtilityFunctions

		private IEnumerator CloseLoadingScreen ( float delay )
		{
			yield return new WaitForSeconds(delay);

			LoadingScreen.Hide();

			yield return false;
		}

		public Vector2 GetDistanceFromUnlock ( int distance )
		{
			Vector2 anchor = Vector2.one;
			int currentGoal = Mathf.Min(ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId), ApplicationManager.config.game.characterUnlockDistance.Length - 1);
			anchor.x = Mathf.Clamp(distance / ApplicationManager.config.game.characterUnlockDistance[currentGoal], 0f, 1f);
			return anchor;
		}

		public int GetMaxCharacterUnlock ()
		{
			/*if(IsLastPlanet())
				return ApplicationManager.config.game.characterUnlockDistance.Length;
			else*/
			return ApplicationManager.config.game.characterUnlockDistance.Length - 1;
		}

		public bool IsLastPlanet ()
		{
			return ApplicationManager.datas.selectedWorldId == ApplicationManager.assets.planets.Length - 1;
		}

		public bool IsLastPlanetGoal ()
		{
			return ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) >= ApplicationManager.config.game.characterUnlockDistance.Length;
		}

		public bool IsNextWordUnlocked ()
		{
			return ApplicationManager.datas.IsWorldUnlocked(ApplicationManager.datas.selectedWorldId + 1);
		}

		#endregion

	}

}