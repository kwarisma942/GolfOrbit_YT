using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Pinpin.UI;
using Pinpin.Scene;
using UnityScene = UnityEngine.SceneManagement.Scene;
using GameScene = Pinpin.Scene.ASceneManager.Scene;
using Pinpin.Types;
using Banner = Pinpin.GameConfig.AdsSettings.Banner;
using PaperPlaneTools;
using I2LocManager = I2.Loc.LocalizationManager;
using Assets.SimpleAndroidNotifications;
using Pinpin.Scene.MainScene.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using YTGameSDK;

namespace Pinpin
{

	[DisallowMultipleComponent]
	public class ApplicationManager : MonoBehaviour
	{
		[SerializeField] private GameConfig m_gameConfig;
		[SerializeField] private GameAssets m_gameAssets;
		[SerializeField] private GameDatas m_gameDatas;
		//[SerializeField] private RateBoxPrefabScript m_rateBoxPrefab;
		[SerializeField] private bool m_isRewardedAvailable;

		public static bool isPurchaseInitializEnd;

		private bool m_initialized = false;


		[SerializeField] private GameObject Addressable_LoadingScreen;        
        public static ApplicationManager singleton { get; set; }
		public static bool isInitialized { get { return (ApplicationManager.singleton != null); } }
		public static ASceneManager currentSceneManager { get; private set; }
		public static bool networkReachable { get { return (Application.internetReachability != NetworkReachability.NotReachable); } }
		public static bool isAnalyticsEnabled { get { return (ApplicationManager.config.application.enableAnalytics); } }
		public static string applicationVersion { get { return (ApplicationManager.config.application.version); } }
		public static bool needOfflineEarningCheck = false;
		public static bool canTakeOfflineEarning = true;

		public static TimeSpan timeSinceLastAppQuit { get; private set; }

		public static Action<bool> onNetworkReachabilityChange;
		public static Action<bool> onRewardedVideoAvailabilityChange;
		public static Action onUTCTimeUpdated;
		public static Action onVipActivated;
		public static bool oeNeedToBeClaimed = false;

		static bool forceAdAvailable = true;
		public YTGameWrapper YTGameWrapper;

		public static YTGameWrapper YTWrapper 
		{
			get { return (ApplicationManager.singleton.YTGameWrapper); }
		}
		public static GameDatas datas
		{
			get { return (ApplicationManager.singleton.m_gameDatas); }
		}

		public static GameConfig config
		{
			get { return (ApplicationManager.singleton.m_gameConfig); }
		}

		public static GameAssets assets
		{
			get { return (ApplicationManager.singleton.m_gameAssets); }
		}

		public static string currentScene
		{
			get { return (SceneManager.GetActiveScene().name); }
		}

		public static bool playerLostVIP { get; private set; }

        private const string FirstLaunchKey = "FirstTimeGameLaunch";
        public static void SetFirstTimeLaunch(bool isFirstTime)
        {
            PlayerPrefs.SetInt(FirstLaunchKey, isFirstTime ? 1 : 0);
            PlayerPrefs.Save(); // Important especially for WebGL
        }

        public static bool IsFirstTimeLaunch()
        {
            return PlayerPrefs.GetInt(FirstLaunchKey, 1) == 1; // Default is true (1)
        }
        /*#if !MOBILE || (UNITY_EDITOR && !UNITY_CLOUD_BUILD)
                private static readonly string[] gdpr_zones = new string[]{
                    "at", "be", "bg", "ch", "cy", "cz", "de", "dk", "ee", "es",
                    "fi", "fr", "gr", "hr", "hu", "ie", "it", "lt", "lu", "lv",
                    "mt", "nl", "pl", "pt", "ro", "se", "si", "sk", "uk", "gb"
                };

                public static bool ShouldAskConsent ()
                {
                    return Array.IndexOf(gdpr_zones, currentCulture.TwoLetterISOLanguageName) >= 0;
                }
        #else

                private static readonly string[] gdpr_zones = new string[]{
                    "AT", "BE", "BG", "CH", "CY", "CZ", "DE", "DK", "EE", "ES",
                    "FI", "FR", "GR", "HR", "HU", "IE", "IT", "LT", "LU", "LV",
                    "MT", "NL", "PL", "PT", "RO", "SE", "SI", "SK", "UK", "GB"
                };

                public static bool ShouldAskConsent ()
                {
                    string isoRegion = PreciseLocale.GetRegion();
                    return Array.IndexOf(gdpr_zones, isoRegion) >= 0;
                }
        #endif*/

