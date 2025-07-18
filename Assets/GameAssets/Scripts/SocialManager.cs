#if PINPIN_SOCIAL
#if UNITY_IOS
	using UnityEngine.SocialPlatforms.GameCenter;
	using Prime31;
#endif

#if UNITY_ANDROID
using GooglePlayGames;
	using GooglePlayGames.BasicApi;
	using GooglePlayGames.BasicApi.SavedGame;
#endif

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using System;
using UnityEngine.Events;

namespace Pinpin
{
	[DefaultExecutionOrder(-200)]
	public static class SocialManager
	{

		public enum AchievementID
		{
			KILL_FIRST_ENEMY,
			UNLOCK_ALL_POWERUP,
			UNLOCK_ALL_WEAPONS,
			UNLOCK_ALL_CHARACTERS,
			WIN_MORE_THAN_500
		}

		public enum LeaderboardID
		{
			Leaderboard_BestScores
		}

		public static bool haveSocialSystem
		{
			get
			{
				#if MOBILE
					return (true);
				#else
					return (false);
				#endif
			}
		}

	#if MOBILE
		
		public static event UnityAction<bool> onInitilized;
		public static string saveName;
		public static bool isInitilized = false;
		
		#if !UNITY_EDITOR
			private static Action<byte[], bool> m_dataLoadedCallback;
		#endif
		
		#if UNITY_ANDROID
			private static Dictionary<AchievementID, string> androidAchievementsList;
			private static Dictionary<LeaderboardID, string> androidLeaderboardsList;
			private static byte[] m_dataToSave;
			private static bool m_saving = false;
		#endif

		public static void Init ()
		{
			#if DEBUG
				Debug.Log("SocialManager - Init.");
			#endif

			#if !UNITY_EDITOR
				#if UNITY_ANDROID

					androidAchievementsList = new Dictionary<AchievementID, string>();
					/*androidAchievementsList.Add(AchievementID.KILL_FIRST_ENEMY, GPGSIds.achievement_first_blood);
					androidAchievementsList.Add(AchievementID.UNLOCK_ALL_CHARACTERS, GPGSIds.achievement_chameleon);
					androidAchievementsList.Add(AchievementID.UNLOCK_ALL_POWERUP, GPGSIds.achievement_overload);
					androidAchievementsList.Add(AchievementID.UNLOCK_ALL_WEAPONS, GPGSIds.achievement_armed__dangerous);
					androidAchievementsList.Add(AchievementID.WIN_MORE_THAN_500, GPGSIds.achievement_scavenger);*/

					androidLeaderboardsList = new Dictionary<LeaderboardID, string>();
					//androidLeaderboardsList.Add(LeaderboardID.Leaderboard_BestScores, GPGSIds.leaderboard_score);

					PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
						.EnableSavedGames()
						.Build();

					PlayGamesPlatform.InitializeInstance(config);
					// recommended for debugging:
					PlayGamesPlatform.DebugLogEnabled = true;
					// Activate the Google Play Games platform
					PlayGamesPlatform.Activate();
				#elif UNITY_IOS
					GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
				#endif

				// Authenticate and register a ProcessAuthentication callback
				// This call needs to be made before we can proceed to other calls in the Social API
				Social.localUser.Authenticate(ProcessAuthentication);
			#else
				SocialManager.ProcessAuthentication(true);
			#endif

		}

		private static void ProcessAuthentication ( bool success )
		{
			#if DEBUG
				if (success)
					Debug.Log("SocialManager - Authenticated");
				else
					Debug.LogError("SocialManager - Failed to authenticate");
			#endif
			isInitilized = success;
			if (onInitilized != null)
				onInitilized.Invoke(success);
		}

		#region LearderBoards Loading

		#if UNITY_ANDROID
		public static void LoadUserLeaderboardPosition ( LeaderboardID leaderboardID, TimeScope timeScope, Action<int, uint> positionCallback )
		{
			if (Social.localUser.authenticated == false)
				return;

			ILeaderboard newLeaderboard = Social.CreateLeaderboard();
			newLeaderboard.id = androidLeaderboardsList[leaderboardID];
			Debug.Log("LocalUserID : " + Social.localUser.id);
			
			newLeaderboard.SetUserFilter(new string[] { Social.localUser.id });
			newLeaderboard.range = new Range(0, 1);
			newLeaderboard.timeScope = timeScope;
			newLeaderboard.LoadScores(( bool success ) =>
			{
				if (success)
				{
					Debug.Log(newLeaderboard.scores.Length);
					positionCallback.Invoke(newLeaderboard.scores[0].rank, newLeaderboard.maxRange);
				}
			});
		}

		public static void LoadLeaderBoard ( LeaderboardID leaderboardID, TimeScope timeScope, int rank, int count, Action<IScore[], IUserProfile[]> scoreCallback )
		{
			if (Social.localUser.authenticated == false)
				return;

			ILeaderboard newLeaderboard = Social.CreateLeaderboard();
			newLeaderboard.id = androidLeaderboardsList[leaderboardID];
			newLeaderboard.timeScope = timeScope;
			newLeaderboard.range = new Range(rank, count);
			newLeaderboard.LoadScores(( bool success ) =>
			{
				if (success)
					LoadUserProfiles(newLeaderboard.scores, scoreCallback);
			});
		}

