using UnityEngine;
using Pinpin.Scene;
using GameScene = Pinpin.Scene.ASceneManager.Scene;
using Pinpin.UI;
using System.Collections;
#if UNITY_WEBGL && !UNITY_EDITOR
	using System.Runtime.InteropServices;
#endif

namespace Pinpin.InitScene
{

	/*
		Do Platform Dependant Initialization here.
	*/
	public sealed class InitSceneManager: ASceneManager
	{
		
		//#if UNITY_WEBGL && !UNITY_EDITOR
		//	[DllImport("__Internal")]
		//	private static extern void HideLoadingScreen();
		//#endif
		[SerializeField] private GameScene m_sceneToLoadAfterInit;
		[SerializeField] private GDPRPanel m_gdprPanel;

		protected override void Awake ()
		{
			base.Awake();
			GameDatas.onLoadComplete += OnGameDatasLoaded;


		}

#if PINPIN_SOCIAL
		private void OnSocialManagerInitialized ( bool success )
		{
			ApplicationManager.datas.LoadDatas();
		}
#endif


		private void OnGameDatasLoaded ()
		{
			ContinueLoading();
		}

		public void GDPRPopupAccepted()
		{
			m_gdprPanel.gameObject.SetActive(false);
			ApplicationManager.datas.haveGDPRConsent = true;
			ContinueLoading();
		}

		private void ContinueLoading ()
		{
			LoadingScreen.Show();
			StartCoroutine(LoadSceneAfterDelay(1f));
		}

		

		private IEnumerator LoadSceneAfterDelay(float delay)
		{
			// karim
			Debug.Log("Karim LoadSceneAfterDelay");
            yield return new WaitForSeconds(delay/2);
            Debug.Log("Coinss " + ApplicationManager.datas.coins);
			if (ApplicationManager.datas.coins>0)
			{
                Debug.Log("Player has coins, loading Addressables immediately.");
                yield return ApplicationManager.singleton.LoadGameAssetLibraryAddressable();
                PlayerPrefs.SetInt("itsFirstTime", 1); // Mark as "not first time" to skip future checks
                PlayerPrefs.Save();
            }
            //yield return StartCoroutine(ApplicationManager.ContinueLoading());
            yield return new WaitForSeconds(delay);
				m_sceneToLoadAfterInit = GameScene.MainScene;
				ApplicationManager.LoadScene(m_sceneToLoadAfterInit);			
		}

		protected override void Start ()
		{
			base.Start();
//#if !PINPIN_SOCIAL
				ApplicationManager.datas.LoadDatas();
//#endif


        }

    }

}