        private void Awake ()
		{
            YTGameWrapper = GameObject.Find("YTGameWrapper").GetComponent<YTGameWrapper>();

            StartCoroutine(AwakeCo());
		}


        IEnumerator AwakeCo()
        {
            if (ApplicationManager.singleton != null)
            {
                GameObject.Destroy(this.gameObject);
#if DEBUG
                Debug.Log("ApplicationManager - Removing duplicate Object");
#endif
                yield break;
            }

#if DEBUG
            Debug.Log("ApplicationManager - Awake()");
#endif

            ApplicationManager.singleton = this;
            GameObject.DontDestroyOnLoad(this.gameObject);


            {
                // Fallback to original logic (check `itsFirstTime`)
                bool isFirstTime = PlayerPrefs.GetInt("itsFirstTime") == 0;
                if (isFirstTime)
                {
                    yield return new WaitForSeconds(2f); // Original delay for first-time users
                    yield return LoadGameAssetLibraryAddressable();
                    PlayerPrefs.SetInt("itsFirstTime", 1);
                    PlayerPrefs.Save();

                }
                else
                {
                    m_gameAssets = StaticVariables.sv_GameAsset;
                }
            }
            // GameEvents
            SceneManager.sceneLoaded += OnSceneLoaded;
            ASceneManager.onSceneReady += OnSceneReady;

            // Loading Screen
            LoadingScreen.ShowSplashScreen();
            LoadingScreen.HideSplashScreen();

            // Culture & Localization
            ApplicationManager.CheckCulture();
            I2.Loc.LocalizationManager.ParamManagers.Add(new GlobalParams());

            // Init config
            if (ApplicationManager.config.application.enableRemoteSettings && Application.isEditor)
                ApplicationManager.singleton.m_gameConfig = ScriptableObject.Instantiate(ApplicationManager.config);
            ApplicationManager.config.Inititialize();

#if TAPNATION
            StartCoroutine(CheckRewardedVideoAvailability());
#endif
        }



        public static IEnumerator ContinueLoading ()
		{
			yield return singleton.Load();
		}
		
		private bool m_purchasingInitialised = false;
		private IEnumerator Load ()
		{
			yield return null;
			// Purchasing
//#if UNITY_PURCHASING
//			if (ApplicationManager.isPurchasingEnabled)
//			{
//				PurchasingManager.Initialize(ApplicationManager.OnPurchasingManagerInitializationComplete, ApplicationManager.OnProductBought);
//				while (m_purchasingInitialised == false && ApplicationManager.networkReachable)
//					yield return null;
//			}
//#endif
			/*
#if !(UNITY_EDITOR && !UNITY_CLOUD_BUILD)
			m_rateBoxPrefab.minCustomEventsCount = RemoteSettings.GetInt("ratebox_shot_count", 11);
			m_rateBoxPrefab.postponeCooldownInHours = RemoteSettings.GetInt("ratebox_time_between_show", 8);
			m_rateBoxPrefab.delayAfterLaunchInHours = RemoteSettings.GetInt("ratebox_time_before_show", 2) / 60f;
#endif*/
			//m_rateBoxPrefab.Init();

			// Analytics
//			if (ApplicationManager.isAnalyticsEnabled)
//			{
//#if GAME_ANALYTICS
//				if (AnalyticsManager.useGameAnalytics)
//					ApplicationManager.singleton.gameObject.AddComponent<GameAnalyticsSDK.GameAnalytics>();
//#endif
//			}
			
			ApplicationManager.datas.sessionCount++;

			// Load Settings
			ApplicationManager.datas.LoadSettings();
			Application.targetFrameRate = ApplicationManager.config.application.targetFrameRate;
			QualitySettings.vSyncCount = 1;
			Time.fixedDeltaTime = 1f / Application.targetFrameRate;

			ApplicationManager.singleton.CheckAppLaunchTimes();

			
			m_initialized = true;
		}