		public static void LoadUserProfiles ( IScore[] scores, Action<IScore[], IUserProfile[]> scoreCallback )
		{
			string[] userIDs = new string[scores.Length];
			for (int i = 0; i < scores.Length; i++)
			{
				userIDs[i] = scores[i].userID;
			}
			Social.LoadUsers(userIDs, ( IUserProfile[] userProfiles ) =>
			{
				scoreCallback.Invoke(scores, userProfiles);
			});
		}
		#endif
		#endregion


		#region Cloud Save 
		//load save from cloud
		public static void LoadFromCloud (Action<byte[], bool> dataLoadedCallback)
		{
			#if DEBUG
				Debug.Log("SocialManager - LoadFromCloud().");
			#endif

			#if !UNITY_EDITOR
				m_dataLoadedCallback = dataLoadedCallback;
				#if UNITY_ANDROID
					m_saving = false;
					((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
						SocialManager.saveName,
						DataSource.ReadCacheOrNetwork,
						ConflictResolutionStrategy.UseLongestPlaytime,
						SavedGameOpened);
				#endif
				#if UNITY_IOS
					byte[] datas = P31CloudFile.readAllBytes(saveName);
					dataLoadedCallback.Invoke(datas, datas != null);
				#endif
			#else
				dataLoadedCallback.Invoke(null, false);
			#endif
		}

		public static void SaveGameDatas(byte[] datas)
		{
			if (Social.localUser.authenticated == false)
			{
				#if DEBUG
					Debug.LogError("SocialManager - SaveGameDatas(). Not authentificated");
				#endif
				return;
			}

			#if DEBUG
				Debug.Log("SocialManager - SaveGameDatas().");
			#endif
			#if !UNITY_EDITOR
				#if UNITY_ANDROID
					m_dataToSave = datas;
					m_saving = true;
					((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(saveName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SavedGameOpened);
				#endif
				#if UNITY_IOS
					P31CloudFile.writeAllBytes(saveName, datas);
				#endif
			#endif
		}

		#if UNITY_ANDROID && !UNITY_EDITOR
			private static void SavedGameOpened ( SavedGameRequestStatus status, ISavedGameMetadata game )
			{
				//check success
				if (status == SavedGameRequestStatus.Success)
				{
					Debug.Log("Load Datas success");
					//saving
					if (m_saving)
					{
						//create builder. here you can add play time, time created etc for UI.
						SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
						SavedGameMetadataUpdate updatedMetadata = builder.Build();
						//saving to cloud
						((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, m_dataToSave, SavedGameWritten);
						//loading
					}
					else
					{
						((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, SavedGameLoaded);
					}
					//error
				}
				else
				{
					Debug.LogWarning("Error opening game: " + status);
				}
			}

			//callback from SavedGameOpened. Check if loading result was successful or not.
			private static void SavedGameLoaded ( SavedGameRequestStatus status, byte[] data )
			{
				if (status == SavedGameRequestStatus.Success)
				{
					Debug.Log("SaveGameLoaded, success=" + status);
					m_dataLoadedCallback.Invoke(data, true);
				}
				else
				{
					Debug.LogWarning("Error reading game: " + status);
					m_dataLoadedCallback.Invoke(null, false);
				}
			}

			//callback from SavedGameOpened. Check if saving result was successful or not.
			private static void SavedGameWritten ( SavedGameRequestStatus status, ISavedGameMetadata game )
			{
				if (status == SavedGameRequestStatus.Success)
				{
					Debug.Log("Game " + game.Description + " written");
				}
				else
				{
					Debug.LogWarning("Error saving game: " + status);
				}
			}
		#endif
		#endregion

		// This function gets called when the LoadAchievement call completes
		/*void ProcessLoadedAchievements(IAchievement[] achievements)
		{
		}*/

		/// <summary>
		/// Reports the achievement progress.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="progressValue">Progress value.</param>
		public static void ReportAchievementProgress ( AchievementID id, float progressValue )
		{
			if (Social.localUser.authenticated == false)
				return;
			#if !UNITY_EDITOR
				string strID = id.ToString();

				#if UNITY_ANDROID
					if (androidAchievementsList.ContainsKey(id))
					{
						strID = androidAchievementsList[id];
					}
					else
						return;
				#endif
			
				Social.ReportProgress(strID, progressValue, success =>
				{
				#if DEBUG
					Debug.Log(success ? "report succeed" : "report failed");
				#endif
				});
			#endif
		}

		public static void ReportLeaderBoard ( LeaderboardID id, long value )
		{
			if (Social.localUser.authenticated == false)
				return;

			#if !UNITY_EDITOR
				string strID = id.ToString();
				#if UNITY_ANDROID
					if (androidLeaderboardsList.ContainsKey(id))
					{
						strID = androidLeaderboardsList[id];
					}
					else
						return;
				#endif
						
				Social.ReportScore(value, strID, (bool success) =>
				{
					#if DEBUG
						Debug.Log(success ? "report succeed" : "report failed");
					#endif
				});
			#endif
		}

		public static void ShowAchievementsUI ()
		{
			Social.ShowAchievementsUI();
		}

		public static void ShowLeaderboardUI ()
		{
			Social.ShowLeaderboardUI();
		}

	#else
		public static void ReportAchievementProgress ( AchievementID id, float progressValue ) {}
		public static void ReportLeaderBoard ( LeaderboardID id, long value ) {}
		public static void ShowAchievementsUI() {}
		public static void ShowLeaderboardUI() {}
	#endif

	}

}
#endif