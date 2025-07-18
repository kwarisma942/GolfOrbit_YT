using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using YTGameSDK;

namespace Pinpin.UI
{

	public class LoadingScreen: MonoBehaviour
	{

		private static LoadingScreen	singleton { get; set; }

		public YTGameWrapper gameWrapper;

		//[SerializeField] private Image			m_loadingScreenBackground;
		[SerializeField] private GameObject     m_loader;
		[SerializeField] private Text			m_loadingText;
		[SerializeField] private Sprite			m_splashScreen;
		[SerializeField] private Image			m_viewPortFront;
		[SerializeField] private Image			m_viewPortBack;
		[SerializeField] private float			m_swapDelay;
		[SerializeField] private float			m_fadeSpeed;
        [SerializeField] private Image m_loadingBarFillImage;
        [SerializeField] private float m_fillSpeed = 0.3f; // Speed of bar fill
        private Coroutine m_loadingBarRoutine;
        private float m_globalProgress = 0f;
        private bool m_keepFilling = false;
        [SerializeField] private string[] sceneOrder; // Assign in Inspector like: ["MainMenu", "Level1", "Level2"]
        private static int currentSceneIndex = 0; // Updated before loading new scene


        private int screensCount { get; set; }
		private int currenFront { get; set; }
		private int currenBack { get; set; }
		private YieldInstruction waitEndFrame { get; set; }
		private YieldInstruction waitDelay { get; set; }
		private float m_splashScreenApparitionTime;
		private bool m_splashScreenActivated;

		private void Awake ()
		{
			if (singleton != null)
			{
				GameObject.Destroy(this.gameObject);
				return ;
			}

			singleton = this;

			gameWrapper.SendGameFirstFrameReady();

			this.waitEndFrame = new WaitForEndOfFrame();
			this.waitDelay = new WaitForSeconds(m_swapDelay);
			GameObject.DontDestroyOnLoad(this.gameObject);
			Hide();
		}

		public static void ShowSplashScreen ()
		{
			#if DEBUG
						Debug.Log("LoadingScreen - ShowSplashScreen()");
			#endif

			singleton.m_viewPortFront.sprite = singleton.m_splashScreen;
			singleton.m_viewPortFront.color = Color.white;
			singleton.m_splashScreenApparitionTime = Time.time;
			singleton.m_viewPortFront.gameObject.SetActive(true);
			singleton.m_viewPortBack.gameObject.SetActive(true);
			singleton.gameObject.SetActive(true);
			singleton.m_splashScreenActivated = true;
		}

		public static void HideSplashScreen ()
		{
#if DEBUG
			Debug.Log("LoadingScreen - HideSplashsSreen()");
#endif

			singleton.StartCoroutine(singleton.WaitHideSplashScreen());
		}

		private IEnumerator WaitHideSplashScreen ()
		{
			while(m_splashScreenApparitionTime + ApplicationManager.config.application.splashScreenDuration > Time.time)
			{
				yield return null;
			}

			singleton.m_viewPortFront.DOFade(0f, 0.2f).SetEase(Ease.OutQuad);
			singleton.m_viewPortBack.DOFade(0f, 0.2f).SetEase(Ease.OutQuad).OnComplete(OnFadeComplete);

			yield return false;
		}

		private void OnFadeComplete ()
		{
			singleton.m_viewPortFront.gameObject.SetActive(false);
			singleton.m_viewPortBack.gameObject.SetActive(false);
			//m_loadingScreenBackground.enabled = true;
			m_loader.SetActive(true);
			singleton.gameObject.SetActive(false);

			singleton.m_splashScreenActivated = false;

		}


        public static void Show(string text = "")
        {
#if DEBUG
            Debug.Log("LoadingScreen - Show()");
#endif

            singleton.gameObject.SetActive(true);
            singleton.m_loadingText.text = text;

            if (singleton.m_loadingBarFillImage != null)
            {
                singleton.m_loadingBarFillImage.fillAmount = singleton.m_globalProgress;
            }
        }