		private void OnApplicationQuit ()
		{
			Debug.Log("OnApplicationQuit");
			if (ApplicationManager.oeNeedToBeClaimed == false)
			{
				SetupOffineEarningValues();
			}
			ApplicationManager.datas.SaveDatas();
		}

		private void OnApplicationPause ( bool pause )
		{
			if (!m_initialized)
				return;

			Debug.Log("OnApplicationPause : " + pause);
			if (pause)
			{
				if (ApplicationManager.oeNeedToBeClaimed == false)
				{
					SetupOffineEarningValues();
				}
				ApplicationManager.datas.SaveDatas();
			}
			else
			{
				if (utcInitialized)
					ApplicationManager.singleton.CheckAppFocusTimes();
			}
			ApplicationManager.datas.SaveDatas();
		}

		private void OnDestroy ()
		{
			ApplicationManager.datas.SaveDatas();
		}

		private void Update ()
		{
			if (m_timeBeforeSave > 0f)
			{
				m_timeBeforeSave -= Time.unscaledDeltaTime;
				if (m_timeBeforeSave <= 0f)
					ApplicationManager.datas.SaveDatas();
			}
		
		}

		static float m_timeBeforeSave = 0f;
		public static void SaveGameDatas ()
		{
			m_timeBeforeSave = 1f;
		}

		private void SetupOffineEarningValues ()
		{
			if (!ApplicationManager.utcInitialized)
				return;

			canTakeOfflineEarning = true;
			Debug.Log("Setup offline earnings");
			m_gameDatas.lastApplicationQuitTime = utcTime;
			int hours = RemoteSettings.GetInt("OfflineEarningNotificationTime", 48);
#if !(UNITY_EDITOR && !UNITY_CLOUD_BUILD)
			if (m_gameDatas.enableOENotifications)
			{
				DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				DateTime notificationDate = epoch.AddSeconds(ApplicationManager.utcTime + hours * 3600);
#if UNITY_IOS
				UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
				UnityEngine.iOS.LocalNotification notification = new UnityEngine.iOS.LocalNotification();
				notification.alertTitle = LocalizationManager.GetTermTranslation("offline_earnings");
				notification.alertBody = LocalizationManager.GetTermTranslation("notification_content");
				notification.fireDate = notificationDate;
				UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notification);
#elif UNITY_ANDROID

				int sdkLevel = GetSDKLevel();
				Debug.Log(sdkLevel);
				if (sdkLevel > 22 )
				{
					NotificationManager.CancelAll();
					NotificationManager.SendWithAppIcon(new TimeSpan(hours, 0, 0), I2.Loc.LocalizationManager.GetTermTranslation("offline_earnings"), I2.Loc.LocalizationManager.GetTermTranslation("notification_content"), Color.white);
				}
#endif
			}
#endif

		}


#if UNITY_ANDROID && !(UNITY_EDITOR && !UNITY_CLOUD_BUILD)
		/*public int GetSDKLevel ()
		{
			var clazz = AndroidJNI.FindClass("android/os/Build$VERSION");
			var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
			var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
			return sdkLevel;
		}*/
			public int GetSDKLevel ()
		{
			return int.Parse(SystemInfo.operatingSystem.Split('-')[1].Split(' ')[0]);
		}
#endif

		public static void SetCurrentSceneManager ( ASceneManager current )
		{
			ApplicationManager.currentSceneManager = current;

		}

