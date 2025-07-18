using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
#if UNITY_WEBGL && !UNITY_EDITOR
    using System.Runtime.InteropServices;
#endif

namespace Pinpin
{
    public partial class GameDatas : ScriptableObject
    {
        [Serializable]
        public class SavedDatas
        {
            [Serializable]
            public class WorldData
            {
                public ulong coins = 0;
                public int goldMultiplier;
                public int currentGoal;
                public int finishedGoal;
                public int highScore;
                public int strengthLevel = 0;
                public int speedLevel = 0;
                public int bounceLevel = 0;

                public WorldData(int goldMultiplier, int currentGoal, int highScore)
                {
                    this.goldMultiplier = goldMultiplier;
                    this.currentGoal = currentGoal;
                    this.highScore = highScore;
                }
            }

            public const string FILENAME = "product.sav";
            public const string PLAYERPREFS_KEY = "Pinpin_SavedData";
            public static string path { get { return (Application.persistentDataPath + "/" + FILENAME); } }
            private static bool m_dataLoaded = false;

            // All existing fields
            public uint lastApplicationLaunchTime = 0;
            public uint firstApplicationLaunchTime = 0;
            public uint lastApplicationQuitTime = 0;
            public uint consecutiveDaysLaunches = 0;
            public uint lastFreeGiftTime = 0;
            public uint lastVIPDailyGiftTime = 0;
            public uint lastNoAdSubPopupSeenTime = 0;
            public uint lastWeeklySubPopupSeenTime = 0;
            public uint lastChristmasEventSeenTime = 0;
            public bool firstLaunch = true;
            public bool tutorialDone = false;
            public int bestScore = 0;
            public int selectedCharacterId = 0;
            public bool isSelectedCharacterPremium = false;
            public int selectedCharacterType = 0;
            public int coins = 0;
            public int diamonds = 0;
            public int strengthLevel = 0;
            public int speedLevel = 0;
            public int bounceLevel = 0;
            public int bonusLevel = 0;
            public int characterLevel = 0;
            public List<int> unlockedCharacters = new List<int>();
            public List<int> unlockedBalls = new List<int>();
            public List<int> unlockedWorlds = new List<int>();
            public Dictionary<int, int> temporaryBallLaunches = new Dictionary<int, int>();
            public int selectedWorldId = 0;
            public int selectedBallId = 0;
            public bool isFirstJackpot = true;
            public int jackpotProgression = 0;
            public int goldenBallCount = 0;
            public int lifetimeShots = 0;
            public Dictionary<int, WorldData> worldDatas = new Dictionary<int, WorldData>();
            public DaytimeMaterial.DayTime dayTime = DaytimeMaterial.DayTime.Day;
            public bool noAds = false;
            public bool vip = false;
            public bool GDPRConsent = false;
            public bool alreadyUsedAGoldenBall = false;
            public int rateboxShown = 0;
            public int sessionCount = 0;
            public bool isInstagramCharacterUnlocked = false;
            public bool isInstagramShown = false;
            public bool firstTimeVIPPurchase = true;
            public bool firstTimeVIPPurchaseDone = false;
            public bool rvCount48sent = false;
            public bool rvCount72sent = false;
            public uint lifetimeRewardedShown = 0;

            public SavedDatas()
            {
                unlockedCharacters.Add(0);
                unlockedWorlds.Add(0);
                worldDatas.Add(0, new WorldData(1, 0, 0));
                unlockedBalls.Add(0);
            }

            //#if UNITY_WEBGL && !UNITY_EDITOR
            //                [DllImport("__Internal")]
            //                private static extern void SyncFiles();
            //#endif

            public static SavedDatas LoadDatas()
            {
                SavedDatas datas = new SavedDatas();

                bool hasMigrated = PlayerPrefs.GetInt("Pinpin_Migrated", 0) == 1;

                // Try loading from PlayerPrefs first (if already migrated)
                if (PlayerPrefs.HasKey(PLAYERPREFS_KEY))
                {
                    try
                    {
                        string jsonData = PlayerPrefs.GetString(PLAYERPREFS_KEY);
                        datas = JsonConvert.DeserializeObject<SavedDatas>(jsonData);
                        datas.UpdateSaveDatas();
                        m_dataLoaded = true;
                        return datas;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("PlayerPrefs load failed: " + e.Message);
                        PlayerPrefs.DeleteKey(PLAYERPREFS_KEY);
                    }
                }

                // If migration hasn't occurred and file exists
                if (!hasMigrated && File.Exists(SavedDatas.path))
                {
                    try
                    {
                        using (FileStream file = File.Open(SavedDatas.path, FileMode.Open))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            datas = (SavedDatas)bf.Deserialize(file);
                        }

                        datas.UpdateSaveDatas();
                        SaveDatas(datas);
                        PlayerPrefs.SetInt("Pinpin_Migrated", 1);
                        File.Delete(SavedDatas.path);

                        Debug.Log("Migration from legacy file to PlayerPrefs completed.");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Migration failed: " + e.Message);
                        try { File.Delete(SavedDatas.path); } catch { }
                    }
                }