        public static void Hide()
        {
#if DEBUG
            Debug.Log("LoadingScreen - Hide()");
#endif

            if (singleton.m_loadingBarRoutine != null)
                singleton.StopCoroutine(singleton.m_loadingBarRoutine);

            singleton.m_keepFilling = false;

            if (!singleton.m_splashScreenActivated)
                singleton.gameObject.SetActive(false);
        }



        private void OnEnable ()
		{
			//if (screensCount > 0) // Set screens To Show diaporama loading
			//{
			//	//m_viewPortBack.color = Color.white;
			//	m_viewPortFront.color = Color.white;
			//	this.currenFront = Random.Range(0, this.screensCount);
			//	m_viewPortFront.sprite = m_screens[this.currenFront];
			//	this.StartCoroutine(this.WaitForNextSwap());
			//}
		}

		private void OnDisable ()
		{
			this.StopAllCoroutines();
		}

		private void ShowNextScreen ()
		{
			//this.currenBack = this.currenFront;
			//while (this.currenBack == currenFront)
			//	this.currenBack = Random.Range(0, this.screensCount);
			//m_viewPortBack.sprite = m_splashScreen[this.currenBack];
			//this.StartCoroutine(this.Swap(m_viewPortFront, m_viewPortBack));
		}
		
		private IEnumerator WaitForNextSwap ()
		{
			yield return (this.waitDelay);
			this.ShowNextScreen();
		}

		private IEnumerator Swap ( Image A, Image B )
		{
			Color c = Color.white;
			while (this.isActiveAndEnabled && A.color.a > 0.01f)
			{
				c.a = Mathf.Lerp(A.color.a, 0f, Time.deltaTime * m_fadeSpeed);
				A.color = c;

				if (this.isActiveAndEnabled == false)
					break;
				yield return (this.waitEndFrame);
			}

			A.sprite = B.sprite;
			A.color = Color.white;
			this.currenFront = this.currenBack;
			this.StartCoroutine(this.WaitForNextSwap());
		}

        //private IEnumerator FillLoadingBar()
        //{
        //    while (m_keepFilling && m_globalProgress < 1f)
        //    {
        //        m_globalProgress += Time.deltaTime * m_fillSpeed;
        //        m_loadingBar.value = m_globalProgress;
        //        yield return null;
        //    }
        //}
        public static void UpdateProgress(float progress)
        {
            if (singleton.m_loadingBarFillImage != null)
            {
                singleton.m_loadingBarFillImage.fillAmount = progress;
                singleton.m_globalProgress = progress;
            }
        }


        public static void LoadSceneWithProgress(string sceneName)
        {
            int targetIndex = System.Array.IndexOf(singleton.sceneOrder, sceneName);
            if (targetIndex == -1)
            {
                Debug.LogWarning("Scene not found in sceneOrder. Defaulting to full range.");
                singleton.StartCoroutine(singleton.InternalLoadSceneWithProgress(sceneName, 0f, 0.3f));
            }
            else
            {
                float segmentStart = targetIndex / (float)singleton.sceneOrder.Length;
                float segmentEnd = (targetIndex + 1) / (float)singleton.sceneOrder.Length;
                currentSceneIndex = targetIndex;
                singleton.StartCoroutine(singleton.InternalLoadSceneWithProgress(sceneName, segmentStart, segmentEnd));
            }
        }

        private IEnumerator InternalLoadSceneWithProgress(string sceneName, float startProgress, float endProgress)
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
            asyncOp.allowSceneActivation = false;

            while (!asyncOp.isDone)
            {
                float sceneProgress = Mathf.Clamp01(asyncOp.progress / 0.9f);
                float visualProgress = Mathf.Lerp(startProgress, endProgress, sceneProgress);
                UpdateProgress(visualProgress);

                if (sceneProgress >= 0.99f)
                {
                    yield return new WaitForSeconds(0.5f);
                    asyncOp.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        private IEnumerator InternalLoadSceneWithProgress(string sceneName)
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
            asyncOp.allowSceneActivation = false;

            while (!asyncOp.isDone)
            {
                float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);
                UpdateProgress(progress);

                if (progress >= 0.99f)
                {
                    yield return new WaitForSeconds(0.5f); // Optional delay
                    asyncOp.allowSceneActivation = true;
                }

                yield return null;
            }
        }


    }

}