		public static void LoadScene ( GameScene scene )
		{
			ApplicationManager.LoadScene(scene.ToString());
		}

        private static void LoadScene(string name)
        {
#if DEBUG
            Debug.Log("ApplicationManager - Loading scene '" + name + "'");
#endif

            ApplicationManager.SaveGameDatas();
            LoadingScreen.Show();
            LoadingScreen.LoadSceneWithProgress(name);
        }

        //private IEnumerator LoadSceneAsyncWithProgress(string sceneName)
        //{
        //    AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        //    asyncOp.allowSceneActivation = false;

        //    while (!asyncOp.isDone)
        //    {
        //        float targetProgress = Mathf.Clamp01(asyncOp.progress / 0.9f); // Normalized 0–1
        //        LoadingScreen.UpdateProgress(targetProgress);

        //        // Allow scene activation once progress nears 100%
        //        if (targetProgress >= 0.99f)
        //        {
        //            yield return new WaitForSeconds(0.5f); // Optional small delay
        //            asyncOp.allowSceneActivation = true;
        //        }

        //        yield return null;
        //    }
        //}


        public static void MuteSound ()
		{
#if DEBUG
			Debug.Log("ApplicationManager - Muting sound.");
#endif
			ApplicationManager.assets.audioMixer.SetFloat("Master", -80f);
		}

		public static void UnMuteSound ()
		{
#if DEBUG
			Debug.Log("ApplicationManager - UnMuting sound.");
#endif
			ApplicationManager.assets.audioMixer.SetFloat("Master", singleton.m_gameDatas.isSoundActive ? 0f : -80f);
		}

		private void OnSceneLoaded ( UnityScene scene, LoadSceneMode mode )
		{
			if (mode != LoadSceneMode.Additive)
			{
#if DEBUG
				Debug.Log("ApplicationManager - Scene changed to '" + scene.name + "'");
#endif
			}
		}

		private void OnSceneReady ()
		{
#if DEBUG
			Debug.Log("ApplicationManager - Scene Ready.");
#endif
			LoadingScreen.Hide();
		}

		public static void Quit ()
		{
#if DEBUG
			Debug.Log("ApplicationManager - Quitting...");
#endif

#if (UNITY_EDITOR && !UNITY_CLOUD_BUILD)
			UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
			AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call<bool>("moveTaskToBack", true);
#else
			Application.Quit();
#endif
		}

		#region Ads
		public static bool isAdsEnabled { get { return (ApplicationManager.config.application.enableAds); } }


		public static bool canWatchInterstitial
		{
			get
			{
				if (!ApplicationManager.isAdsEnabled)
					return false;

				if (PlayerPrefs.GetInt("NoAds", 0) == 1)
					return false;

				if (datas.noAds)
					return false;

				if (datas.vip)
					return false;

				if (ApplicationManager.datas.lifetimeShots <= ApplicationManager.config.ads.lifeTimeShotBeforeInterstitial)
					return false;

				//if (!MaxSdk.IsInitialized())
				//	return false;

				if (!QP_QuietObject.instance.QP_Can_Show_Inter())
					return false;

				//if (FGMediation.IsInterstitialReady)
				//{
				//	return true;
				//}
				return false;
			}
		}

		public static bool canWatchRewardedVideo
		{
			get
			{
				if (forceAdAvailable) return true;
#if UNITY_EDITOR
				if (!isInitialized || !singleton.m_isRewardedAvailable)
					return false;
#endif
				if (!ApplicationManager.isAdsEnabled)
					return false;

				if (Application.internetReachability == NetworkReachability.NotReachable)
					return false;

				//if (!MaxSdk.IsInitialized())
				//	return false;

				//return FGMediation.IsRewardedReady;
				return false;
			}
		}

