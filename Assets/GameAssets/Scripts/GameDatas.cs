using UnityEngine;
using Pinpin.Types;
using I2.Loc;
using System;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace Pinpin
{

	[CreateAssetMenu(fileName = "GameDatas", menuName = "Game/GameDatas", order = 1)]
	public partial class GameDatas : ScriptableObject
	{

		public static event Action onLoadComplete;
		[SerializeField, ShowOnly] private SavedDatas m_savedDatas = new SavedDatas();
		[SerializeField, ShowOnly] private PlayerPrefDatas m_playerPrefDatas = new PlayerPrefDatas();

		private bool dirty { get; set; }

		public bool noAds
		{
			get { return (m_savedDatas.noAds); }
			set
			{
				this.dirty = true;
				m_savedDatas.noAds = value;
			}
		}

		public bool vip
		{
			get { return (m_savedDatas.vip); }
			set
			{
				this.dirty = true;
				m_savedDatas.vip = value;
			}
		}

		public bool firstTimeVIPPurchaseDone
		{
			get { return (m_savedDatas.firstTimeVIPPurchaseDone); }
			set
			{
				this.dirty = true;
				m_savedDatas.firstTimeVIPPurchaseDone = value;
			}
		}

		public bool enableOENotifications
		{
			get { return (m_playerPrefDatas.enableOENotifications); }
			set
			{
				this.dirty = true;
				m_playerPrefDatas.enableOENotifications = value;
			}
		}

		public bool firstTimeOEPopup
		{
			get { return (m_playerPrefDatas.firstTimeOEPopup); }
			set
			{
				this.dirty = true;
				m_playerPrefDatas.firstTimeOEPopup = value;
			}
		}

		public bool alreadyUsedAGoldenBall
		{
			get { return (m_savedDatas.alreadyUsedAGoldenBall); }
			set
			{
				this.dirty = true;
				m_savedDatas.alreadyUsedAGoldenBall = value;
			}
		}

		public int sessionCount
		{
			get { return (m_savedDatas.sessionCount); }
			set
			{
				this.dirty = true;
				m_savedDatas.sessionCount = value;
			}
		}

		public bool haveGDPRConsent
		{
			get { return (m_savedDatas.GDPRConsent); }
			set
			{
				this.dirty = true;
				m_savedDatas.GDPRConsent = value;
			}
		}

		public int rateboxShown
		{
			get { return (m_savedDatas.rateboxShown); }
			set
			{
				this.dirty = true;
				m_savedDatas.rateboxShown = value;
			}
		}

		public bool rvCount48sent
		{
			get { return (m_savedDatas.rvCount48sent); }
			set
			{
				this.dirty = true;
				m_savedDatas.rvCount48sent = value;
			}
		}
		public bool rvCount72sent
		{
			get { return (m_savedDatas.rvCount72sent); }
			set
			{
				this.dirty = true;
				m_savedDatas.rvCount72sent = value;
			}
		}

		public uint lastApplicationLaunchTime
		{
			get { return (m_savedDatas.lastApplicationLaunchTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastApplicationLaunchTime = value;
			}
		}

		public uint firstApplicationLaunchTime
		{
			get { return (m_savedDatas.firstApplicationLaunchTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.firstApplicationLaunchTime = value;
			}
		}

		public uint lastApplicationQuitTime
		{
			get { return (m_savedDatas.lastApplicationQuitTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastApplicationQuitTime = value;
			}
		}

		public uint lastFreeGiftTime
		{
			get { return (m_savedDatas.lastFreeGiftTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastFreeGiftTime = value;
			}
		}

		public uint lastVIPDailyGiftTime
		{
			get { return (m_savedDatas.lastVIPDailyGiftTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastVIPDailyGiftTime = value;
			}
		}

		public uint lastNoAdSubPopupSeenTime
		{
			get { return (m_savedDatas.lastNoAdSubPopupSeenTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastNoAdSubPopupSeenTime = value;
			}
		}

		public uint lastWeeklySubPopupSeenTime
		{
			get { return (m_savedDatas.lastWeeklySubPopupSeenTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastWeeklySubPopupSeenTime = value;
			}
		}

		public uint lastChristmasEventSeenTime
		{
			get { return (m_savedDatas.lastChristmasEventSeenTime); }
			set
			{
				this.dirty = true;
				m_savedDatas.lastChristmasEventSeenTime = value;
			}
		}
		

		public uint consecutiveDaysLaunches
		{
			get { return (m_savedDatas.consecutiveDaysLaunches); }
			set
			{
				this.dirty = true;
				m_savedDatas.consecutiveDaysLaunches = value;
			}
		}

        public bool isSoundActive
        {
            get { return m_playerPrefDatas.soundActive; }
            set
            {
                this.dirty = true;
                m_playerPrefDatas.soundActive = value;

                // For WebGL, we adjust volume through the AudioListener directly
                if (value)
                {
                    AudioListener.volume = 1f; // Set volume to normal
                }
                else
                {
                    AudioListener.volume = 0f; // Mute audio
                }
            }
        }


        public bool isVibrationActive
		{
			get { return (m_playerPrefDatas.vibrationActive); }
			set
			{
				this.dirty = true;
				m_playerPrefDatas.vibrationActive = value;
			}
		}

		public float musicVolume
		{
			get { return (m_playerPrefDatas.musicVolume); }
			set
			{
				this.dirty = true;
				m_playerPrefDatas.musicVolume = value;
				ApplicationManager.assets.audioMixer.SetFloat("Music", -80f + value * 80f);
			}
		}

		public float sfxVolume
		{
			get { return (m_playerPrefDatas.sfxVolume); }
			set
			{
				this.dirty = true;
				m_playerPrefDatas.sfxVolume = value;
				ApplicationManager.assets.audioMixer.SetFloat("Sfx", -80f + value * 80f);
			}
		}

		public Language language
		{
			get { return (m_playerPrefDatas.language); }
			set
			{

#if DEBUG
				Debug.Log("GameDatas - Setting language to [" + value.ToString() + "]");
#endif

				if (LocalizationManager.HasLanguage(value.ToString()))
				{
					this.dirty = true;
					m_playerPrefDatas.language = value;
					LocalizationManager.CurrentLanguage = value.ToString();
				}
				else
					Debug.LogError("GameDatas - I2 Language does Not Exist");
			}
		}

		public int bestScore
		{
			get { return (m_savedDatas.bestScore); }
			set
			{
				this.dirty = true;
				m_savedDatas.bestScore = value;
			}
		}

        //public ulong coins
        //{
        //    get { return m_savedDatas.GetSelectedWorldData().coins; }
        //    set
        //    {
        //        this.dirty = true;
        //        var data = m_savedDatas.GetSelectedWorldData();
        //        data.coins = value;
        //        GameDatas.SavedDatas.SaveDatas(m_savedDatas); //  Correct call
        //    }
        //}
        public ulong coins
        {
            get { return (m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].coins); }
            set
            {
                this.dirty = true;
                m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].coins = value;
                GameDatas.SavedDatas.SaveDatas(m_savedDatas); //  Correct call
				Debug.Log(m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].coins + " Current world Coins");
            }
        }

        /*public int coins
		{
			get { return (m_savedDatas.coins); }
			set
			{
				this.dirty = true;
				m_savedDatas.coins = value;
			}
		}*/

        public int diamonds
		{
			get { return (m_savedDatas.diamonds); }
			set
			{
				this.dirty = true;
				m_savedDatas.diamonds = value;
			}
		}
        public int strengthLevel
        {
            get { return (m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].strengthLevel); }
            set
            {
                this.dirty = true;
                m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].strengthLevel = value;
            }
        }

        public int speedLevel
        {
            get { return (m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].speedLevel); }
            set
            {
                this.dirty = true;
                m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].speedLevel = value;
            }
        }

        public int bounceLevel
        {
            get { return (m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].bounceLevel); }
            set
            {
                this.dirty = true;
                m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].bounceLevel = value;
            }
        }


        public bool isFirstJackpot
		{
			get { return (m_savedDatas.isFirstJackpot); }
			set
			{
				this.dirty = true;
				m_savedDatas.isFirstJackpot = value;
			}
		}

		public int jackpotProgression
		{
			get { return (m_savedDatas.jackpotProgression); }
			set
			{
				this.dirty = true;
				m_savedDatas.jackpotProgression = value;
			}
		}

		/*public int goldenBallCount
		{
			get { return (m_savedDatas.goldenBallCount); }
			set
			{
				this.dirty = true;
				m_savedDatas.goldenBallCount = value;
			}
		}*/

		public int lifetimeShots
		{
			get { return (m_savedDatas.lifetimeShots); }
			set
			{
				this.dirty = true;
				m_savedDatas.lifetimeShots = value;
			}
		}

		public uint lifetimeRewardedShown
		{
			get { return (m_savedDatas.lifetimeRewardedShown); }
			set
			{
				this.dirty = true;
				m_savedDatas.lifetimeRewardedShown = value;
			}
		}

		public DaytimeMaterial.DayTime dayTime
		{
			get { return m_savedDatas.dayTime; }
			set
			{
				this.dirty = true;
				m_savedDatas.dayTime = value;
			}
		}

		public int GetUpgradeLevel ( Pinpin.UI.UIShopButton.UpgradeType type )
		{
			switch (type)
			{
				case UI.UIShopButton.UpgradeType.Strength:
					return strengthLevel;
				case UI.UIShopButton.UpgradeType.Speed:
					return speedLevel;
				case UI.UIShopButton.UpgradeType.Bounce:
					return bounceLevel;
				default:
					return 0;
			}
		}

		public void SetUpgradeLevel ( Pinpin.UI.UIShopButton.UpgradeType type, int level )
		{
			switch (type)
			{
				case UI.UIShopButton.UpgradeType.Strength:
					strengthLevel = level;
					break;
				case UI.UIShopButton.UpgradeType.Speed:
					speedLevel = level;
					break;
				case UI.UIShopButton.UpgradeType.Bounce:
					bounceLevel = level;
					break;
			}
		}

		#region Character

		public int selectedCharacterId
		{
			get { return (m_savedDatas.selectedCharacterId); }
			set
			{
				this.dirty = true;
				m_savedDatas.selectedCharacterId = value;
			}
		}

		public int selectedCharacterType
		{
			get { return (m_savedDatas.selectedCharacterType); }
			set
			{
				this.dirty = true; m_savedDatas.selectedCharacterType = value;
			}
		}

		public int unlockedCharacterCount { get { return (m_savedDatas.unlockedCharacters.Count); } }

		public bool IsCharacterUnlocked ( int characterId )
		{
			return (m_savedDatas.unlockedCharacters.Contains(characterId));
		}

		public void UnlockCharacter ( int characterId )
		{
			if (!m_savedDatas.unlockedCharacters.Contains(characterId))
			{
				m_savedDatas.unlockedCharacters.Add(characterId);
			}
		}

		public int UnlockRandomCharacter ()
		{
			if (m_savedDatas.unlockedCharacters.Count >= ApplicationManager.assets.golfers.Length)
				return -1;

			int randChar = 0;
			do
			{
				randChar = UnityEngine.Random.Range(0, ApplicationManager.assets.golfers.Length);
			}
			while (m_savedDatas.unlockedCharacters.Contains(randChar));

			m_savedDatas.unlockedCharacters.Add(randChar);
			this.dirty = true;

			return randChar;
		}

        public bool isInstagramCharacterUnlocked
        {
            get { return m_savedDatas.isInstagramCharacterUnlocked; }
            set
			{
				m_savedDatas.isInstagramCharacterUnlocked = value;
				this.dirty = true;
			}
		}

		public bool isInstagramShown
		{
			get { return m_savedDatas.isInstagramShown; }
			set
			{
				m_savedDatas.isInstagramShown = value;
				this.dirty = true;
			}
		}

		#endregion

		#region Ball

		public int selectedBallId
		{
			get { return (m_savedDatas.selectedBallId); }
			set
			{
				this.dirty = true;
				m_savedDatas.selectedBallId = value;
			}
		}

		public int unlockedBallCount { get { return (m_savedDatas.unlockedBalls.Count); } }

		public bool IsBallUnlocked ( int ballId )
		{
			if (ballId == 1)
				return vip;
			else
				return (m_savedDatas.unlockedBalls.Contains(ballId));
		}

		public void UnlockBall ( int ballId )
		{
			if (m_savedDatas.unlockedBalls.Contains(ballId) == false)
				m_savedDatas.unlockedBalls.Add(ballId);
			this.dirty = true;
		}

		public int GetBallCount ( int ballId )
		{
			if (m_savedDatas.temporaryBallLaunches.ContainsKey(ballId))
			{
				return (m_savedDatas.temporaryBallLaunches[ballId]);
			}
			return (0);
		}

		public void AddBalls ( int ballId, int count )
		{
			if (m_savedDatas.temporaryBallLaunches.ContainsKey(ballId))
				m_savedDatas.temporaryBallLaunches[ballId] += count;
			else
				m_savedDatas.temporaryBallLaunches.Add(ballId, count);
			this.dirty = true;
		}
		public int RemoveBall ( int ballId )
		{
			if (m_savedDatas.temporaryBallLaunches.ContainsKey(ballId))
			{
				this.dirty = true;
				return (m_savedDatas.temporaryBallLaunches[ballId] -= 1);
			}
			else
				return (0);
		}
		#endregion

		#region World

		public int selectedWorldId
		{
			get { return (m_savedDatas.selectedWorldId); }
			set
			{
				this.dirty = true;
				m_savedDatas.selectedWorldId = value;
			}
		}

		public int unlockedWorldCount { get { return (m_savedDatas.unlockedWorlds.Count); } }

		public bool IsWorldUnlocked ( int worldId )
		{
			return (m_savedDatas.unlockedWorlds.Contains(worldId));
		}

        public void UnlockWorld(int worldId)
        {
            if (!m_savedDatas.unlockedWorlds.Contains(worldId))
            {
                m_savedDatas.unlockedWorlds.Add(worldId);

                if (!m_savedDatas.worldDatas.ContainsKey(worldId))
                {
                    m_savedDatas.worldDatas[worldId] = new SavedDatas.WorldData(1, 0, 0);
                }

                this.dirty = true;
                GameDatas.SavedDatas.SaveDatas(m_savedDatas);
            }
        }

        public int GetWorldMultiplier(int worldId)
        {
            return m_savedDatas.worldDatas.TryGetValue(worldId, out var world)
                ? world.goldMultiplier
                : 1;
        }

        public int GetWorldCurrentGoal(int worldId)
        {
            return m_savedDatas.worldDatas.TryGetValue(worldId, out var world)
                ? world.currentGoal
                : 0;
        }

        public void IncrementWorldCurrentGoal(int worldId)
        {
            if (m_savedDatas.worldDatas.TryGetValue(worldId, out var world))
            {
                world.currentGoal++;
                this.dirty = true;
                GameDatas.SavedDatas.SaveDatas(m_savedDatas);
            }
        }

        public int GetWorldBestScore(int worldId)
        {
            return m_savedDatas.worldDatas.TryGetValue(worldId, out var world)
                ? world.highScore
                : 0;
        }

        public void SetWorldBestScore(int worldId, int score)
        {
            if (m_savedDatas.worldDatas.TryGetValue(worldId, out var world))
            {
                world.highScore = score;
                this.dirty = true;
                GameDatas.SavedDatas.SaveDatas(m_savedDatas);
            }
        }

        #endregion


        #region Loading

        public void LoadSettings ()
		{
#if DEBUG
			Debug.Log("GameDatas - Loading settings");
#endif

			// Load Music settings, Graphics, Language, etc

			// Load Sounds Settins
			this.isSoundActive = this.isSoundActive;
			this.musicVolume = this.musicVolume;
			this.sfxVolume = this.sfxVolume;

			// Translation
			this.language = this.language;

			this.dirty = false; // DO NOT After that

#if DEBUG
			Debug.Log("GameDatas - Settings Loaded");
#endif
		}

        public void LoadDatas()
        {
            // Load local data immediately

            //m_savedDatas = SavedDatas.LoadDatas();
            SavedDatas.LoadCloudDatas(OnCloudDatasLoaded);

            m_playerPrefDatas = PlayerPrefDatas.LoadDatas();
            // Start cloud load (non-coroutine version)
        }

        private void OnCloudDatasLoaded(SavedDatas cloudData)
        {

            m_savedDatas = cloudData;
			if (m_savedDatas == null)
				m_savedDatas = SavedDatas.LoadDatas();
			else 
			{
                MergeCloudData(cloudData);
                SavedDatas.SaveDatas(m_savedDatas);
			}

#if DEBUG
            Debug.Log("GameDatas - Data Loaded");
            LogDataState();
#endif

            GameDatas.onLoadComplete?.Invoke();
        }

        private void MergeCloudData(SavedDatas cloudData)
        {
            // Preserve critical local settings
            bool localNoAds = m_savedDatas.noAds;
            bool localVIP = m_savedDatas.vip;
            int localSelectedCharacter = m_savedDatas.selectedCharacterId;
            int localSelectedWorld = m_savedDatas.selectedWorldId;

            // Copy cloud data
            m_savedDatas = JsonConvert.DeserializeObject<SavedDatas>(JsonConvert.SerializeObject(cloudData));


            // Restore important local preferences
            m_savedDatas.noAds = localNoAds || cloudData.noAds;
            m_savedDatas.vip = localVIP || cloudData.vip;
            m_savedDatas.selectedCharacterId = localSelectedCharacter;
            m_savedDatas.selectedWorldId = localSelectedWorld;
        }

        public void ForceSave()
        {
            dirty = true;
            SaveDatas();
        }

        public void SaveDatas()
        {
#if DEBUG
            Debug.Log("GameDatas - Saving");
#endif

            if (dirty)
            {
                // Local save
                SavedDatas.SaveDatas(m_savedDatas);
                //Debug.LogError("GameDatas - Saving + Dirty Data ");
                m_playerPrefDatas.SaveDatas();
//				Debug.LogError(m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].coins + " While Saving Coins worlds");
                // Cloud save (fire-and-forget)
                SavedDatas.SaveCloudDatas(m_savedDatas);

                dirty = false;

#if DEBUG
                Debug.Log("GameDatas - Saved");
                LogDataState();
#endif
            }
#if DEBUG
            else
            {
                Debug.Log("GameDatas - Already Up to date");
            }
#endif
        }

#if DEBUG
        private void LogDataState()
        {
            Debug.Log($"Current Data State:\n" +
                      $"Worlds: {m_savedDatas.worldDatas.Count}\n" + //  updated from worldDataList
                      $"Diamonds: {m_savedDatas.diamonds}\n" +
                      $"Selected World: {m_savedDatas.selectedWorldId}\n" +
                      $"Selected World Coins: {m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].coins}\n" +
                      $"Best Score: {m_savedDatas.bestScore}\n" +
                      $"Strength Level: {m_savedDatas.worldDatas[m_savedDatas.selectedWorldId].strengthLevel}");
        }

#endif

        #endregion


    }

}