                // Fallback to default save
                datas.UpdateSaveDatas();
                SaveDatas(datas);
                m_dataLoaded = true;
                return datas;
            }

            public static void SaveDatas(SavedDatas data)
            {
                if (!m_dataLoaded)
                    return;

                try
                {
                    data.UpdateSaveDatas();

                    string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                    PlayerPrefs.SetString(PLAYERPREFS_KEY, jsonData);
                    PlayerPrefs.Save();

#if UNITY_WEBGL && !UNITY_EDITOR
        // SyncFiles();
#endif
                }
                catch (Exception e)
                {
                    Debug.LogError("Save to PlayerPrefs failed: " + e.Message);
                }
            }



            private void UpdateSaveDatas()
            {
                if (worldDatas == null)
                {
                    worldDatas = new Dictionary<int, WorldData>();
                }

                if (temporaryBallLaunches == null)
                {
                    temporaryBallLaunches = new Dictionary<int, int>();
                    temporaryBallLaunches.Add(0, 0);
                }

                if (unlockedWorlds.Count == 0)
                {
                    unlockedWorlds.Add(0);
                }

                if (unlockedBalls.Count == 0)
                {
                    unlockedBalls.Add(0);
                }

                for (int i = 0; i < unlockedWorlds.Count; i++)
                {
                    if (!worldDatas.ContainsKey(unlockedWorlds[i]))
                    {
                        worldDatas.Add(unlockedWorlds[i], new WorldData(1, characterLevel, unlockedWorlds[i] == selectedWorldId ? bestScore : 0));
                        
                    }

                    Debug.Log(worldDatas[selectedWorldId].coins+" Coins Custom");
                }

                if (goldenBallCount > 0)
                {
                    temporaryBallLaunches.Add(1, goldenBallCount);
                    goldenBallCount = 0;
                }

                //if (coins < 0)
                //{
                //    worldDatas[selectedWorldId].coins = (ulong)int.MaxValue + (ulong)int.MaxValue + (ulong)coins;
                //    coins = 0;
                //}
                //else if (coins > 0)
                //{
                //    worldDatas[selectedWorldId].coins = (ulong)coins;
                //    coins = 0;
                //}

                if (isSelectedCharacterPremium)
                {
                    selectedCharacterType = 1;
                    isSelectedCharacterPremium = false;
                }
            }
            public static void DeleteLegacyFile()
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Debug.Log("Legacy save file deleted.");
                }
            }
            //public WorldData GetSelectedWorldData()
            //{
            //    if (worldDatas == null)
            //        worldDatas = new Dictionary<int, WorldData>();

            //    if (!worldDatas.ContainsKey(selectedWorldId))
            //        worldDatas[selectedWorldId] = new WorldData(1, characterLevel, bestScore);

            //    return worldDatas[selectedWorldId];
            //}
            public void SaveToPlayerPrefs()
            {
                SaveDatas(this);
            }

            public static void LoadCloudDatas(Action<SavedDatas> callback)
            {
                try
                {
                    if (PlayerPrefs.HasKey(PLAYERPREFS_KEY))
                    {
                        string json = PlayerPrefs.GetString(PLAYERPREFS_KEY);
                        SavedDatas data = JsonConvert.DeserializeObject<SavedDatas>(json);
                        callback?.Invoke(data);
#if DEBUG
                        Debug.Log("GameDatas.CloudDatas - LoadCloudDatas() Success.");
#endif
                    }
                    else
                    {
#if DEBUG
                        Debug.Log("GameDatas.CloudDatas - No saved cloud data found.");
#endif
                        callback?.Invoke(null);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"GameDatas.CloudDatas - LoadCloudDatas() Error: {e.Message}");
                    callback?.Invoke(null);
                }
            }
            public static void SaveCloudDatas(SavedDatas data)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(data);
                    PlayerPrefs.SetString(PLAYERPREFS_KEY, json);
                    PlayerPrefs.Save();

#if DEBUG
                    Debug.Log($"GameDatas.CloudDatas - SaveCloudDatas() Success. {json.Length} chars saved.");
#endif
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"GameDatas.CloudDatas - SaveCloudDatas() Error: {e.Message}");
                }
            }
        }
    }
}