		public static bool canShowBanner
		{
			get
			{
#if CAPTURE
				return false;
#endif

				if (!ApplicationManager.isAdsEnabled)
					return false;

				if (PlayerPrefs.GetInt("NoAds", 0) == 1)
					return false;

				if (datas.noAds)
					return false;

				if (datas.vip)
					return false;

				//if (!MaxSdk.IsInitialized())
				//	return false;

				return true;
			}
		}

		#endregion

		#region Culture & Localization

		public static CultureInfo currentCulture { get; private set; }

		public static Language language
		{
			get { return (ApplicationManager.datas.language); }
			set { ApplicationManager.datas.language = value; }
		}

		// Gets the current culture
		private static void CheckCulture ()
		{
			SystemLanguage language = Application.systemLanguage;
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
			ApplicationManager.currentCulture = cultures.FirstOrDefault(x => x.EnglishName.Contains(Enum.GetName(language.GetType(), language)));

			if (ApplicationManager.currentCulture == null)
				ApplicationManager.currentCulture = cultures.FirstOrDefault(x => x.EnglishName.Contains("English"));
			
#if DEBUG
			Debug.Log("ApplicationManager - Current culture set to [" + ApplicationManager.currentCulture + "]");
#endif
		}


		public class GlobalParams : I2.Loc.ILocalizationParamsManager
		{
			public string GetParameterValue ( string Param )
			{
				if (Param == "WEEKLY_PRICE")
				{
					string price = PurchasingManager.getProductPriceString("com.gpp.golforbit.weeklypremiumsubscription2");
					if (price == null)
					{
						price = "$7.99";
					}
					return price;
				}
				else if (Param == "new_planet")
				{
					return I2LocManager.GetTermTranslation(assets.planets[datas.selectedWorldId + 1].name);
				}
				else if (Param == "leaving_planet")
				{
					return I2LocManager.GetTermTranslation(assets.planets[datas.selectedWorldId].name);
				}
				else if(Param == "distance_unit_abreviation")
				{
					return I2LocManager.GetTermTranslation("distance_unit_abreviation");
				}
				return null;
			}
		}

#endregion

		bool lastNetworkReachable = false;
		private IEnumerator CheckNetworkReachability ()
		{
			WaitForSeconds checkTime = new WaitForSeconds(1);
			while (true)
			{
				if (lastNetworkReachable != networkReachable)
				{
					lastNetworkReachable = networkReachable;
					if (onNetworkReachabilityChange != null)
						onNetworkReachabilityChange.Invoke(networkReachable);
				}
				yield return checkTime;
			}
		}
		bool lastRewardedVideoAvailable = false;
		private IEnumerator CheckRewardedVideoAvailability ()
		{
			WaitForSeconds checkTime = new WaitForSeconds(1);
			while (true)
			{
				if (lastRewardedVideoAvailable != canWatchRewardedVideo)
				{
					lastRewardedVideoAvailable = canWatchRewardedVideo;
					if (onRewardedVideoAvailabilityChange != null)
						onRewardedVideoAvailabilityChange.Invoke(lastRewardedVideoAvailable);
				}
				yield return checkTime;
			}
		}

		#region App Timestamps

		public static bool utcInitialized { get; set; }
		public static uint lastUtcLaunchTime { get; private set; }
		public static uint utcLaunchTime { get; private set; }
		public static uint utcTime
		{
			get
			{
				DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

				TimeSpan timeSpan = (DateTime.UtcNow - epoch);
				return (uint)timeSpan.TotalSeconds;
			}
		}
		//public static uint utcTime { get { return (ApplicationManager.utcLaunchTime + (uint)Time.realtimeSinceStartup); } }
		public static uint utcTomorrow { get; private set; }
		public static bool newDay { get; set; }
		public static TimeSpan timeSinceFirstLaunch { get; private set; }

