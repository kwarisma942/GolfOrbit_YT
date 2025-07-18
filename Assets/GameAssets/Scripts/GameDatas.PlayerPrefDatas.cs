using UnityEngine;
using Pinpin.Types;
using System;

namespace Pinpin
{

	public partial class GameDatas: ScriptableObject
	{
		
		[System.Serializable]
		public class PlayerPrefDatas
		{

			public bool			soundActive = true;
			public bool			vibrationActive = true;
			public float		sfxVolume = 1f;
			public float		musicVolume = 1f;
			public Language		language = Language.English;
			public bool			enableOENotifications = false;
			public bool			firstTimeOEPopup = true;

			public static PlayerPrefDatas LoadDatas ()
			{
				PlayerPrefDatas datas = new PlayerPrefDatas();

				if (PlayerPrefs.HasKey(PlayerPrefKey.SoundActive))
					datas.soundActive = PlayerPrefs.GetInt(PlayerPrefKey.SoundActive) != 0;
					
				if (PlayerPrefs.HasKey(PlayerPrefKey.MusicVolume))
					datas.musicVolume = PlayerPrefs.GetFloat(PlayerPrefKey.MusicVolume);
					
				if (PlayerPrefs.HasKey(PlayerPrefKey.SfxVolume))
					datas.sfxVolume = PlayerPrefs.GetFloat(PlayerPrefKey.SfxVolume);

				if (PlayerPrefs.HasKey(PlayerPrefKey.EnableOENotifications))
					datas.enableOENotifications = PlayerPrefs.GetInt(PlayerPrefKey.EnableOENotifications) != 0;

				if (PlayerPrefs.HasKey(PlayerPrefKey.FirstTimeOEPopup))
					datas.firstTimeOEPopup = PlayerPrefs.GetInt(PlayerPrefKey.FirstTimeOEPopup) != 0;

				/*if (PlayerPrefs.HasKey(PlayerPrefKey.Language))
					datas.language = (Language)PlayerPrefs.GetInt(PlayerPrefKey.Language);
				else*/
				datas.language = (Language)System.Enum.Parse(typeof(Language), I2.Loc.LocalizationManager.CurrentLanguage);

                // Load from YT Game Cloud
                if (ApplicationManager.YTWrapper != null && ApplicationManager.YTWrapper.InPlayablesEnv())
                {
                    ApplicationManager.YTWrapper.LoadGameSaveData((saveData) =>
                    {
                        try
                        {
                            JsonUtility.FromJsonOverwrite(saveData, datas);
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning("YT Game Load failed: " + e.Message);
                        }
                    });
                }

                return datas;
            }

			public void SaveDatas ()
			{
				PlayerPrefs.SetInt(PlayerPrefKey.SoundActive, this.soundActive ? 1 : 0);
				PlayerPrefs.SetFloat(PlayerPrefKey.MusicVolume, this.musicVolume);
				PlayerPrefs.SetFloat(PlayerPrefKey.SfxVolume, this.sfxVolume);
				PlayerPrefs.SetInt(PlayerPrefKey.Language, (int)this.language);
				PlayerPrefs.SetInt(PlayerPrefKey.EnableOENotifications, this.enableOENotifications ? 1 : 0);
				PlayerPrefs.SetInt(PlayerPrefKey.FirstTimeOEPopup, this.firstTimeOEPopup ? 1 : 0);

                // Save to YT Game Cloud
                if (ApplicationManager.YTWrapper != null && ApplicationManager.YTWrapper.InPlayablesEnv())
                {
                    string json = JsonUtility.ToJson(this);
                    int result = ApplicationManager.YTWrapper.SendGameSaveData(json);

                    if (result != 0)
                        Debug.LogWarning("YT Game Save Failed with error code: " + result);
                }
            }

		}
	
	}

}