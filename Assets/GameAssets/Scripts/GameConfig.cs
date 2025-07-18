using UnityEngine;
using System;

namespace Pinpin
{

	[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig", order = 1)]
	public class GameConfig : ScriptableObject
	{

		[Serializable]
		public class ApplicationConfig
		{
			public string version = "1.0";
			public string jsonID = "default";
			public int targetFrameRate = 60;
			public int splashScreenDuration;
			public bool enableRemoteSettings = false;
			public bool enablePurchasing = false;
			public bool enableAnalytics = false;
			public bool enableAds = false;
			public bool enableFacebook = false;
			public bool enableOfflineEarning = false;
		}

		[Serializable]
		public sealed class AdsSettings
		{
			public enum Banner
			{
				None,
				Bottom,
				Top
			}

			[Min(0f)] public float delayFirstInterstitial = 0;
			[Min(0f)] public float delayBetweenInterstitials = 0;
			[Min(0f)] public int lifeTimeShotBeforeInterstitial = 0;
			public bool isFirstInterstitial = true;

			public Banner banner;
			public bool useSmartBanner = false;
			public bool useIntertitial = true;

			public string rewardedVideoTestAdUnit;
			public string interstitialTestAdUnit;

		}

		[Serializable]
		public sealed class GameSettings
		{

			[Serializable]
			public enum WheelGiftType
			{
				//Diamond,
				Gold,
				GoldenBall,
				RedBall,
				BlueBall,
				VioletBall,
				OrangeBall,
			}

			[Serializable]
			public class BallsCost
			{
				public int coinsPrice;
				public int gemPrice;
				public bool useVideo;
			}

			[Serializable]
			public class PriceMultiplier
			{
				public int startLevel;
				public float multiplier;
			}

			[Serializable]
			public class PowerBonuses
			{
				public int startLevel;
				public float bonus;
			}

			[Serializable]
			public class JackpotGain
			{
				public int chance;
				public int multiplier;
			}

			[Serializable]
			public class IAP
			{
				public int handfullOfGemsAmount = 200;
				public int pileOfGems = 600;
				public int cupOfGemsAmount = 1500;
				public int chestOfGemsAmount = 10000;
			}

			[Serializable]
			public class WheelGift
			{
				public WheelGiftType type;
				public int amount;
			}

			public float baseCoinMultiplier = 0.2f;

			public BallsCost[] ballCosts;

			public float worldDifficultyMultiplier = 1.25f;
			public int worldEndedGoldMultiplier = 3;
			public float[] characterUnlockDistance;

			public int upgradeBasePrice = 50;
			public PriceMultiplier[] priceMultipliers;

			public float powerBase = 75f;
			public float powerPerLevel = 2.5f;
			public float powerLevelMultiplier = 0.8f;
			public PowerBonuses[] powerBonuses;

			public float minBounciness = 0.1f;
			public float maxBounciness = 0.8f;

			public int gamesForJackpot = 10;
			public JackpotGain[] jackpotMultipliers;
			public int[] holescoreMultipliers;

			public int puttType = 0;
			public int planetDifficulty = 1;
			public float moneyMultiplier = 1f;
			public float upgradePriceMultiplier = 1f;
			public float upgradeValueMultiplier = 1f;
			public float ballSqrSpeedToDestroyObstacles = 100f;

			public float minPerfectAngle = 28f;
			public float maxPerfectAngle = 34f;
			public IAP iap;

			public WheelGift[] wheelGifts;
			public bool enableStartAnimation = false;
			public bool enableChristmasEvent = false;
			public bool enableInstagramPopup = true;

			public bool useNewVideoIcon = false;
			public bool useNewFXs = false;
			public int subPopupABTest = -1;


            public int GetJackpotMultiplier ( int randValue )
			{
				int currentChance = 0;
				for (int i = 0; i < jackpotMultipliers.Length; i++)
				{
					currentChance += jackpotMultipliers[i].chance;
					if (randValue < currentChance)
					{
						return jackpotMultipliers[i].multiplier;
					}
				}
				return jackpotMultipliers[0].multiplier;
			}

			public ulong GetUpgradePrice ( int level )
			{
				double price = upgradeBasePrice;

				int priceMultiplierLevel = 0;
				for (int i = 0; i < level; i++)
				{
					if (priceMultiplierLevel < powerBonuses.Length - 1 && i == powerBonuses[priceMultiplierLevel].startLevel)
						priceMultiplierLevel++;

					price *= (double)priceMultipliers[priceMultiplierLevel].multiplier;
				}
				return (ulong)(price * upgradePriceMultiplier);
			}

			public float GetPower ( int level )
			{
				float power = powerBase;
				for (int i = 0; i < level; i++)
				{
					power += powerPerLevel;
					power += level * powerLevelMultiplier;
				}
				return power * upgradeValueMultiplier;
			}

			public float GetBonusPower ( int level )
			{
				float power = 0;
				for (int i = 0; i < level; i++)
				{
					power += powerPerLevel;
					power += level * powerLevelMultiplier;
				}
				return power * upgradeValueMultiplier;
			}

			public float GetBounciness ( int level )
			{
				float bounciness = minBounciness + (-1f / Mathf.Pow(Mathf.Exp(level), 0.1f) + 1f) * (maxBounciness - minBounciness);
				return bounciness;
			}

        }

		public ApplicationConfig application;
		public AdsSettings ads;
		public GameSettings game;
		public Action onDataLoaded;

        public void Inititialize ()
		{
			if (this.application.enableRemoteSettings)
				RemoteSettings.Updated += new RemoteSettings.UpdatedEventHandler(this.OnRemoteSettingsUpdate);
		}

		private void OnDisable ()
		{
			if (this.application.enableRemoteSettings)
				RemoteSettings.Updated -= new RemoteSettings.UpdatedEventHandler(this.OnRemoteSettingsUpdate);
		}

		public void LoadData()
		{
			/*game.puttType = RemoteSettings.GetInt("putt_type", 0);
			game.planetDifficulty = RemoteSettings.GetInt("planet_difficulty", 0);

			game.moneyMultiplier = RemoteSettings.GetFloat("money_earning_multiplier", 3f);
			game.upgradePriceMultiplier = RemoteSettings.GetFloat("stat_upgrade_price_multiplier", 1f);
			game.upgradeValueMultiplier = RemoteSettings.GetFloat("stat_upgrade_value_multiplier", 1f);


			ads.delayBetweenInterstitials = RemoteSettings.GetInt("interstitials_frequecy", 30);
			ads.lifeTimeShotBeforeInterstitial = RemoteSettings.GetInt("interstitials_lifetime_shots", 2);
			ads.delayFirstInterstitial = RemoteSettings.GetInt("interstitials_time_after_start", 0);*/
		}


        private void OnRemoteSettingsUpdate ()
		{
			Debug.Log("GameConfig - OnRemoteSettingsUpdate()");

			if (onDataLoaded != null)
				onDataLoaded.Invoke();
		}

	}

}