		// Retrieve time and do checks
		private void CheckAppLaunchTimes ()
		{
			/*UnityEngine.Networking.UnityWebRequest request;
			request = UnityEngine.Networking.UnityWebRequest.Get("https://timestamp.pinpinteam.com/timestamp.php");

			yield return (request.SendWebRequest());

			if (request.responseCode != 200)
			{
				ApplicationManager.utcInitialized = false;
				yield break; // NO INTERNET
			}

			string html = request.downloadHandler.text;
			uint utcTimestamp = uint.Parse(html);*/
			DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			TimeSpan timeSpan = (DateTime.UtcNow - epoch);
			uint utcTimestamp = (uint)timeSpan.TotalSeconds;


			DateTime lastLaunchDate = epoch.AddSeconds(m_gameDatas.lastApplicationLaunchTime);
			DateTime lastQuitDate = epoch.AddSeconds(m_gameDatas.lastApplicationQuitTime);
			DateTime launchDate = epoch.AddSeconds(utcTimestamp);
			DateTime lastVIPDailyGiftDate = epoch.AddSeconds(m_gameDatas.lastVIPDailyGiftTime);

			// store last app launch
			if (m_gameDatas.firstApplicationLaunchTime == 0)
				m_gameDatas.firstApplicationLaunchTime = utcTimestamp;
			DateTime firstApplicationLaunchTime = epoch.AddSeconds(m_gameDatas.firstApplicationLaunchTime);
			ApplicationManager.timeSinceFirstLaunch = launchDate - firstApplicationLaunchTime;
			ApplicationManager.utcLaunchTime = utcTimestamp;

			// check new day
			if (lastVIPDailyGiftDate.DayOfYear != launchDate.DayOfYear)
				newDay = true;

			// check consecutive days
			int daysDiff = launchDate.DayOfYear - lastLaunchDate.DayOfYear;
			if (daysDiff == 1)
				m_gameDatas.consecutiveDaysLaunches++;
			else if (daysDiff > 1)
				m_gameDatas.consecutiveDaysLaunches = 1;

			timeSinceLastAppQuit = new TimeSpan(0);
			//Store time since last app launch
			if (m_gameDatas.lastApplicationQuitTime != 0)
				timeSinceLastAppQuit = launchDate - lastQuitDate;

			needOfflineEarningCheck = true;
#if DEBUG
			Debug.Log("ApplicationManager - Last app Launch date : " + lastLaunchDate);
			Debug.Log("ApplicationManager - Current app Launch date : " + launchDate);
			Debug.Log("ApplicationManager - Consecutive days launches : " + m_gameDatas.consecutiveDaysLaunches);
			Debug.Log("ApplicationManager - Time since last app launch : " + timeSinceLastAppQuit.TotalMinutes + " minutes");
#endif


			ApplicationManager.utcInitialized = true;
		}


		// Retrieve time and do checks
		private void CheckAppFocusTimes ()
		{
			/*UnityEngine.Networking.UnityWebRequest request;
			request = UnityEngine.Networking.UnityWebRequest.Get("https://timestamp.pinpinteam.com/timestamp.php");

			yield return (request.SendWebRequest());

			if (request.responseCode != 200)
			{
				yield break; // NO INTERNET
			}

			string html = request.downloadHandler.text;
			uint utcTimestamp = uint.Parse(html); */
			DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			TimeSpan timeSpan = (DateTime.UtcNow - epoch);
			uint utcTimestamp = (uint)timeSpan.TotalSeconds;

			DateTime lastQuitDate = epoch.AddSeconds(m_gameDatas.lastApplicationQuitTime);
			DateTime launchDate = epoch.AddSeconds(utcTimestamp);
			DateTime lastVIPDailyGiftDate = epoch.AddSeconds(m_gameDatas.lastVIPDailyGiftTime);


			// store last app launch
			m_gameDatas.lastApplicationLaunchTime = utcTimestamp;

			// check new day
			if (lastQuitDate.DayOfYear != launchDate.DayOfYear)
				newDay = true;

			//Store time since last app launch
			timeSinceLastAppQuit = launchDate - lastQuitDate;

			needOfflineEarningCheck = true;
#if DEBUG
			Debug.Log("ApplicationManager - Current app Focus date : " + launchDate);
			Debug.Log("ApplicationManager - Time since last app quit : " + timeSinceLastAppQuit.TotalMinutes + " minutes");
#endif

			if (onUTCTimeUpdated != null)
				onUTCTimeUpdated.Invoke();

		}

		public static TimeSpan GetTimeSinceFirstLaunch ()
		{
			DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			DateTime firstApplicationLaunchTime = epoch.AddSeconds(datas.firstApplicationLaunchTime);
			TimeSpan timeSinceFirstLaunch = DateTime.UtcNow - firstApplicationLaunchTime;
			return timeSinceFirstLaunch;
		}

		#endregion

		#region Purchasing

		public static bool isPurchasingEnabled { get { return (ApplicationManager.config.application.enablePurchasing); } }

		// Called when Purchasing availibility changed
		// call when your reactivate internet if previously fail cause of non network connection)
		private static void OnPurchasingManagerInitializationComplete ( bool isInitialized )
		{
#if DEBUG
			Debug.Log("ApplicationManager - Purchasing Manager initialized [" + isInitialized + "]");
#endif
			singleton.m_purchasingInitialised = true;
			
            if (GPlayPassManager.IsActive)
            {
				//PlayerPrefs.SetString("UnlockVIP", "yearly_sub");
				ApplicationManager.datas.vip = true;
				if (ApplicationManager.datas.selectedBallId == 0)
					ApplicationManager.datas.selectedBallId = 1;
				ApplicationManager.SaveGameDatas();
				if (onVipActivated != null)
				{
					Debug.LogError("Testttt");
					onVipActivated.Invoke();
				}
            }
            else
            {
				Debug.Log("UnlockVIP " + PlayerPrefs.GetString("UnlockVIP", "null"));
				if (PlayerPrefs.GetString("UnlockVIP", "null") != "null")
					UnlockVIP(PlayerPrefs.GetString("UnlockVIP", "null"));
                else
				{
					ApplicationManager.datas.vip = false;
					ApplicationManager.SaveGameDatas();
				}

			}

			if (isInitialized)
				CheckVIP();
		}


		private static void CheckVIP ()
		{
			bool wasVIP = datas.vip;
//#if !(UNITY_EDITOR && !UNITY_CLOUD_BUILD)
		//	datas.vip = PurchasingManager.isSubscriptionActive("com.gpp.golforbit.weeklypremiumsubscription2") || PurchasingManager.isSubscriptionActive("com.gpp.golforbit.monthlypremiumsubscription2") || PurchasingManager.isSubscriptionActive("com.gpp.golforbit.yearlypremiumsubscription2");
//#endif
			if (!datas.vip)
			{
				if (wasVIP)
				{
					playerLostVIP = true;
				}

				if (datas.selectedCharacterType == 1)
				{
					datas.selectedCharacterId = 0;
					datas.selectedCharacterType = 0;
				}
				if (datas.selectedBallId == 1 && datas.GetBallCount(datas.selectedBallId) == 0)
					datas.selectedBallId = 0;
			}
			else if (!wasVIP)
			{
				if (datas.selectedBallId == 0)
					datas.selectedBallId = 1;
			}
			datas.SaveDatas();
		}

		// Called when product bought
		private static void OnProductBought ( string productId )
		{
#if DEBUG
			Debug.Log("ApplicationManager - IAP Product bought [" + productId + "]");
#endif
			switch (productId)
			{
				case "com.gpp.gosmalliap":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.handfullOfGemsAmount;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gomediumiap":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.pileOfGems;
					//ApplicationManager.datas.noAds = true;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gobigiap":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.cupOfGemsAmount;
					//ApplicationManager.datas.noAds = true;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gogiganticiap":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.chestOfGemsAmount;
					//ApplicationManager.datas.noAds = true;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gosmalliap2":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.handfullOfGemsAmount;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gomediumiap2":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.pileOfGems;
					//ApplicationManager.datas.noAds = true;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gobigiap3":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.cupOfGemsAmount;
					//ApplicationManager.datas.noAds = true;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.gogiganticiap2":
					ApplicationManager.datas.diamonds += ApplicationManager.config.game.iap.chestOfGemsAmount;
					//ApplicationManager.datas.noAds = true;
					ApplicationManager.SaveGameDatas();
					break;

				case "com.gpp.golforbit.weeklypremiumsubscription2":
					UnlockVIP("weekly_sub");
					break;

				case "com.gpp.golforbit.monthlypremiumsubscription2":
					UnlockVIP("monthly_sub");
					break;

				case "com.gpp.golforbit.yearlypremiumsubscription2":
					UnlockVIP("yearly_sub");
					break;

				case "com.gpp.golforbit.weeklypremium":
					UnlockVIP("weekly_sub");
					break;

				case "com.gpp.golforbit.monthlypremium":
					UnlockVIP("monthly_sub");
					break;

				case "com.gpp.golforbit.yearlypremiums":
					UnlockVIP("yearly_sub");
					break;
				case "google_play_pass":
					GPlayPassManager.IsActive = true;
					break;
				case "noads":
					PlayerPrefs.SetInt("NoAds", 1);
					QP_QuietObject.instance.Close_RemoveAds();
					QP_QuietObject.instance.Close_BannerClose_UI();
					break;
				default:
#if DEBUG
					Debug.LogError("ApplicationManager - Unrecognized product id");
#endif
					break;
			}
			string transactionid = PurchasingManager.getProductTransactionID(productId);
			double Price = (double)PurchasingManager.getProductPrice(productId);
			string CurrencyCode = PurchasingManager.getProductCurrencyCode(productId);
			Debug.LogError("Old Price " + Price+ CurrencyCode);
			Price -= ((Price * ExampleRemoteConfigABtests._instance.TaxIAP)/100);
			Debug.LogError("New Price " + Price + CurrencyCode);
		}

		private static void UnlockVIP ( string subscriptionDuration )
		{
			Debug.LogError("UnlockVIP");
			PlayerPrefs.SetInt("NoAds", 1);
			ApplicationManager.datas.diamonds += 100;
			ApplicationManager.datas.coins += 100000;
			if(UpdateStore.Instance)
				UpdateStore.Instance.ShopPanel.UpdateCurrencies();
			UpdateValue.Instance.UpdateText();
			PlayerPrefs.SetString("UnlockVIP", subscriptionDuration);
			ApplicationManager.datas.vip = true;
			if (ApplicationManager.datas.selectedBallId == 0)
				ApplicationManager.datas.selectedBallId = 1;
			ApplicationManager.SaveGameDatas();
			if (onVipActivated != null)
            {
				Debug.LogError("Testttt");
				onVipActivated.Invoke();

            }
  
		}

        #endregion


        #region Addressables

        public GameAssets GameAssetsAddressables { get; private set; }


        public IEnumerator LoadGameAssetLibraryAddressable()
        {
            Debug.Log("Loading Desinger Library");
            if (GameAssetsAddressables == null)
            {
				Addressable_LoadingScreen.SetActive(true);

                string label = "Library";
                Debug.Log($"Loading AddressableDesignerLibrary from Addressables with label: {label}");
                AsyncOperationHandle<GameAssets> handle = Addressables.LoadAssetAsync<GameAssets>(label);
                yield return handle;

                yield return new WaitUntil(() => handle.IsDone);
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Successfully loaded AccessoriesLibrary from Addressables.");
                    GameAssetsAddressables = handle.Result;
                    m_gameAssets = handle.Result;
					StaticVariables.sv_GameAsset = m_gameAssets;
                }
                else
                {
                    Debug.LogError("Failed to load AccessoriesLibrary from Addressables.");
                }
				Addressable_LoadingScreen.SetActive(false);
            }

            else
            {
                m_gameAssets = GameAssetsAddressables;
            }
        }
        #endregion

    }

}